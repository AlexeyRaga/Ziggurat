CreateRegistration? Guid registrationId, string login, string password
RegistrationCreated! Guid registrationId, string login, string password
RegistrationFailed! Guid registrationId, string login, IList<string> errors
RegistrationSucceded! Guid registrationId, string login