using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Definition.Client;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public sealed class ClientBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IDocumentStore store)
        {
            yield return new ProjectListProjection(store);
            yield return new ProjectOverviewProjection(store);
            yield break;
        }
    }
}
