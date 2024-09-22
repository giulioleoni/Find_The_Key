# First_Year_Project

This is the final project of the first year of AIV in which to utilize all the knowledge of C# gained during the year.

The goal was to create a 2d point-and-click where you have to explore the map to find a key and open a door that will lead you to the room with treasures. There is also a power-up for movement speed and an npc that will ask you a question and if it is answered correctly will reveal the location of the key.

Here's the full YT [video](https://www.youtube.com/watch?v=fLkiEce2PEA)

## Engine

The project was written all in C#, using the classes and interfaces developed during the year, which created a small Unity-like engine.

| **Features** | **Description**                                                                                                              |
|--------------|------------------------------------------------------------------------------------------------------------------------------|
| Camera       | Possible to manage multiple cameras (parallax scrolling)<br>Different camera behaviors                                       |
| Animation    | Animation component to create 2d animations <br>Possible to choose fps and whether to loop the animation                     |
| Sound        | Possible to create a sound emitter component, <br>for both sound effects and  music                                          |
| UI           | Possible to manage multiple fonts<br>Possible to create 2d texts                                                             |
| Physics      | RigidBody 2d<br>Possible to create box and circle collider 2d<br>Possible to create a collider compound of circles and boxes |
| Pathfinding  | Possible to create pathfinding maps<br>Possible to create pathfinding agents                                                |
| Scene        | Possible to manage multiple scenes                                                                                           |
| GameObjects  | Possible to create GameObjects, Actors and Players                                                                           |

    
![photo_1_2024-09-22_16-55-26](https://github.com/user-attachments/assets/1c607b84-6975-4e46-b966-10fc77499eb9) 

![photo_2_2024-09-22_16-55-26](https://github.com/user-attachments/assets/31384168-da3b-43ed-adc5-a3e7015a8de5)

> **_Draw and Update:_**  Drawable and Updatable objects (every class that implements the related interfaces) will be added to a static manager (one for Draw and one for Update) that will be responsible for executing all Draw and Update methods for the items that have registered.


> **_Physics:_**  every RigidBody will be added to a static manager that will execute their Update methods and handle their collisions.


## Map

The maps were created using [Tiled](https://www.mapeditor.org/) and were then imported into the project as TMX files.

The files were then parsed to reconstruct the correct map.

## Pathfinding

Using TMX files for maps also came in handy to build the pathfinding map, as I already have a list of values I can use to define the costs of nodes in my pathfinding map. I just have to parse them correctly from the file.

When the cursor moves across the map, it will mark reachable nodes in green and unreachable nodes in red.

Once clicked, the fastest route to the destination will be found using an [A-star](https://en.wikipedia.org/wiki/A*_search_algorithm) search algorithm.




https://github.com/user-attachments/assets/2444fe82-fe66-48e8-b9b0-5757949e477b





## Scene

At the start of the game a scene will be created that will load the maps, GameObjects and music.

The scene will check if the player has reached a point where it is necessary to change map and will proceed to clear the current map and load the new one.



https://github.com/user-attachments/assets/e474c1fe-b19f-4603-8fe2-42ba27e4dae6


## Dependancies

This project uses [opentk](https://github.com/opentk/opentk) and [aiv.fast2d](https://github.com/aiv01/aiv-fast2d).























