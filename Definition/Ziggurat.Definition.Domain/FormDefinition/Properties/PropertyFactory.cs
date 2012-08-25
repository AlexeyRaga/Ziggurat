using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    internal static class PropertyFactory
    {
        public static PropertyBase Create(FormDefinitionAggregate root, Guid id, PropertyType type)
        {
            switch (type)
            {
                case PropertyType.Textbox:
                    return new TextboxProperty(root, id);
                case PropertyType.CheckBox:
                    return new CheckboxProperty(root, id);
                case PropertyType.Company:
                    return new CompanyProperty(root, id);
                case PropertyType.Computation:
                    return new ComputationProperty(root, id);
                case PropertyType.Concatenation:
                    return new ConcatenationProperty(root, id);
                case PropertyType.Counter:
                    return new CounterProperty(root, id);
                case PropertyType.CumulativeText:
                    return new CumulativeProperty(root, id);
                case PropertyType.Date:
                    return new DateProperty(root, id);
                case PropertyType.DateTime:
                    return new DateTimeProperty(root, id);
                case PropertyType.DropDownList:
                    return new DropDownProperty(root, id);
                case PropertyType.File:
                    return new FileProperty(root, id);
                case PropertyType.Float:
                    return new FloatProperty(root, id);
                case PropertyType.FormLink:
                    return new FormLinkProperty(root, id);
                case PropertyType.FormType:
                    return new FormTypeProperty(root, id);
                case PropertyType.Integer:
                    return new IntegerProperty(root, id);
                case PropertyType.Markup:
                    return new MarkupProperty(root, id);
                case PropertyType.Member:
                    return new MemberProperty(root, id);
                case PropertyType.RadioButton:
                    return new RadioButtonProperty(root, id);
                case PropertyType.ThreadHistory:
                    return new ThreadHistoryProperty(root, id);
                case PropertyType.Time:
                    return new TimeProperty(root, id);
            }

            throw new InvalidOperationException("Unknown property type: " + type);
        }
    }
}
