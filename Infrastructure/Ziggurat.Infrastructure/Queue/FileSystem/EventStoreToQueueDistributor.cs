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
        private readonly ISerializer _serializer;

        private readonly string _queueName;

        private long lastDistributedStamp;

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
            _serializer = serializer;
        }

        public void Run(CancellationToken token)
        {
            var lastMarker = GetMarker();
            lastDistributedStamp = lastMarker.Stamp;

            PerformDistribution(token);
        }

        private void PerformDistribution(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var envelopes = _eventStore
                    .LoadSince(lastDistributedStamp + 1);

                bool thereWasSomethingNew = false;
                foreach (var envelope in envelopes)
                {
                    thereWasSomethingNew = true;
                    _queueWriter.Enqueue(_serializer.SerializeToByteArray(envelope));
                    lastDistributedStamp = envelope.GetStamp();
                }

                if (thereWasSomethingNew)
                {
                    _markerWriter.AddOrUpdate(_queueName, marker => marker.Stamp = lastDistributedStamp);
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
        }
    }
}
