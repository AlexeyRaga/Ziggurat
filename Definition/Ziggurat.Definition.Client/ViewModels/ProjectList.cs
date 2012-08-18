using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Client.ViewModels
{
    public sealed class ProjectListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public ProjectListItem() { }
        public ProjectListItem(Guid id, string name, string shortName)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
        }
    }

    public sealed class ProjectList
    {
        public IList<ProjectListItem> Projects { get; set; }

        public ProjectList()
        {
            Projects = new List<ProjectListItem>();
        }
    }

    public sealed class ProjectListProjection
    {
        private IProjectionWriter<string, ProjectList> _writer;

        public ProjectListProjection(IProjectionStoreFactory store)
        {
            _writer = store.GetWriter<string, ProjectList>();
        }

        public void When(ProjectCreated evt)
        {
            _writer.AddOrUpdate("all", list => list.Projects.Add(new ProjectListItem(evt.Id, evt.Name, evt.ShortName)));
        }
    }
}
