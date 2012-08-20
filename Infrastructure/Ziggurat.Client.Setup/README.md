To Be or Not To Be?
===================

This project is something strange.

From one point of view, there should be a place for setting up an event store (_EventStoreBuilder_),
and every BC can share this code in order not to know and repeat the same configuration again and again
(compression, encryption, event storage connection). 
Because this is a part of _configuration_ it cannot be just put into **Ziggurat.Infrastructure** project.

So it has to be _somewhere_. 
But I hate this project with just one class in it (_JOEventStore_, in fact,
**can** be moved to **Ziggurat.Infrastructure** easily.

If there is any idea how to make it better and to "kill" this project - **WELCOME**!