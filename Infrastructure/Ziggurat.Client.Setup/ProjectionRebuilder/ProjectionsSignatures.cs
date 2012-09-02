using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public class ProjectionsSignatures
    {
        public IDictionary<string, string> TypeSignatures { get; set; }

        public ProjectionsSignatures()
        {
            TypeSignatures = new Dictionary<string, string>();
        }

        public ProjectionsSignatures(IDictionary<string, string> value)
        {
            if (value == null)
                value = new Dictionary<string, string>();
            else
                value = new Dictionary<string, string>(value);

            TypeSignatures = value;
        }
    }
}
