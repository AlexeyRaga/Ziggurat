using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    [Serializable]
    public sealed class ProjectOverview
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public sealed class ProjectOverviewProjection
    {
        private IDocumentWriter<string, ProjectOverview> _writer;

        public ProjectOverviewProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<string, ProjectOverview>();
        }

        public void When(ProjectCreated evt)
        {
            _writer.AddOrUpdate(evt.ShortName, view =>
            {
                view.ProjectId = evt.Id;
                view.Name = evt.Name;
                view.ShortName = evt.ShortName;
            });
        }
    }
}
