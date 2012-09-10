IMPORTANT CHANGE
================
**From now Ziggurat Web Host is expected to be run on *localtest.me* domain**
Recommended configuration steps:

 1. In IIS create a new web site and point it to Ziggurat.WebHost project folder
 2. In IIS bindings you may change the port, but *leave '**Host&nbsp;Name**' field empty*!
 3. Go to '*http://localtest.me*' and it should work.

Ziggurat Overview
=================

Ziggurat is an attempt to learn how to build CQRS-style applications by solving a real business problem.

It is believed that the following arguments will be popping up from time to time:

 > We don't need Event Sourcing for the simple feature of 'A', just save data to the DB!

Because of the learning reasons Ziggurat is intentionally "pure" Event Sourcing system and these arguments
will be rejected and will not be considered for this project! **THIS IS SPARTA!** :)

Objectives
==========

Ziggurat is a learning prototype system for a project collaboration domain.
Main functionality that is supposed to be built:

  - Defining a project
  - Defining a document type (Form)
  - Defining/editing a form position within the project (blockheader, order)
  - Defining properties for document types (a subset of property types will do)
  - Logging in users and changing projects (permissions are out of a scope, everyone can see everything)
  - Displaying/changing a project information (some basic stuff)
  - Displaying/changing a form information (some basic stuff)
  - Displaying/changing a property information (some basic stuff)
  - "Configuration filters": a list of properties for the particular form
  
Technical Composition
=====================

  - **Ziggurat.Contracts**. Shared contracts (messages: commands and events). Technically each domain can have its own contracts library, but in this project we don't have many BCs/Domains anyway, so let's keep it simple :)

The business-concerns independent core of the system is located in the **Infrastructure** folder.
It consists of the following projects:

  - **Ziggurat.Infrastructure**. As the name says, this project defines the core infrastructure such as *Event Store* interfaces and functionality, *Projections* functionality, etc. This project **must not** contain any business logic.
  - **Ziggurat.WebHost**. A system's web interface project, a front-end. This project should normally be allowed to read projected data (persisted view models) and to send commands only. Building projections is not the responsibility of this project.
  
The **Definition** functionality is represenred by:
 
  - **Ziggurat.Definition.Domain**. A definition domain BC. Contains the aggregates that are included into this BC (such as _Project_, _ProjectStructure_, _FormDefinition_) as well as domain-specific services and lookups (implemented through projections).
  - **Ziggurat.Definition.Client**. A definition client BC. This project is responsible for building projections that are needed by the client (e.g. list of projects, project structure or "left nav tree", etc). This projections-building functionality is intentionally separated from the frontend (WebHost). The main reason for this separation is the ability to "stop" the client BC, upgrade it, regenerate projections while the WebHost can continue serving users and displaying data.
  - **Ziggurat.Definition.Worker** Simply a "runner" (a console app or a windows service, ot an azure worker) for the Definition functionality. It supposed to wire things up. This is the physical process where commands are received, business logic is performed, events are fired, etc.
  
 The **Registration** functionality is responsible for registering/administering users. In the end of the day, we will need some, don't we? :)