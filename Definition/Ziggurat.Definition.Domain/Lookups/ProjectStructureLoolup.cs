using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Domain.Lookups
{
    public interface IProjectStructureLookupService
    {
        Guid GetStructureIdByProjectId(Guid projectId);
    }

    public sealed class ProjectStructureLookupService : IProjectStructureLookupService
    {
        public IProjectionReader<string, ProjectStructureLoolup> _reader;
        public ProjectStructureLookupService(IProjectionStoreFactory store)
        {
            _reader = store.GetReader<string, ProjectStructureLoolup>();
        }

        public Guid GetStructureIdByProjectId(Guid projectId)
        {
            var index = _reader.Load("index");
            return index.Structures[projectId];
        }
    }

    public sealed class ProjectStructureLoolup
    {
        public IDictionary<Guid, Guid> Structures { get; set; }

        public ProjectStructureLoolup()
        {
            Structures = new Dictionary<Guid, Guid>();
        }
    }

    public sealed class ProjectStructureLookupProjection
    {
        private readonly IProjectionWriter<string, ProjectStructureLoolup> _writer;
        public ProjectStructureLookupProjection(IProjectionStoreFactory store)
        {
            _writer = store.GetWriter<string, ProjectStructureLoolup>();
        }

        public void When(ProjectStructureCreated evt)
        {
            _writer.AddOrUpdate("index", index => index.Structures[evt.ProjectId] = evt.Id);
        }
    }
}
