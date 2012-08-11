using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition.Properties
{
    internal static class PropertyFactory
    {
        public static PropertyBase BuildPropertyEntity(PropertyType type, ExpandoObject state)
        {
            switch (type)
            {
                case PropertyType.Textbox:
                    return new TextBoxProperty(state);
            }

            throw new ArgumentException("Cannot build property of type " + type.ToString());
        }
    }
}
