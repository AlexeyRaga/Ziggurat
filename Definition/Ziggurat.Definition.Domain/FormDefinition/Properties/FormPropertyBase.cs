using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public abstract class FormPropertyBase
    {
        public Guid Id { get; set; }

        //do we really care? Isn't it enough to have a derived type like FormPropertyText?
        public abstract PropertyType Type { get; }
        public string UniqueName { get; set; }

        public bool IsUnused { get; set; }
	    public bool IsNameHidden { get; set; }

	    public bool IsRequired { get; set; }
	    public bool IsReadOnly { get; set; }

        public void When(PropertyMadeUsed evt) 
        {
            IsUnused = false;
        }
    }
}
