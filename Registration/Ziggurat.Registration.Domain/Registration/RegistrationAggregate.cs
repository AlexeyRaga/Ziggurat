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
        public void CreateRegistration(Guid registrationId, RegistrationData data)
        {
            var securityId = RegistrationIdGenerator.NewSecutiryId(registrationId);
            var profileId = RegistrationIdGenerator.NewProfileId(registrationId);

            var securityData = new SecurityData(securityId, data.Login, data.Email, data.Password);
            var profileData = new ProfileData(profileId, data.DisplayName, data.Email);

            Apply(new RegistrationStarted(registrationId, data.CreatedDate, securityData, profileData));

        }

        public void AttachSecurity(Guid securityId, ILoginIndexLookupService loginIndex)
        {
            Apply(new SecurityAttachedToRegistration(State.Id, securityId));
            TryCompleteRegistration(loginIndex);
        }

        public void AttachProfile(Guid profileId, ILoginIndexLookupService loginIndex)
        {
            Apply(new ProfileAttachedToRegistration(State.Id, profileId));
            TryCompleteRegistration(loginIndex);
        }

        private void TryCompleteRegistration(ILoginIndexLookupService loginIndex)
        {
            if (State.SecurityId.HasValue && State.ProfileId.HasValue)
            {
                if (loginIndex.IsLoginTaken(State.Login))
                {
                    var errors = new List<string> { String.Format("Username '{0}' is already taken", State.Login) };
                    Apply(new RegistrationFailed(State.Id, State.Login, errors));
                }
                else
                {
                    Apply(new RegistrationCompleted(State.Id, State.SecurityId.Value, State.ProfileId.Value, State.Login));
                }
            }
        }
    }
}
