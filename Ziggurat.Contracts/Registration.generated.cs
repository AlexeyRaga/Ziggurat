// ----------------------------------------------------------------------------------------------------
// This code is generated based on *.dsl file.
// Do not edit this file. Edit *.dls files instead.
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ziggurat.Contracts.Registration
{
	[Serializable, DataContract]
	public sealed partial class CreateRegistration : ICommand
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }
		[DataMember(Order = 2 )] public string Password { get; set; }

		public CreateRegistration() { }
		public CreateRegistration(Guid registrationId, string login, string password)
		{
			RegistrationId = registrationId;
			Login = login;
			Password = password;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }
		[DataMember(Order = 2 )] public string Password { get; set; }

		public RegistrationCreated() { }
		public RegistrationCreated(Guid registrationId, string login, string password)
		{
			RegistrationId = registrationId;
			Login = login;
			Password = password;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationFailed : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }
		[DataMember(Order = 2 )] public IList<string> Errors { get; set; }

		public RegistrationFailed() { }
		public RegistrationFailed(Guid registrationId, string login, IList<string> errors)
		{
			RegistrationId = registrationId;
			Login = login;
			Errors = errors;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationSucceded : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }

		public RegistrationSucceded() { }
		public RegistrationSucceded(Guid registrationId, string login)
		{
			RegistrationId = registrationId;
			Login = login;
		}
	}

}
