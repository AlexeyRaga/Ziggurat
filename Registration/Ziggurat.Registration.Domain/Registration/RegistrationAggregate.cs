using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Registration.Domain.Lookups.LoginIndex;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationAggregate : AggregateRootBase<RegistrationState>
    {
        public void CreateRegistration(
            Guid registrationId,
            RegistrationData data,
            ILoginIndexLookupService uniqueIndex)
        {
            var errors = new List<string>();
            if (uniqueIndex.IsLoginTaken(data.Login))
            {
                errors.Add(String.Format("Username '{0}' is already taken"));
            }

            if (errors.Any())
            {
                Apply(new RegistrationFailed(registrationId, data.Login, errors));
                return;
            }

            var securityId = RegistrationIdGenerator.NewSecutiryId(data.Login);
            var profileId = RegistrationIdGenerator.NewProfileId(data.Login);

            var securityData = new SecurityData(securityId, data.Login, data.Email, data.Password);
            var profileData = new ProfileData(profileId, data.DisplayName, data.Email);

        }
    }
}
