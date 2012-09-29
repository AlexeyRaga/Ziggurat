using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ziggurat.Definition.Client;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class PropertyInfoModel
    {
        public FormInfo Form { get; private set; }
        public FormPropertyList AllFormProperties { get; private set; }

        public PropertyData Property { get; private set; }

        public PropertyInfoModel(FormInfo form, FormPropertyList allProps, PropertyData property)
        {
            Form              = form;
            AllFormProperties = allProps;
            Property          = property;
        }
    }
}