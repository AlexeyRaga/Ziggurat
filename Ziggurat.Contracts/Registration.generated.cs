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
		[DataMember(Order = 1 )] public RegistrationData Data { get; set; }

		public CreateRegistration() { }
		public CreateRegistration(Guid registrationId, RegistrationData data)
		{
			RegistrationId = registrationId;
			Data = data;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public DateTime CreatedDate { get; set; }
		[DataMember(Order = 2 )] public SecurityData Security { get; set; }
		[DataMember(Order = 3 )] public ProfileData Profile { get; set; }

		public RegistrationCreated() { }
		public RegistrationCreated(Guid registrationId, DateTime createdDate, SecurityData security, ProfileData profile)
		{
			RegistrationId = registrationId;
			CreatedDate = createdDate;
			Security = security;
			Profile = profile;
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
	public sealed partial class CreateSecurityForRegistration : ICommand
	{
		[DataMember(Order = 0 )] public Guid SecurityId { get; set; }
		[DataMember(Order = 1 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 2 )] public SecurityData Security { get; set; }

		public CreateSecurityForRegistration() { }
		public CreateSecurityForRegistration(Guid securityId, Guid registrationId, SecurityData security)
		{
			SecurityId = securityId;
			RegistrationId = registrationId;
			Security = security;
		}
	}

	[Serializable, DataContract]
	public sealed partial class SecurityCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid SecurityId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }

		public SecurityCreated() { }
		public SecurityCreated(Guid securityId, string login)
		{
			SecurityId = securityId;
			Login = login;
		}
	}

	[Serializable, DataContract]
	public sealed partial class SecurityPasswordSet : IEvent
	{
		[DataMember(Order = 0 )] public Guid SecurityId { get; set; }
		[DataMember(Order = 1 )] public string Login { get; set; }
		[DataMember(Order = 2 )] public string Password { get; set; }

		public SecurityPasswordSet() { }
		public SecurityPasswordSet(Guid securityId, string login, string password)
		{
			SecurityId = securityId;
			Login = login;
			Password = password;
		}
	}

	[Serializable, DataContract]
	public sealed partial class SecurityCreatedForRegistration : IEvent
	{
		[DataMember(Order = 0 )] public Guid SecurityId { get; set; }
		[DataMember(Order = 1 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 2 )] public string Login { get; set; }
		[DataMember(Order = 3 )] public string Email { get; set; }

		public SecurityCreatedForRegistration() { }
		public SecurityCreatedForRegistration(Guid securityId, Guid registrationId, string login, string email)
		{
			SecurityId = securityId;
			RegistrationId = registrationId;
			Login = login;
			Email = email;
		}
	}

	[Serializable, DataContract]
	public sealed partial class CreateProfileForRegistration : ICommand
	{
		[DataMember(Order = 0 )] public Guid ProfileId { get; set; }
		[DataMember(Order = 1 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 2 )] public ProfileData Profile { get; set; }

		public CreateProfileForRegistration() { }
		public CreateProfileForRegistration(Guid profileId, Guid registrationId, ProfileData profile)
		{
			ProfileId = profileId;
			RegistrationId = registrationId;
			Profile = profile;
		}
	}

	[Serializable, DataContract]
	public sealed partial class ProfileCreated : IEvent
	{
		[DataMember(Order = 0 )] public Guid ProfileId { get; set; }
		[DataMember(Order = 1 )] public string DisplayName { get; set; }
		[DataMember(Order = 2 )] public string Email { get; set; }

		public ProfileCreated() { }
		public ProfileCreated(Guid profileId, string displayName, string email)
		{
			ProfileId = profileId;
			DisplayName = displayName;
			Email = email;
		}
	}

	[Serializable, DataContract]
	public sealed partial class ProfileCreatedForRegistration : IEvent
	{
		[DataMember(Order = 0 )] public Guid ProfileId { get; set; }
		[DataMember(Order = 1 )] public Guid RegistrationId { get; set; }

		public ProfileCreatedForRegistration() { }
		public ProfileCreatedForRegistration(Guid profileId, Guid registrationId)
		{
			ProfileId = profileId;
			RegistrationId = registrationId;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationAttachSecurity : ICommand
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public Guid SecurityId { get; set; }

		public RegistrationAttachSecurity() { }
		public RegistrationAttachSecurity(Guid registrationId, Guid securityId)
		{
			RegistrationId = registrationId;
			SecurityId = securityId;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationAttachProfile : ICommand
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public Guid ProfileId { get; set; }

		public RegistrationAttachProfile() { }
		public RegistrationAttachProfile(Guid registrationId, Guid profileId)
		{
			RegistrationId = registrationId;
			ProfileId = profileId;
		}
	}

	[Serializable, DataContract]
	public sealed partial class ProfileAttachedToRegistration : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public Guid ProfileId { get; set; }

		public ProfileAttachedToRegistration() { }
		public ProfileAttachedToRegistration(Guid registrationId, Guid profileId)
		{
			RegistrationId = registrationId;
			ProfileId = profileId;
		}
	}

	[Serializable, DataContract]
	public sealed partial class SecurityAttachedToRegistration : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public Guid SecurityId { get; set; }

		public SecurityAttachedToRegistration() { }
		public SecurityAttachedToRegistration(Guid registrationId, Guid securityId)
		{
			RegistrationId = registrationId;
			SecurityId = securityId;
		}
	}

	[Serializable, DataContract]
	public sealed partial class RegistrationSucceded : IEvent
	{
		[DataMember(Order = 0 )] public Guid RegistrationId { get; set; }
		[DataMember(Order = 1 )] public Guid SecurityId { get; set; }
		[DataMember(Order = 2 )] public Guid ProfileId { get; set; }
		[DataMember(Order = 3 )] public string Login { get; set; }

		public RegistrationSucceded() { }
		public RegistrationSucceded(Guid registrationId, Guid securityId, Guid profileId, string login)
		{
			RegistrationId = registrationId;
			SecurityId = securityId;
			ProfileId = profileId;
			Login = login;
		}
	}

}

