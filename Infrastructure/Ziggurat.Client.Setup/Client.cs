using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore;
using EventStore.Dispatcher;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Client.Setup
{
    public sealed class Client : IDisposable
    {
        private static readonly byte[] EncryptionKey = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

        private static IStoreEvents WireupEventStore(Action<IEnumerable<Envelope>> eventsDispatcher)
        {
            return Wireup.Init()
               .LogToOutputWindow()
               .UsingSqlPersistence("ESTest") // Connection string is in app.config
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
            throw new NotImplementedException();
        }

        private void SetupEventStore()
        {

        }
    }
}
