using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Definition
{
    [Serializable, DataContract]
    public sealed class ConcatenationFormulaDescriptor
    {
        [DataMember]
        public IList<ConcatenationFormulaPart> Parts { get; set; }

        public ConcatenationFormulaDescriptor(IEnumerable<ConcatenationFormulaPart> parts = null)
        {
            Parts = parts == null ? new List<ConcatenationFormulaPart>() : parts.ToList();
        }
    }

    [Serializable, DataContract]
    public abstract class ConcatenationFormulaPart
    {
        [DataMember] public bool IsPropRef { get { return this is ConcatenationPropRef; } }
        [DataMember] public bool IsConstant { get { return this is ConcatenationConstant; } }

        public static ConcatenationFormulaPart CreateConstant(string value)
        {
            return new ConcatenationConstant(value);
        }

        public static ConcatenationFormulaPart CreatePropRef(Guid propertyId)
        {
            return new ConcatenationPropRef(propertyId);
        }
    }

    [Serializable, DataContract]
    public sealed class ConcatenationPropRef : ConcatenationFormulaPart
    {
        [DataMember] public Guid Value { get; set; }
        public ConcatenationPropRef() { }
        public ConcatenationPropRef(Guid value)
        {
            Value = value;
        }
    }

    [Serializable, DataContract]
    public sealed class ConcatenationConstant : ConcatenationFormulaPart
    {
        [DataMember] public string Value { get; set; }
        public ConcatenationConstant() { }
        public ConcatenationConstant(string value)
        {
            Value = value;
        }
    }
}
