This project supposed to be a web-frontend host (and only a host).

Ideally it should not contain any frontend related logic, etc., but should provide 
a hosting infrastructure. 

All the logic (such as controllers, views, scripts, etc.) should belong to BCs (Bounded Contexts or Business Components).

The web host should only be allowed to "own" some common stuff like theme, common css, 
web infrastructure/modularity/wireup functionality.
