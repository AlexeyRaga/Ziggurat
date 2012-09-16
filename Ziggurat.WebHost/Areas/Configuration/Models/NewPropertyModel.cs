using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class NewPropertyModel
    {
        public Guid FormId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public PropertyType Type { get; set; }
    }
}