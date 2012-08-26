using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Registration.Domain.Lookups.LoginIndex
{
    public sealed class LoginIndexLookup
    {
        public IDictionary<string, Guid> Logins { get; private set; }

        public LoginIndexLookup()
        {
            Logins = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
