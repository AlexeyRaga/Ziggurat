CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, DateTime createdDate, SecurityData security, ProfileData profile
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login