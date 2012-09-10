using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class CreateProjectModel
    {
        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text), MaxLength(20)]
        [Display(Name = "Project short name")]
        public string ShortName { get; set; }
    }
}