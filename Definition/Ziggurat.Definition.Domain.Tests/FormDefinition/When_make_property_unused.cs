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
    public sealed class When_make_property_unused : AggregateTest<FormDefinitionAggregate> 
    {
        private Guid ProjectId = Guid.NewGuid();
        private Guid FormId = Guid.NewGuid();
        private Guid PropertyId = Guid.NewGuid();

        [TestMethod]
        public void Should_not_make_changes_if_already_unused()
        {
            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some Form", "formUniqueName"),
                new PropertyCreated(FormId, PropertyId, PropertyType.Textbox, "Subject")
            };

            //remember: property is unused by default!
            When = form => form.MakePropertyUnused(PropertyId);

            Then = new IEvent[0];
        }

        [TestMethod]
        public void Should_make_property_used()
        {
            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some Form", "formUniqueName"),
                new PropertyCreated(FormId, PropertyId, PropertyType.Textbox, "Subject")
            };

            //remember: property is unused by default!
            When = form => form.MakePropertyUsed(PropertyId);

            Then = new IEvent[] {
                new PropertyMadeUsed(FormId, PropertyId)
            };
        }

        [TestMethod]
        public void Should_not_make_property_used_second_time()
        {
            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some Form", "formUniqueName"),
                new PropertyCreated(FormId, PropertyId, PropertyType.Textbox, "Subject"),
                new PropertyMadeUsed(FormId, PropertyId)
            };

            //remember: property is unused by default!
            When = form => form.MakePropertyUsed(PropertyId);

            Then = new IEvent[0];
        }

        [TestMethod]
        public void Should_make_property_unused()
        {
            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some Form", "formUniqueName"),
                new PropertyCreated(FormId, PropertyId, PropertyType.Textbox, "Subject"),
                new PropertyMadeUsed(FormId, PropertyId)
            };

            //remember: property is unused by default!
            When = form => form.MakePropertyUnused(PropertyId);

            Then = new IEvent[] {
                new PropertyMadeUnused(FormId, PropertyId)
            };
        }
    }
}
