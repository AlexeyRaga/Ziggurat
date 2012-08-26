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

        public void When(RegistrationAttachSecurity evt)
        {

        }

        public void When(RegistrationAttachProfile evt)
        {
        }
    }
}
