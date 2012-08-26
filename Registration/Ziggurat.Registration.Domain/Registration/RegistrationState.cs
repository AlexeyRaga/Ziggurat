using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Registration.Domain.Registration
{
    public sealed class RegistrationState : State
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}
