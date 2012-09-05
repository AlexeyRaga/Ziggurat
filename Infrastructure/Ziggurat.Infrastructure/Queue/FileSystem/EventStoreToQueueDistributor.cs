using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    public sealed class EventStoreToQueueDistributor
    {
        private readonly IProjectionReader<string, EventStoreMarker> _markerReader;
        private readonly IProjectionWriter<string, EventStoreMarker> _markerWriter;
        private readonly IEventStore _eventStore;
        private readonly IQueueWriter _queueWriter;
        private readonly QueueMessageSerializer _serializer;

        private readonly string _queueName;

        private EventStoreMarker _lastKnownDispatchSituation;

        public EventStoreToQueueDistributor(
            string queueName, 
            IQueueFactory queueFactory, 
            IEventStore eventStore, 
            IProjectionStoreFactory projectionStore,
            ISerializer serializer)
        {
            _markerReader = projectionStore.GetReader<string, EventStoreMarker>();
            _markerWriter = projectionStore.GetWriter<string, EventStoreMarker>();
            _queueName = queueName;
            _eventStore = eventStore;
            _queueWriter = queueFactory.CreateWriter(queueName);
            _serializer = new QueueMessageSerializer(serializer);
        }

        public void Run(CancellationToken token)
        {
            _lastKnownDispatchSituation = GetMarker();

            PerformDistribution(token);
        }

        private void PerformDistribution(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var envelopes = _eventStore
                    .LoadSince(_lastKnownDispatchSituation.Stamp);

                bool thereWasSomethingNew = false;

                foreach (var envelope in envelopes)
                {
                    var envelopeStamp = envelope.GetStamp();
                    var envelopeUniqueId = envelope.GetUniqueId();

                    if (_lastKnownDispatchSituation.KnownMessagesWithinStamp.Contains(envelopeUniqueId)) continue;

                    thereWasSomethingNew = true;

                    if (_lastKnownDispatchSituation.Stamp != envelopeStamp)
                        _lastKnownDispatchSituation = new EventStoreMarker(envelopeStamp);

                    _lastKnownDispatchSituation.KnownMessagesWithinStamp.Add(envelopeUniqueId);

                    _queueWriter.Enqueue(_serializer.Serialize(envelope));
                }

                if (thereWasSomethingNew)
                {
                    _markerWriter.AddOrReplace(_queueName, _lastKnownDispatchSituation);
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        private EventStoreMarker GetMarker()
        {
            EventStoreMarker marker;
            if (!_markerReader.TryGet(_queueName, out marker))
                marker = new EventStoreMarker();

            return marker;
        }

        public sealed class EventStoreMarker
        {
            public long Stamp { get; set; }
            public List<string> KnownMessagesWithinStamp { get; set; }

            public EventStoreMarker()
            {
                KnownMessagesWithinStamp = new List<string>();
            }

            public EventStoreMarker(long stamp)
                : this()
            {
                Stamp = stamp;
            }
        }
    }
}
