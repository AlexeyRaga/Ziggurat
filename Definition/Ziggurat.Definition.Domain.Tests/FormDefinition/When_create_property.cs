using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.FormDefinition;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Definition.Domain.Tests.FormDefinition
{
    [TestClass]
    public sealed class When_create_property : AggregateTest<FormDefinitionAggregate>
    {
        private Guid ProjectId = Guid.NewGuid();
        private Guid FormId = Guid.NewGuid();

        [TestMethod]
        public void Should_create_property()
        {
            var propertyId = Guid.NewGuid();

            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some form", "someForm")
            };

            When = form => form.CreateProperty(propertyId, PropertyType.Textbox, "Subject");

            Then = new IEvent[] {
                new PropertyCreated(FormId, propertyId, PropertyType.Textbox, "Subject")
            };
        }

        
    }
}
