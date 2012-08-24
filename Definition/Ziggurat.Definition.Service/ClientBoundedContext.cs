using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Definition.Projections;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Service
{
    public sealed class ClientBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IProjectionStoreFactory factory)
        {
            yield return new ProjectListProjection(factory);
            yield break;
        }
    }
}
