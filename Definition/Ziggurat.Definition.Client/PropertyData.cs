using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public interface IPropertySpecifics { }
    public sealed class PropertyData
    {
        public Guid FormId { get; set; }
        public Guid PropertyId { get; set; }
        public bool IsUsed { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }

        public IPropertySpecifics Specifics { get; set; }

        public PropertyData() { }
        public PropertyData(Guid formId, Guid propertyId, string name, PropertyType type)
        {
            FormId     = formId;
            PropertyId = propertyId;
            Name       = name;
            Type       = type;
        }

        public static string CreateKey(Guid formId, Guid propertyId)
        {
            return String.Concat(formId.ToString(), "-", propertyId.ToString());
        }
    }

    public sealed class TextBoxPropertySpecifics : IPropertySpecifics 
    {
    }

    public sealed class ConcatenationPropertySpecifics : IPropertySpecifics
    {
        public IList<string> FormulaParts { get; set; }
        public ConcatenationPropertySpecifics() { FormulaParts = new List<string>(); }
    }

    public sealed class DateTimePropertySpecifics : IPropertySpecifics
    {
    }

    public sealed class FilePropertySpecifics : IPropertySpecifics
    {
    }

    public sealed class PropertyDataProjection
    {
        private readonly IDocumentWriter<string, PropertyData> _writer;
        public PropertyDataProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<string, PropertyData>();
        }

        public void When(NewPropertyAddedToForm evt)
        {
            var propData = new PropertyData(evt.FormId, evt.PropertyId, evt.Name, evt.Type);
            _writer.Add(PropertyData.CreateKey(evt.FormId, evt.PropertyId), propData);
        }

        public void When(ConcatenationPropertyFormulaSet evt)
        {
            _writer.Update(PropertyData.CreateKey(evt.FormId, evt.PropertyId), prop =>
            {
                if (prop.Specifics == null) prop.Specifics = new ConcatenationPropertySpecifics();
                var specifics = (ConcatenationPropertySpecifics)prop.Specifics;

                var parts = evt.Formula.Parts
                    .Select(x =>
                    {
                        if (x.IsConstant) return ((ConcatenationConstant)x).Value;
                        if (x.IsPropRef) return "{{" + ((ConcatenationPropRef)x).Value + "}}";
                        return String.Empty;
                    })
                    .Where(x => !String.IsNullOrEmpty(x))
                    .ToList();

                specifics.FormulaParts = parts;
            });
        }
    }
}
