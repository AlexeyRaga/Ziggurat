CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, DateTime createdDate, SecurityData security, ProfileData profile
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login

CompleteRegistrationWithSecurity? Guid registrationId, Guid securityId
CompleteRegistrationWithProfile? Guid registrationId, Guid profileId

CreateSecurityForRegistration? Guid securityId, Guid registrationId, SecurityData security
SecurityCreated! Guid securityId, string login
SecurityPasswordSet! Guid securityId, string login, string password
SecurityCreatedForRegistration! Guid securityId, Guid registrationId, string login, string email

CreateProfileForRegistration? Guid profileId, Guid registrationId, ProfileData profile
ProfileCreated! Guid profileId, string displayName, string email
ProfileCreatedForRegistration! Guid profileId, Guid registrationId