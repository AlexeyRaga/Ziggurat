using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Definition.Domain.FormDefinition;
using Ziggurat.Contracts.Definition;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.Tests.FormDefinition
{
    [TestClass]
    public sealed class When_create_form : AggregateTest<FormDefinitionAggregate>
    {
        [TestMethod]
        public void Should_create_form()
        {
            var projectId = Guid.NewGuid();
            var id = Guid.NewGuid();
            When = form => form.Create(projectId, id, "Test Form", "tstfrm");
            Then = new IEvent[] {
                new FormCreated(projectId, id, "Test Form", "tstfrm")
            };
        }

        [TestMethod]
        public void Double_create_should_fail()
        {
            var projectId = Guid.NewGuid();
            var id = Guid.NewGuid();
            Given = new IEvent[] {
                new FormCreated(projectId, id, "Test Form", "tstfrm")
            };
            When = form => form.Create(projectId, Guid.NewGuid(), "Some name", "SomeUid");
            ThenException = ex => ex is InvalidOperationException;
        }
    }
}
