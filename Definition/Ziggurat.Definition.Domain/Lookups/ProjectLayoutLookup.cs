using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Domain.Lookups
{
    public interface IProjectLayoutLookupService
    {
        Guid GetStructureIdByProjectId(Guid projectId);
    }

    public sealed class ProjectLayoutLookupService : IProjectLayoutLookupService
    {
        public IProjectionReader<string, ProjectLayoutLoolup> _reader;
        public ProjectLayoutLookupService(IProjectionStoreFactory store)
        {
            _reader = store.GetReader<string, ProjectLayoutLoolup>();
        }

        public Guid GetStructureIdByProjectId(Guid projectId)
        {
            var index = _reader.Load("index");
            return index.Structures[projectId];
        }
    }

    public sealed class ProjectLayoutLoolup
    {
        public IDictionary<Guid, Guid> Structures { get; set; }

        public ProjectLayoutLoolup()
        {
            Structures = new Dictionary<Guid, Guid>();
        }
    }

    public sealed class ProjectStructureLookupProjection
    {
        private readonly IProjectionWriter<string, ProjectLayoutLoolup> _writer;
        public ProjectStructureLookupProjection(IProjectionStoreFactory store)
        {
            _writer = store.GetWriter<string, ProjectLayoutLoolup>();
        }

        public void When(ProjectLayoutCreated evt)
        {
            _writer.AddOrUpdate("index", index => index.Structures[evt.ProjectId] = evt.Id);
        }
    }
}
