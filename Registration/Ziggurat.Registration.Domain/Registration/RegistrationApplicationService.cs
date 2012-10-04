using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Registration.Domain.Lookups.LoginIndex;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationApplicationService : ApplicationServiceBase<RegistrationAggregate>
    {
        private readonly ILoginIndexLookupService _loginIndexService;

        public RegistrationApplicationService(IEventStore eventStore, ILoginIndexLookupService loginIndexService)
            : base(eventStore)
        {
            _loginIndexService = loginIndexService;
        }

        public void When(StartRegistration cmd)
        {
            Update(cmd.RegistrationId, aggregate =>
                aggregate.CreateRegistration(cmd.RegistrationId, cmd.Data));
        }

        public void When(RegistrationAttachSecurity cmd)
        {
            Update(cmd.RegistrationId, aggregate => aggregate.AttachSecurity(cmd.SecurityId, _loginIndexService));
        }

        public void When(RegistrationAttachProfile cmd)
        {
            Update(cmd.RegistrationId, aggregate => aggregate.AttachProfile(cmd.ProfileId, _loginIndexService));
        }
    }
}
