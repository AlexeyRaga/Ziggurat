using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ziggurat.Definition.Client;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class FormOverviewModel
    {
        public FormInfo Form { get; set; }
        public FormPropertyList Properties { get; set; }

        public FormOverviewModel(FormInfo form, FormPropertyList props)
        {
            Form       = form;
            Properties = props;
        }
    }
}