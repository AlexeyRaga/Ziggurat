CreateRegistration? Guid registrationId, RegistrationData data
RegistrationCreated! Guid registrationId, RegistrationData registration
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login