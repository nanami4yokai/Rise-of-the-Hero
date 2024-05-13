Roll-a-Ball tech implementation review

For the first project of the course, the students had to work on a "Roll-a-Ball" game, featuring a ball that the player has to move and collect all the items, scattered around the level. First, we had to follow a tutorial that introduced us to a lot of base principles and functionalities of the Unity game engine, and helped us create a very simple pick up game, with basic UI and level design. 

![Screenshot_1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/b1b95917-005f-46da-bff3-c0ccc9efa09f)

Following that, we had to develop our games however we wanted, and extend it. My idea of an extension of the base game was to introduce it into a forest level, with different terrain and 3rd party assets (3d models, skybox, materials). 

First, I made a main menu scene from which the player could go forward to play the game or quit it. For that, I used the main menu scene of the “Witcher 3: Wild Hunt” and modified it in Adobe Photoshop, to fit my game.  

![Screenshot_2](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/90f7ea32-31a8-43fb-b2d8-dca723b8a6e7)

The switch between scenes could be implemented with a bit of code and the build settings, where I just set the order of the scenes.

![Screenshot_1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/90b691a8-f94b-445c-b209-199c66b3c1c1)

Once the “Play” button is clicked, the player is prompted to the 1st (and only) level of the game, where they have to finish the quest. The banner at the top of the screen is an UI element that appears right when the player enters the level, and has a timeout, implemented by code. A distinguished feature I implemented is the minimap, which should help the player find the pick up items in the scene, since they are all hidden. To make it easier for the player to find the said items, I attached an image to the “Pickup item” prefab that would show on the minimap and indicate that the item is somewhere in the area. After picking up the item, it would disappear from the scene and increase the counter in the quest list. 

![Screenshot_3](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/91603e0f-676d-433f-9277-bf72abe76e29)

Another new feature in this version of the game is the camera that follows the player around the scene. My main idea was to implement a RPG style camera, from a 3rd person view. That would’ve been easy to implement, since Unity does have such a mode inside CineMachine, but since the ball is a spinning object that modifies all its axes when moving, the attached camera would rotate together with the ball. Eventually, I decided to leave the CineMachine and stick to the base Main Camera that UNity provides, and try to make it work through scripts. I locked the ball’s movement on the Z axis, so it wouldn’t spin, and made 2 controllers: for the player and the camera, where I could modify the speed with which the player could move, and the speed with which the camera followed the player. In the scripts I also slowed down the physics influence onto the ball - since the terrain is quite varied, the ball was gathering and losing acceleration, making it pretty difficult to move around. Unfortunately, this and another issue - camera movement, have made it out to the final build of the game. 
