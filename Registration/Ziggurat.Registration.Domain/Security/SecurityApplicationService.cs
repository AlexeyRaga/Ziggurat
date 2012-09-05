using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Registration.Domain.Security
{
    public sealed class SecurityApplicationService : ApplicationServiceBase<SecurityAggregate>
    {
        public SecurityApplicationService(IEventStore eventStore)
            : base(eventStore)
        {
            
        }

        public void When(CreateSecurityForRegistration cmd)
        {
            Update(cmd.SecurityId,
                aggregate => aggregate.CreateForRegistration(cmd.SecurityId, cmd.RegistrationId, cmd.Security));
        }
    }
}
