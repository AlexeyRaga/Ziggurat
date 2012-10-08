using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Messages.Client
{
    public interface IFormValue { }

    public class TextFormValue : IFormValue
    {
        public string Value { get; set; }
    }

    public class IntegerFormValue : IFormValue
    {
        public int? Value { get; set; }
    }

    public class FloatFormValue : IFormValue 
    {
        public long? Value { get; set; }
    }

    public class DateTimeFormValue : IFormValue
    {
        public DateTime? Value { get; set; }
    }

    public class DateFormValue : IFormValue
    {
        public DateTime? Value { get; set; }
    }

    public class TimeFormValue : IFormValue
    {
        public DateTime? Value { get; set; }
    }

    internal static class FormValueFactory
    {
        public static IFormValue CreateEmptyValue(PropertyType propertyType)
        {
            switch (propertyType)
            {
                case PropertyType.Textbox  : return new TextFormValue();
                case PropertyType.Integer  : return new IntegerFormValue();
                case PropertyType.Float    : return new FloatFormValue();
                case PropertyType.DateTime : return new DateTimeFormValue();
                case PropertyType.Date     : return new DateFormValue();
                case PropertyType.Time     : return new TimeFormValue();
                default                    : return null;
            }
        }
    }
}
