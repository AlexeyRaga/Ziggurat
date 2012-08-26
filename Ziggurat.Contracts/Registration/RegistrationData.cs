using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    public sealed class RegistrationData
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
