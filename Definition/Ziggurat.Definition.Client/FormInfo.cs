using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public sealed class FormInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }

    public sealed class FormInfoProjection
    {
        public readonly IDocumentWriter<Guid, FormInfo> _writer;
        public FormInfoProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<Guid, FormInfo>();
        }

        public void When(FormCreated evt)
        {
            _writer.AddOrUpdate(evt.FormId, info =>
            {
                info.Id = evt.FormId;
                info.Name = evt.Name;
                info.UniqueName = evt.UniqueName;
            });
        }
    }
}
