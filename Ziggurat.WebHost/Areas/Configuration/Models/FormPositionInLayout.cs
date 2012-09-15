using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class FormPositionInLayout
    {
        public Guid FormId { get; set; }
        public string Header { get; set; }
        public int Position { get; set; }
    }
}