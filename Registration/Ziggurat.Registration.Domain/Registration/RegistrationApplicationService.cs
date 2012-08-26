using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationApplicationService : ApplicationServiceBase<RegistrationAggregate>
    {
        public RegistrationApplicationService(IEventStore eventStore)
            : base(eventStore)
        {

        }

        public void When(CreateRegistration cmd)
        {
            Update(cmd.RegistrationId, aggregate =>
                aggregate.CreateRegistration(cmd.RegistrationId, cmd.Data, null));
        }

        public void When(RegistrationAttachSecurity cmd)
        {
            Update(cmd.RegistrationId, aggregate => aggregate.AttachSecurity(cmd.SecurityId));
        }

        public void When(RegistrationAttachProfile cmd)
        {
            Update(cmd.RegistrationId, aggregate => aggregate.AttachProfile(cmd.ProfileId));
        }
    }
}
