using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Domain.FormDefinition;

namespace Ziggurat.Definition.Domain.Tests.FormDefinition
{
    [TestClass]
    public sealed class When_concatenation_formula_set : AggregateTest<FormDefinitionAggregate>
    {
        private Guid ProjectId = Guid.NewGuid();
        private Guid FormId = Guid.NewGuid();

        [TestMethod]
        public void Should_set_dependencies_and_formula()
        {
            var concatenationPropId = Guid.NewGuid();
            var subjectPropId = Guid.NewGuid();

            var formulaSet = new ConcatenationFormulaDescriptor(new[] { 
                    ConcatenationFormulaPart.CreateConstant("Subject: "),
                    ConcatenationFormulaPart.CreatePropRef(subjectPropId),
                    ConcatenationFormulaPart.CreateConstant(" and-suffix") });

            Given = new IEvent[] {
                new FormCreated(ProjectId, FormId, "Some form", "someForm"),
                new NewPropertyAddedToForm(FormId, concatenationPropId, PropertyType.Concatenation, "Concatenation"),
                new NewPropertyAddedToForm(FormId, subjectPropId, PropertyType.Textbox, "Subject")
            };

            When = form => form.SetConcatenationFormula(concatenationPropId, formulaSet);

            Then = new IEvent[] {
                new PropertyDependenciesSet(FormId, concatenationPropId, new List<Guid> { subjectPropId }),
                new ConcatenationPropertyFormulaSet(FormId, concatenationPropId, formulaSet)
            };
        }
    }
}
