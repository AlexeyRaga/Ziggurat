using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.Project;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure;

namespace Ziggurat.Definition.Domain.Tests.Project
{
    [TestClass]
    public sealed class When_create_project : AggregateTest<ProjectAggregate>
    {
        [TestMethod]
        public void Should_register_new_project()
        {
            var id = Guid.NewGuid();
            var layoutId = DefinitionIdGenerator.NewProjectLayoutId(id);
            When = prj => prj.Create(id, "Some Name", "shortName2");
            Then = new IEvent[] {
                new NewProjectRegistered(id, "Some Name", "shortName2")
            };
        }

        [TestMethod]
        public void ShortName_cannot_contain_spaces()
        {
            When = prj => prj.Create(Guid.NewGuid(), "Some name", "short name");
            ThenException = ex => ex is ArgumentException;
        }

        [TestMethod]
        public void ShortName_cannot_contain_special_symbols()
        {
            When = prj => prj.Create(Guid.NewGuid(), "Some name", "short+name");
            ThenException = ex => ex is ArgumentException;
        }
    }
}
