using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;

namespace Ziggurat.Registration.Domain.Security
{
    public sealed class SecurityState : State
    {
        public bool Created { get; set; }
        public Guid SecurityId { get; set; }
        public string Login { get; set; }

        public void When(SecurityCreated evt)
        {
            Created = true;
            SecurityId = evt.SecurityId;
            Login = evt.Login;
        }
    }
}
