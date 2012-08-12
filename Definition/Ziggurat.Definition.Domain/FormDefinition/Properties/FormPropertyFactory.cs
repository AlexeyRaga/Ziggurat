using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    internal static class FormPropertyFactory
    {
        public static FormPropertyBase Create(Guid id, PropertyType type, String uniqueName)
        {
            switch (type)
            {
                case PropertyType.Textbox:
                    return new TextboxProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.CheckBox:
                    return new CheckboxProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Company:
                    return new CompanyProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Computation:
                    return new ComputationProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Concatenation:
                    return new ConcatenationProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Counter:
                    return new CounterProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.CumulativeText:
                    return new CumulativeProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Date:
                    return new DateProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.DateTime:
                    return new DateTimeProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.DropDownList:
                    return new DropDownProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.File:
                    return new FileProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Float:
                    return new FloatProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.FormLink:
                    return new FormLinkProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.FormType:
                    return new FormTypeProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Integer:
                    return new IntegerProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Markup:
                    return new MarkupProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Member:
                    return new MemberProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.RadioButton:
                    return new RadioButtonProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.ThreadHistory:
                    return new ThreadHistoryProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
                case PropertyType.Time:
                    return new TimeProperty { Id = id, UniqueName = uniqueName, IsUnused = true };
            }

            throw new InvalidOperationException("Unknown property type: " + type);
        }
    }
}
