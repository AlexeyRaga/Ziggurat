using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public sealed class FormPropertyList
    {
        public Guid FormId { get; set; }
        public List<Property> Properties { get; set; }
        public FormPropertyList()
        {
            Properties = new List<Property>();
        }

        public sealed class Property
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public PropertyType Type { get; set; }
            public bool IsUsed { get; set; }
        }
    }

    public sealed class FormPropertyListProjection
    {
        private readonly IDocumentWriter<Guid, FormPropertyList> _writer;
        public FormPropertyListProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<Guid, FormPropertyList>();
        }

        public void When(NewPropertyAddedToForm evt)
        {
            _writer.AddOrUpdate(evt.FormId,
                () =>
                {
                    var list = new FormPropertyList { FormId = evt.FormId };
                    var property = new FormPropertyList.Property
                    {
                        Id   = evt.PropertyId,
                        Name = evt.Name,
                        Type = evt.Type,
                    };
                    list.Properties.Add(property);
                    return list;
                },
                list =>
                {
                    if (list.Properties.Any(x => x.Id == evt.PropertyId)) return;

                    var property = new FormPropertyList.Property
                    {
                        Id   = evt.PropertyId,
                        Type = evt.Type,
                        Name = evt.Name
                    };

                    list.Properties.Add(property);
                });
        }

        public void When(PropertyMadeUsed evt)
        {
            _writer.Update(evt.FormId, list =>
            {
                var property = list.Properties.FirstOrDefault(x => x.Id == evt.PropertyId);
                property.IsUsed = true;
            });
        }

        public void When(PropertyMadeUnused evt)
        {
            _writer.Update(evt.FormId, list =>
            {
                var property = list.Properties.FirstOrDefault(x => x.Id == evt.PropertyId);
                property.IsUsed = false;
            });
        }
    }
}
