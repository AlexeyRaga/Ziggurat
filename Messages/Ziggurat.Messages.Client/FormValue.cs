using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Messages.Client
{
    public interface IFormValue { }

    public class TextFormValue : IFormValue
    {
        public string Value { get; set; }
    }

    public class IntegerFormValue : IFormValue
    {
        public int Value { get; set; }
    }

    public class FloatFormValue : IFormValue 
    {
        public Double Value { get; set; }
    }
}
