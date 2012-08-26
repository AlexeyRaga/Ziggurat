using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Projections;

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
    }
}
