using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class CreateFormModel
    {
        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        [Display(Name = "Form type name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        [Display(Name = "Form type unique name")]
        public string UniqueName { get; set; }
    }
}