using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Registration.Domain.Profile
{
    public sealed class ProfileApplicationService : ApplicationServiceBase<ProfileAggregate>
    {
        public ProfileApplicationService(IEventStore eventStore)
            : base(eventStore)
        {

        }
    }
}
