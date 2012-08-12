using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    internal static class PropertyFactory
    {
        public static PropertyBase Create(FormDefinitionAggregate root, Guid id, PropertyType type, String uniqueName)
        {
            switch (type)
            {
                case PropertyType.Textbox:
                    return new TextboxProperty(root, id, uniqueName);
                case PropertyType.CheckBox:
                    return new CheckboxProperty(root, id, uniqueName);
                case PropertyType.Company:
                    return new CompanyProperty(root, id, uniqueName);
                case PropertyType.Computation:
                    return new ComputationProperty(root, id, uniqueName);
                case PropertyType.Concatenation:
                    return new ConcatenationProperty(root, id, uniqueName);
                case PropertyType.Counter:
                    return new CounterProperty(root, id, uniqueName);
                case PropertyType.CumulativeText:
                    return new CumulativeProperty(root, id, uniqueName);
                case PropertyType.Date:
                    return new DateProperty(root, id, uniqueName);
                case PropertyType.DateTime:
                    return new DateTimeProperty(root, id, uniqueName);
                case PropertyType.DropDownList:
                    return new DropDownProperty(root, id, uniqueName);
                case PropertyType.File:
                    return new FileProperty(root, id, uniqueName);
                case PropertyType.Float:
                    return new FloatProperty(root, id, uniqueName);
                case PropertyType.FormLink:
                    return new FormLinkProperty(root, id, uniqueName);
                case PropertyType.FormType:
                    return new FormTypeProperty(root, id, uniqueName);
                case PropertyType.Integer:
                    return new IntegerProperty(root, id, uniqueName);
                case PropertyType.Markup:
                    return new MarkupProperty(root, id, uniqueName);
                case PropertyType.Member:
                    return new MemberProperty(root, id, uniqueName);
                case PropertyType.RadioButton:
                    return new RadioButtonProperty(root, id, uniqueName);
                case PropertyType.ThreadHistory:
                    return new ThreadHistoryProperty(root, id, uniqueName);
                case PropertyType.Time:
                    return new TimeProperty(root, id, uniqueName);
            }

            throw new InvalidOperationException("Unknown property type: " + type);
        }
    }
}
