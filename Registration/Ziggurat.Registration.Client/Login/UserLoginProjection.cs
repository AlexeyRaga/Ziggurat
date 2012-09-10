using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Registration.Client.Login
{
    public sealed class UserLoginData
    {
        public string Username { get; set; }
        public Guid SecurityId { get; set; }
        public string Password { get; set; }
    }

    public sealed class UserLoginIndex
    {
        public IList<UserLoginData> Logins { get; set; }
        public UserLoginIndex() { Logins = new List<UserLoginData>(); }
    }

    public sealed class UserLoginProjection
    {
        private IDocumentWriter<byte, UserLoginIndex> _passwordsWriter;
        public UserLoginProjection(IDocumentStore storeFactory)
        {
            _passwordsWriter = storeFactory.GetWriter<byte, UserLoginIndex>();
        }

        public void When(SecurityPasswordSet evt)
        {
            var key = Partition.GetPartition(evt.Login);
            _passwordsWriter.AddOrUpdate(key, index =>
            {
                var data = GetDataFromIndexOrCreateNew(index, evt.SecurityId);
                data.Password = evt.Password;
                data.Username = evt.Login;
            });
        }

        private UserLoginData GetDataFromIndex(UserLoginIndex index, Guid securityId)
        {
            var value = index.Logins.FirstOrDefault(x => x.SecurityId == securityId);
            return value;
        }

        private UserLoginData GetDataFromIndexOrCreateNew(UserLoginIndex index, Guid securityId)
        {
            var value = GetDataFromIndex(index, securityId);
            if (value == null)
            {
                value = new UserLoginData { SecurityId = securityId };
                index.Logins.Add(value);
            }

            return value;
        }
    }
}
