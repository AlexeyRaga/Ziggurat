CreateForm? Guid id, string name, string uniqueName
FormCreated! Guid id, string name, string uniqueName

//does Property really needs to have its own ID? Isn't UniqueId enough?
CreateProperty? Guid formId, Guid propertyId, string name, string uniqueName
PropertyCreated! Guid formId, Guid propertyId, string name, string uniqueName 