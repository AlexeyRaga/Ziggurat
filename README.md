Ziggurat Overview
=================

Ziggurat is an attempt to learn how to build CQRS-style applications by solving real business problem.

It is believed that the following arguments will be popping up from time to time:

 > We don't need Event Sourcing for the simple feature of 'A', just save data to the DB!

Because of the learning reasong Ziggurat is intentionally "pure" Event Sourcing system and these arguments
will be rejected and will not be considered for this project! :)

Objectives
==========

Ziggurat is a learning prototype system for a project collaboration domain.
Main functions that are supposed to be built:

  - Defining a project
  - Defining a document type (Form)
  - Defining/editing a form position within the project (blockheader, order)
  - Defining properties for document types (a subset of property types will do)
  - Logging in and changing projects (permissions are out of a scope)
  - Displaying/changing a project information (some basic stuff)
  - Displaying/changing a form information (some basic stuff)
  - Displaying/changing a property information (some basic stuff)
  - "Configuration filters": a list of properties for the particular form
  
Technical Composition
=====================

The business-concerns independent core of the system is located in the **Infrastructure** folder.
It consists of the following projects:

  - **Ziggurat.Contracts**. Shared contracts (messages: commands and events). Technically each domain can have its own contracts library, but in this project we don't have many anyway BCs/Domains anyway, so let's keep it simple :)
  
And the code itself is:

  - **Ziggurat.Infrastructure**. As the name says, this project defines core infrastructure such as *Event Store* interfaces and functionality, *Projections* functionality, etc. This project **must not** contain any business logic.
  - **Ziggurat.WebHost**. A system's web interface project. This project should normally be allowed to read projected data (persisted view models) and to send commands only. Building projections is not the responsibility of this project.
  
The **Definition** functionality is represenred by:
 
  - **Ziggurat.Definition.Domain**. A definition domain BC. Contains the aggregates that are included into this BC (such as _Project_, _ProjectStructure_, _FormDefinition_) as well as domain-specific services and lookups (implemented through projections).
  - **Ziggurat.Definition.Projections**. A definition client BC. This project is responsible for building projections that are needed by the client (e.g. list of projects, project structure or "left nav tree", etc). This projections-building functionality is intentionally separated from the frontend (WebHost). The main reason for this separation is the ability to "stop" the client BC, upgrade it, regenerate projections while the WebHost can continue serving users and displaying data.
  - **Ziggurat.Definition.Client** Simply a "runner" (a console app or a windows service) for the Definition functionality. It sopposed to wire things up.
  
 **NOTE**: Both _Ziggurat.Definition.Projections_ and _Ziggurat.Definition.Client_ names are **BAD**. We need to come up with something more meaningful and descriptive.
  