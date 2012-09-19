using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.DocumentStore;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Messages.Client
{
    public static class MessagesClientBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IDocumentStore docStore)
        {
            yield break;
        }
    }
}
