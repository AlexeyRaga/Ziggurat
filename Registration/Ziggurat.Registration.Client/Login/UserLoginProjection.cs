using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Registration.Client.Login
{
    public sealed class UserLoginProjection
    {
        private IProjectionWriter<byte, PasswordIndex> _passwordsWriter;
        public UserLoginProjection(IProjectionStoreFactory storeFactory)
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
