using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Registration.Domain.Lookups.LoginIndex;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationAggregate : AggregateRootBase<RegistrationState>
    {
        public void CreateRegistration(
            Guid registrationId,

            ILoginIndexLookupService uniqueIndex)
        {
        }
    }
}
