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
    public sealed class ProjectInfo
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Guid ProjectLayoutId { get; set; }
    }

    public sealed class ProjectInfoProjection
    {
        private IDocumentWriter<string, ProjectInfo> _writer;
        public ProjectInfoProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<string, ProjectInfo>();
        }

        public void When(ProjectCreated evt)
        {
            _writer.AddOrUpdate(evt.ShortName, view =>
            {
                view.ProjectId = evt.Id;
                view.Name = evt.Name;
                view.ShortName = evt.ShortName;
                view.ProjectLayoutId = evt.ProjectLayoutId;
            });
        }
    }
}
