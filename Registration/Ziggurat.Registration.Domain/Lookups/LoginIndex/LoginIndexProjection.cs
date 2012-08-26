using System;
using System.Collections.Generic;
using System.Linq;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;

namespace Ziggurat.Registration.Domain.Lookups.LoginIndex
{
    public sealed class LoginIndexProjection
    {
        private readonly IProjectionWriter<byte, LoginIndexLookup> _writer;

        public LoginIndexProjection(IProjectionStoreFactory storeFactory)
        {
            if (storeFactory == null) throw new ArgumentNullException("storeFactory");
            _writer = storeFactory.GetWriter<byte, LoginIndexLookup>();
        }

        public void When(RegistrationSucceded evt)
        {
            var partition = Partition.GetPartition(evt.Login);
            _writer.AddOrUpdate(partition, index => index.Logins[evt.Login] = evt.SecurityId);
        }
    }
}
