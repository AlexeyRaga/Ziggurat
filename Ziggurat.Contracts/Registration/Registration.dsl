CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, DateTime createdDate, SecurityData security, ProfileData profile
RegistrationFailed! Guid registrationId, string login, IList<string> errors


CreateSecurityForRegistration? Guid securityId, Guid registrationId, SecurityData security
SecurityCreated! Guid securityId, string login
SecurityPasswordSet! Guid securityId, string login, string password
SecurityCreatedForRegistration! Guid securityId, Guid registrationId, string login, string email

CreateProfileForRegistration? Guid profileId, Guid registrationId, ProfileData profile
ProfileCreated! Guid profileId, string displayName, string email
ProfileCreatedForRegistration! Guid profileId, Guid registrationId

RegistrationAttachSecurity? Guid registrationId, Guid securityId
RegistrationAttachProfile? Guid registrationId, Guid profileId

ProfileAttachedToRegistration! Guid registrationId, Guid profileId
SecurityAttachedToRegistration! Guid registrationId, Guid securityId

RegistrationSucceded! Guid registrationId, string login