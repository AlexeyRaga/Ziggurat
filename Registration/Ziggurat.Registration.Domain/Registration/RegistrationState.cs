using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationState : State
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public Guid? SecurityId { get; set; }
        public Guid? ProfileId { get; set; }

        public bool Success { get; set; }

        public void When(RegistrationCreated evt)
        {
            Id = evt.RegistrationId;
            Login = evt.Security.Login;
        }

        public void When(SecurityAttachedToRegistration evt)
        {
            SecurityId = evt.SecurityId;
        }

        public void When(ProfileAttachedToRegistration evt)
        {
            ProfileId = evt.ProfileId;
        }
    }
}
