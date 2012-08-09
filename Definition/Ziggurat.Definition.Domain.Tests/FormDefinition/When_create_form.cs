using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.FormDefinition;

namespace Ziggurat.Definition.Domain.Tests.FormDefinition
{
    [TestClass]
    public sealed class When_create_form : AggregateTest<FormDefinitionAggregate>
    {
        [TestMethod]
        public void Should_create_form()
        {
            var id = Guid.NewGuid();
            When = form => form.Create(id, "Test Form", "tstfrm");
            Then = new IEvent[] {
                new FormCreated(id, "Test Form", "tstfrm")
            };
        }

        [TestMethod]
        public void Double_create_should_fail()
        {
            var id = Guid.NewGuid();
            Given = new IEvent[] {
                new FormCreated(id, "Test Form", "tstfrm")
            };
            When = form => form.Create(Guid.NewGuid(), "Some name", "SomeUid");
            ThenException = ex => ex is InvalidOperationException;
        }
    }
}
