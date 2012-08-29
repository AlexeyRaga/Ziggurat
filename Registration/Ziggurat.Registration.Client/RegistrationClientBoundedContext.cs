using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Registration.Client
{
    public static class RegistrationClientBoundedContext
    {
        public IEnumerable<object> BuildProjections(IProjectionStoreFactory factory)
        {
            yield return new RegistrationStatus.RegistrationStatusProjection(factory);
        }
    }
}
