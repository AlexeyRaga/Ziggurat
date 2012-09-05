using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;

namespace Ziggurat.Registration.Domain.Profile
{
    public sealed class ProfileAggregate : AggregateRootBase<ProfileState>
    {
        public void CreateForRegistration(Guid id, Guid registrationId, ProfileData data)
        {
            if (State.Created)
                throw new InvalidOperationException("Profile has already been created. Duplication?");

            Apply(new ProfileCreated(id, data.DisplayName, data.Email));
            Apply(new ProfileCreatedForRegistration(id, registrationId));
        }
    }
}
