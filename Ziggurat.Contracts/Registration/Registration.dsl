CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, SecurityData security, ProfileData profile
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login