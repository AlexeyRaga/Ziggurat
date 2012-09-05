using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;

namespace Ziggurat.Registration.Domain.Profile
{
    public sealed class ProfileState : State
    {
        public bool Created { get; set; }
        public Guid ProfileId { get; set; }

        public void When(ProfileCreated evt)
        {
            Created = true;
            ProfileId = evt.ProfileId;
        }
    }
}
