using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Messages.Client
{
    public sealed class DefaultFormValue
    {
        public PropertyType PropertyType { get; set; }
        public IFormValue PropertyValue { get; set; }

        public DefaultFormValue() { }
        public DefaultFormValue(PropertyType propertyType, IFormValue propertyValue)
        {
            PropertyType  = propertyType;
            PropertyValue = propertyValue;
        }
    }

    public sealed class FormDefaultValues
    {
        public IDictionary<Guid, DefaultFormValue> Values { get; set; }
        public FormDefaultValues()
        {
            Values = new Dictionary<Guid, DefaultFormValue>();
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
                values.Values[evt.PropertyId] = new DefaultFormValue(evt.Type, FormValueFactory.CreateEmptyValue(evt.Type));
            });
        }
    }
}
