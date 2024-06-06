Continuing with the further topics of the course, I will report about the Game AI and Game architecture I’ve implemented. Starting with the Game AI, which I implemented through the features of the enemies and coroutines.

Meet the Slime. 

![slime_idle1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/45e3ad5e-fae2-42b8-838b-b896660786c3)

Slime is smart. It recognises when, where and for how long to follow the Player and enter combat, using the combination of its animator and some script. The structure of the animator is the same as  the one I used for the Player, since for both, animations are the same, besides their use and sprites. While the player is managed with the keyboard (or other hardware) inputs, the enemies are controlled indirectly, by the player.

![Screenshot 2024-06-01 202031](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/69286cfe-d27a-4c48-bf4d-8fdd1c18c003)

The features of the Slime (and its fellow comrades) start from these variables. Besides the basic variables of an intractable 2D game object, it also possesses a check and attack radius, chase and attack ranges as well as a layer mask that checks for the target the Slime will follow and a reference to the animator attached to the game object. 

The Start() method initializes the rigid body and animator. It also checks for a game object with the “Player” tag, and in case such a game object is found, the target variable gets assigned to the position of the player. 

![Screenshot 2024-06-01 202149](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/ef197b71-a33b-4298-baf8-d358cc36f4a2)

The Update() method is pretty ample, as it is responsible for real-time player targeting, animation handling (updates the “isRunning” bool parameter in the animator, that triggers the start of the blend tree, once the player is identified within the chase range), overlap circle usage (used for identifying if the player is in the chase range or attack range, by creating a circle around the current position of the enemy), direction calculation, which calculates the direction vector from the enemy to the player, rotation (used in correspondence with the animator and updates the parameters “X” and “Y”, to determine the rotation of the enemy to face the player), and of course set the necessary information for the script to identify whether or not the Slime should enter combat, based on the target location. 

The FixedUpdate() is used because of the need to update physics-related aspects consistently, at fixed time intervals, that leads to smoother movement and better handling of physics interactions. And as you can see I did just that, since the method is mainly used for updating movement.

![Screenshot 2024-06-01 202234](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/85a3df13-e934-4d5c-a6e3-9355358d814d)

Setting the desired values in the inspector window, the Slime successfully follows the player and enters combat. 

![enemy_ai](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/8b87dff3-f076-4800-98d2-c12ac5f7ed08)

Another example of Game AI would be the coroutines I used throughout the project. I will reference only one of them, since there is not much one can say about coroutines. 

![Screenshot 2024-06-01 202447](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/53920564-924f-48f2-953b-05b7a233a6a4)

This is one of the coroutines I used, and as you can see it’s responsible for the playing of the “walking” sound clip. With additional implementation in the AudioManager (which I will talk about more in the next dev blog), I call on its instance that either starts or ends the playing of the sound, and the coroutine in this case manages exactly that - the playing of the sound effect based on the player’s movement. 

Generally, coroutines are used to perform actions over a period of time or wait for certain conditions without blocking the main thread, which might end up in freezing the game. Why am I using coroutine in the first place in this context? To create ambience, by default I play 2 other audio clips in a looping manner, but of course I would also like some additional sound effects when interacting with other objects in the scene or when I do something like walking or swinging the sword. That’s why I created a 3rd audio source that would occasionally play the necessary sound effects, together with the main 2, and in order to achieve this, I used a coroutine for the walking sound, as it must only play when the player is moving. 

Next thing worth mentioning is the game architecture I implemented in the project. There are multiple principles I chose to follow, both for a better development experience and structuring of the project. A couple of easily noticeable things are the scriptable objects, prefabs and events I used. 

As you could see in the gif above and images in the previous dev blog, the player stores some items in the inventory. These items are nothing other than instances of the “Loot” prefab, which using some script attach them to scriptable objects. 

![hp potion _ ScrObj](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/be2e83ba-d1a5-4f5f-91ce-f3bb848e9115)

I already talked a bit about scriptable objects in the previous dev blog, and described some of their functionality, but in the context of this dev blog, I would like to mention why I chose to use scriptable objects. Well, they are a great way to manage data, by minimizing redundancy and making game data editing a breeze, since it’s all done in one place. 

Prefabs, even though might seem similar to scriptable objects in terms of efficiency, are used for creating reusable game object templates in the scene, and can be modified based on need. Here are a couple examples: the “Loot” prefab, that gives a “body” to the scriptable objects, and allows for different modifications depending on the usage. 

![hp potion _ ScrObj + prefab](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/e743cb89-43f5-4814-9fae-8260f8e83f0f)
![mp potion _ ScrObj + prefab](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/c8396dc3-ec48-4e2e-a142-34d311b985fd)

Another example of why using prefabs is comfortable is my using the “bat” prefab for default “bat” enemies and the final boss, which is also a “bat” that includes more features:

![Screenshot 2024-06-01 202725](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/b0894cb0-a59f-429d-9813-8e8a1ae6035d)
![Screenshot 2024-06-01 202803](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/76dc05e9-1caa-457b-b04b-e9cef4b3a95d)

Events are another major aspect of game architecture that I mainly used for inventory management and of course buttons. In the case of buttons it is quite easy to explain, since the majority don’t have an over the top functionality implementation. An example could be the main menu buttons:

![Screenshot 2024-06-01 202915](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/18ecd9c5-86fa-43fa-b1d6-23f1346cbb8c)
![Screenshot 2024-06-01 202924](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/b70135d0-b3cf-4cd2-9f36-d627da1433a4)

In the case of the “Play” button, it will class on the “PlayGame” method in the “MainMenu” class, that implements the feature of entering the next scene in the game building list:

![Screenshot 2024-06-01 202935](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/103cd7af-1943-4cec-980e-dfbe1a417046)

Unrelated in this context, but by implementing this method I also implemented the scene management architecture.

In the case of the “Options” button, it will call on an inbuilt method “SetActive”, that basically indicates whether the referenced game object should be visible or not. In this case, when on runtime the player would click on the “Options” button, the “OptionsMenu” will appear, while the “MenuBox” will disappear (please forgive the poor naming choice). This way, the player can browse through the different menus and access different features.  

![Screenshot 2024-06-01 202945](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/ebc6b6ea-afd5-4714-95d4-cd5c9b3b163b)
![Screenshot 2024-06-01 202953](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/21a8f18f-ef6b-4b37-8dcd-3c7f7ca03af7)

Another example of events I used in the project are better identifiable in the “InventoryItem” script. 

![Screenshot 2024-06-01 203001](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/f200c427-dd55-4c65-944f-615ba4d9daf1)

Overall the event system in Unity is a framework that facilitates handling user input and interactions with UI and game objects, but in this case it’s more about UI, since we are talking about the inventory, which is an UI element. For this purpose, Unity provides a series of interfaces for handling different events and in my case - drag-and-dropping items across inventory slots. 

![Screenshot 2024-06-01 203118](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/2d04f72a-615c-4792-a07a-ee72b0b44af0)

In the image above you can see the 3 methods that facilitate the entire drag-and-drop functionality. The “OnBeginDrag” stores the original parent of the item so it can be returned to its parent if necessary, moves the item to the root of the hierarchy, making it independent of its original parent, the script also ensures that the item rendered on top of all other UI elements (by setting it as the last sibling in the hierarchy, since we know that what is further down in the hierarchy comes on top in the scene), and it disables the raycast target on the image component so the item is not blocking raycast to other UI elements while being dragged. Then teh “OnDrag” method updates the position of the item to follow the mouse cursor, and then the “OnEndDrag” method in short tries to find the closest slot to the mouse cursor and checks if the cursor is within the limits of a slot, then checks if the found slot is valid or not. If yes - the item is set under a parent, its position is updated and the raycast is restored, and if not - the item is restored to its original parent, the position is reset to it’s original one and the raycast is also enabled back.  

![inventory item handling](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/24474b00-a471-478c-8315-940e623f5f59)

Other instances of game architecture I can mention is persistence between scenes, which means setting an object as indestructible when switching scenes. By default, game objects are destroyed when switching between scenes, so if I create a game object in scene A, by default it will destroy when moving to scene B, unless I use the DontDestroyOnLoad() method on the game object. For example, the AudioManager that I would like to keep even when moving between the main menu and the dungeon:

![Screenshot 2024-06-01 203129](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/270cf4cb-6981-4ee2-a492-716648004fc1)

I also tried following the Single Responsibility principle, which you can notice in the scripts of the game. While I do confess that I couldn’t follow this rule throughout the whole project, I tried to incorporate it as much as I could. For example, in order to handle the whole inventory system I created 3 scripts - “InventoryItem”, “InventorySlot” and “InventoryManager”. For the player - “Player”, “PlayerMovement” and “PlayerCombat”, and for enemies - “Enemy_AI”, “Enemy_Combat”, and separating the boss functionality with the “BossController” script. 
