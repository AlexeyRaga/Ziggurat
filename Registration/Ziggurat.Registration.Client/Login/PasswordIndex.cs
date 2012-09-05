using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Registration.Client.Login
{
    public sealed class PasswordIndex
    {
        public IDictionary<string, string> Passwords { get; private set; }

        public PasswordIndex()
        {
            Passwords = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
