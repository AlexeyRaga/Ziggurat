using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Web
{
    public sealed class ClientBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IProjectionStoreFactory factory)
        {
            yield return new ViewModels.ProjectListProjection(factory);
            yield break;
        }
    }
}
