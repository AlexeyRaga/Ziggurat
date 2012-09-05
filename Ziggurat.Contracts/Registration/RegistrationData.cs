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
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public RegistrationData() { }
        public RegistrationData(string login, string email, string displayName, string password, DateTime createdAt)
        {
            Login = login;
            Email = email;
            DisplayName = displayName;
            Password = password;
            CreatedDate = createdAt;
        }
    }
}
