CreateProject? Guid id, string name, string shortName
ProjectCreated! Guid id, Guid projectLayoutId, string name, string shortName

CreateProjectLayout? Guid projectId, Guid id
ProjectLayoutCreated! Guid projectId, Guid id

AddFormToProject? Guid projectId, Guid formId
FormAddedToProject! Guid projectId, Guid projectLayoutId, Guid formId

AttachFormToProjectLayout? Guid formId, Guid projectId, Guid projectLayoutId
FormAttachedToProjectLayout! Guid formId, Guid projectId, Guid projectLayoutId, string blockheader, int order

CreateForm? Guid projectId, Guid formId, string name, string uniqueName
FormCreated! Guid projectId, Guid formId, string name, string uniqueName

// Property is an ENTITY.
// ENTITIES inside the boundary have local identity, unique only within the AGGREGATE.
CreateProperty? Guid formId, Guid propertyId, PropertyType type, string name
PropertyCreated! Guid formId, Guid propertyId, PropertyType type, string name

let ! = IPropertyDefinitionEvent

MakePropertyUnused? Guid formId, Guid propertyId
MakePropertyUsed? Guid formId, Guid propertyId

PropertyMadeUnused! Guid formId, Guid propertyId
PropertyMadeUsed! Guid formId, Guid propertyId