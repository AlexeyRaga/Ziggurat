using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Domain.ProjectLayout;

namespace Ziggurat.Definition.Domain.Tests.ProjectLayout
{
    [TestClass]
    public sealed class When_create_project_layout : AggregateTest<ProjectLayoutAggregate>
    {
        private Guid ProjectId = Guid.NewGuid();
        private Guid LayoutId = Guid.NewGuid();
        
        [TestMethod]
        public void Should_create_aggregate()
        {
            When = layout => layout.CreateForProject(ProjectId, LayoutId);
            Then = new IEvent[] {
                new ProjectLayoutCreated(ProjectId, LayoutId)
            };
        }
    }
}
