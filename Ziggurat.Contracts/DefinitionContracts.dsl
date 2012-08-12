CreateProject? Guid id, string name, string shortName
ProjectCreated! Guid id, string name, string shortName

CreateForm? Guid projectId, Guid id, string name, string uniqueName
FormCreated! Guid projectId, Guid id, string name, string uniqueName

// Property is an ENTITY.
// ENTITIES inside the boundary have local identity, unique only within the AGGREGATE.
CreateProperty? Guid formId, Guid propertyId, PropertyType type, string name
PropertyCreated! Guid formId, Guid propertyId, PropertyType type, string name

let ! = IPropertyDefinitionEvent

MakePropertyUnused? Guid formId, Guid propertyId
MakePropertyUsed? Guid formId, Guid propertyId

PropertyMadeUnused! Guid formId, Guid propertyId
PropertyMadeUsed! Guid formId, Guid propertyId