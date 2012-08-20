Web Host
========

This project supposed to be a web-frontend host (and only a host).

The web frontend represents a "read" side of the system and should only be able to consume read-only projections
data and to send commands.

**Note**: This project should *not* contain any projections (things which react to events and produce/change
persisted models). This logic should belong to the specific client BCs (see _Ziggurat.Definition.Projections_ and
_Ziggurat.Definition.Client_ projects).
 