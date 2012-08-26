using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    public sealed class SecurityData
    {
        public Guid SecurityId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public SecurityData() { }
        public SecurityData(Guid id, string login, string email, string password)
        {
            SecurityId = id;
            Login = login;
            Email = email;
            Password = password;
        }
    }
}
