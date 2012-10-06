using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Messages.Client
{
    public sealed class FormDefaultValues
    {
        public IDictionary<Guid, IFormValue> Values { get; set; }
        public FormDefaultValues()
        {
            Values = new Dictionary<Guid, IFormValue>();
        }
    }

    public sealed class FormDefaultValuesProjection
    {
        private readonly IDocumentWriter<Guid, FormDefaultValues> _writer;
        public FormDefaultValuesProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<Guid, FormDefaultValues>();
        }

        public void When(NewPropertyAddedToForm evt)
        {
            _writer.AddOrUpdate(evt.FormId, values =>
            {
                values.Values[evt.PropertyId] = null;
            });
        }
    }
}
