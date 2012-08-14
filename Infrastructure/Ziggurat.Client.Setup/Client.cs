using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Client.Setup
{
    public sealed class Client : IDisposable
    {
        private static readonly byte[] EncryptionKey = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

		private readonly IMessageDispatcher _commandDispatcher = new ConventionalToWhenDispatcher();
		private readonly IMessageDispatcher _eventsDispatcher = new ConventionalToWhenDispatcher();

		
		public IEventStore EventStore { get; private set; }

		public static Client Create()
		{
			return new Client();
		}

		public void SubscribeToCommands(object handler)
		{
			_commandDispatcher.Subscribe(handler);
		}

		public void SubscribeToEvents(object handler)
		{
			_eventsDispatcher.Subscribe(handler);
		}

		private Client()
		{
			var eventStore = BuildEventStore(DispatchEvents);
			EventStore = eventStore;
		}

		private void DispatchEvents(IEnumerable<Envelope> events)
		{
			foreach (var evt in events)
			{
				_eventsDispatcher.DispatchToAll(evt.Body);
			}
		}

        private static IEventStore BuildEventStore(Action<IEnumerable<Envelope>> eventsDispatcher)
        {
            var joEventStore = ConfigureEventStore(eventsDispatcher);
            return new JOEventStore(joEventStore);
        }

        private static IStoreEvents ConfigureEventStore(Action<IEnumerable<Envelope>> eventsDispatcher)
        {
            return Wireup.Init()
               .LogToOutputWindow()
               .UsingSqlPersistence("EventStream") // Connection string is in app.config
                   .EnlistInAmbientTransaction() // two-phase commit
                   .InitializeStorageEngine()
                   .UsingJsonSerialization()
                //.Compress()
                //.EncryptWith(EncryptionKey)
                //.HookIntoPipelineUsing(new[] { new AuthorizationPipelineHook() })
                .UsingSynchronousDispatchScheduler()
                   .DispatchTo(new DelegateMessageDispatcher(commit => eventsDispatcher(commit.Events.ToEnvelopes())))
               .Build();
        }

        public void Dispose()
        {
            var eventStore = EventStore;
            if (eventStore != null) eventStore.Dispose();
        }
    }
}
