using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Registration.Domain
{
    public sealed class RegistrationDomainBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IProjectionStoreFactory factory)
        {
            yield return new Lookups.LoginIndex.LoginIndexProjection(factory);
            yield break;
        }

        public static IEnumerable<object> BuildApplicationServices(IEventStore eventStore)
        {
            yield return new Security.SecurityApplicationService(eventStore);
            yield return new Profile.ProfileApplicationService(eventStore);
            yield return new Registration.RegistrationApplicationService(eventStore);
            yield break;
        }

        public static IEnumerable<object> BuildEventProcessors(ICommandSender commandSender)
        {
            yield return new Processes.RegistrationProcess(commandSender);
            yield break;
        }
    }
}
