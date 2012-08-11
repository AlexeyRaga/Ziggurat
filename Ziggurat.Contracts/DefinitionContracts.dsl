CreateProject? Guid id, string name, string shortName
ProjectCreated! Guid id, string name, string shortName

CreateForm? Guid projectId, Guid id, string name, string uniqueName
FormCreated! Guid projectId, Guid id, string name, string uniqueName

//does Property really needs to have its own ID? Isn't UniqueId enough?
CreateProperty? Guid formId, Guid propertyId, PropertyType type, string name, string uniqueName
PropertyCreated! Guid formId, Guid propertyId, PropertyType type, string name, string uniqueName 