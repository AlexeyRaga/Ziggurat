using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ziggurat.Definition.Client;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class FormInfoModel
    {
        public FormInfo Form { get; set; }
        public FormPropertyList Properties { get; set; }

        public FormInfoModel(FormInfo form, FormPropertyList properties)
        {
            Form       = form;
            Properties = properties;
        }
    }
}