using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.ProjectLayout;

namespace Ziggurat.Definition.Domain.Tests.ProjectLayout
{
    [TestClass]
    public sealed class When_add_form_to_project : AggregateTest<ProjectLayoutAggregate>
    {
        private Guid ProjectId = Guid.NewGuid();
        private Guid LayoutId = Guid.NewGuid();
        private Guid FormId = Guid.NewGuid();

        [TestMethod]
        public void Should_add_form_to_layout()
        {
            Given = new IEvent[] {
                new ProjectLayoutCreated(ProjectId, LayoutId)
            };

            When = layout => layout.AddForm(FormId);

            Then = new IEvent[] {
                new FormAddedToProject(ProjectId, LayoutId, FormId, ProjectLayoutAggregate.DefaultBlockHeaderName)
            };
        }
    }
}
