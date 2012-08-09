// ----------------------------------------------------------------------------------------------------
// This code is generated based on *.dsl file.
// Do not edit this file. Edit *.dls files instead.
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ziggurat.Contracts
{
	[Serializable, DataContract]
	public sealed partial class CreateForm : ICommand
	{
		[DataMember(Order = 0 )] public Guid Id { get; set; }
		[DataMember(Order = 1 )] public string Name { get; set; }
		[DataMember(Order = 2 )] public string UniqueName { get; set; }

		public CreateForm() { }
		public CreateForm(Guid id, string name, string uniqueName)
		{
			Id = id;
			Name = name;
			UniqueName = uniqueName;
		}
	}

	[Serializable, DataContract]
	public sealed partial class FormCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid Id { get; set; }
		[DataMember(Order = 1 )] public string Name { get; set; }
		[DataMember(Order = 2 )] public string UniqueName { get; set; }

		public FormCreated() { }
		public FormCreated(Guid id, string name, string uniqueName)
		{
			Id = id;
			Name = name;
			UniqueName = uniqueName;
		}
	}

	[Serializable, DataContract]
	public sealed partial class CreateProperty : ICommand
	{
		[DataMember(Order = 0 )] public Guid FormId { get; set; }
		[DataMember(Order = 1 )] public Guid PropertyId { get; set; }
		[DataMember(Order = 2 )] public string Name { get; set; }
		[DataMember(Order = 3 )] public string UniqueName { get; set; }

		public CreateProperty() { }
		public CreateProperty(Guid formId, Guid propertyId, string name, string uniqueName)
		{
			FormId = formId;
			PropertyId = propertyId;
			Name = name;
			UniqueName = uniqueName;
		}
	}

	[Serializable, DataContract]
	public sealed partial class PropertyCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid FormId { get; set; }
		[DataMember(Order = 1 )] public Guid PropertyId { get; set; }
		[DataMember(Order = 2 )] public string Name { get; set; }
		[DataMember(Order = 3 )] public string UniqueName { get; set; }

		public PropertyCreated() { }
		public PropertyCreated(Guid formId, Guid propertyId, string name, string uniqueName)
		{
			FormId = formId;
			PropertyId = propertyId;
			Name = name;
			UniqueName = uniqueName;
		}
	}

}
