using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public sealed class FormInProjectInfoRecord
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }

    public sealed class FormsInProject
    {
        public List<FormInProjectInfoRecord> Forms { get; set; }

        public FormsInProject()
        {
            Forms = new List<FormInProjectInfoRecord>();
        }
    }

    public sealed class FormsInProjectProjection
    {
        private readonly IDocumentWriter<Guid, FormsInProject> _witer;
        public FormsInProjectProjection(IDocumentStore store)
        {
            _witer = store.GetWriter<Guid, FormsInProject>();
        }

        public void When(FormCreated evt)
        {
            _witer.AddOrUpdate(evt.ProjectId,
                forms => forms.Forms.Add(new FormInProjectInfoRecord
                {
                    FormId = evt.FormId,
                    Name = evt.Name,
                    UniqueName = evt.UniqueName,
                }));
        }
    }
}
