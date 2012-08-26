CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, DateTime createdDate, SecurityData security, ProfileData profile
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login

CreateSecurityForRegistration? Guid securityId, Guid registrationId, SecurityData security
SecurityPasswordSet! Guid securityId, string login, string encryptedPassword
SecurityCreatedForRegistration! Guid securityId, Guid registrationId, string login, string displayName, string email