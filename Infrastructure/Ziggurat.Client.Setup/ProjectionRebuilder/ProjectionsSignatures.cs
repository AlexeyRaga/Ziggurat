using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeName = System.String;
using Signature = System.String;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public class ProjectionsSignatures
    {
        public IDictionary<TypeName, Signature> TypeSignatures { get; set; }

        public ProjectionsSignatures()
        {
            TypeSignatures = new Dictionary<TypeName, Signature>();
        }

        public ProjectionsSignatures(IDictionary<TypeName, Signature> value)
        {
            if (value == null)
                value = new Dictionary<TypeName, Signature>();
            else
                value = new Dictionary<TypeName, Signature>(value);

            TypeSignatures = value;
        }
    }
}
