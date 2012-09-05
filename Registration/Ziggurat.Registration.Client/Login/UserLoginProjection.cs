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
    public sealed class UserLoginProjection
    {
        private IDocumentWriter<byte, PasswordIndex> _passwordsWriter;
        public UserLoginProjection(IDocumentStore storeFactory)
        {
            _passwordsWriter = storeFactory.GetWriter<byte, PasswordIndex>();
        }

        public void When(SecurityPasswordSet evt)
        {
            var key = Partition.GetPartition(evt.Login);
            _passwordsWriter.AddOrUpdate(key, index => index.Passwords[evt.Login] = evt.Password);
        }
    }
}
