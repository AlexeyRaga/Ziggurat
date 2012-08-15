using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure;

namespace Ziggurat.Definition.Domain.ProjectStructure
{
    public sealed class ProjectStructureDomainService
    {
        private readonly IBus _bus;
        public ProjectStructureDomainService(IBus bus)
        {
            _bus = bus;
        }

        public void When(ProjectCreated evt)
        {
            _bus.SendCommand(new CreateProjectStructure(evt.Id, Guid.NewGuid()));
        }

        public void When(object evt)
        {
            //do nothing
        }
    }
}
