using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;

namespace Ziggurat.Registration.Domain.Security
{
    public sealed class SecurityAggregate : AggregateRootBase<SecurityState>
    {
        public void CreateForRegistration(Guid id, Guid registrationId, SecurityData data)
        {
            Apply(new SecurityCreated(id, data.Login));
            Apply(new SecurityPasswordSet(id, data.Login, data.Password));
            Apply(new SecurityCreatedForRegistration(id, registrationId, data.Login, data.Email));
        }
    }
}
