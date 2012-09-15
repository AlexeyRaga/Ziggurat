using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ziggurat.Web.Areas.Configuration.Models
{
    public sealed class ProjectFormsListModel
    {
        public List<Group> FormGroups { get; private set; }

        public ProjectFormsListModel()
        {
            FormGroups = new List<Group>();
        }

        public sealed class Form
        {
            public Guid FormId { get; private set; }
            public string Name { get; private set; }

            public Form(Guid formId, string name)
            {
                FormId = formId;
                Name = name;
            }
        }

        public sealed class Group
        {
            public string Header { get; set; }
            public List<Form> Forms { get; set; }

            public Group(string header, IEnumerable<Form> forms)
            {
                Header = header;
                Forms = forms.ToList();
            }
        }
    }
}