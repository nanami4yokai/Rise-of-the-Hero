

Due to unforeseen difficulties of the development process, it is possible that the project will need to be cut to ⅔ or even ⅓, meaning that possibly, only the dungeon (and the Old City) will make it to the final version of the game, in the context of this course assignment. Therefore, I will continue with the devlogs according to their listing on itslearning, and talk about the development process, depending on the topics learned in class. 

Let’s start with the UI, which for now is pretty simple and only partially functional. In the future there will also be a minimap in the top-right corner of the screen, as well as a quests list under that minimap, featuring only one quest while in the dungeon - defeat the monsters and find a way out. Maybe I will also add some subquests, meant to help the player navigate and discover the dungeon more deeply than just beating enemies and following the path.   

![Screenshot_4](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/04ff8235-b05e-4668-8f33-1d19d6527425)

For now it consists of an HP bar, MP bar and Energy bar, as well as a tool bar and inventory icon. Once the inventory item is clicked, the main inventory window will open and darken the rest of the screen, in order to draw more focus to the inventory itself. The inventory has 16 slots for different items, as well as 3 items for the gear (with placeholders) and another 3 for accessories (that would increase the stats of the player). 

![Screenshot_5](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/a9989c8e-2334-48b4-83b9-da9ab0924ae9)
![Screenshot_6](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/a8a75407-de27-44ff-b65e-067d88aff0f6)

![Screenshot_7](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/fba3df91-4dc1-430b-a1d0-a75d6997168c)

The feature that took me the longest to implement and delayed progression so much is the inventory system, specifically trying to pick up items, place them inside the inventory, and then use the items. The main problem was to keep the items as objects throughout the whole process, and use it as necessary  - the picked up item would transfer only to the inventory as a new instance, without any identifier attached to it, even though I used scriptable objects that I attached to prefabs. Therefore, the problem that took me the longest to solve was to stop the creation of new instances each time an item was picked and keep its properties (Type, Category type and Action type, as well as sprite and other properties).For this, see image below. 

 ![Screenshot_8](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/a1fbd32d-5326-419b-9dc4-d19dba881e9a)

As you can see, once the player picks up an item, it goes inside the toolbar (which is also part of the inventory), and the console displays the messages about calling on the “Initialize” method, that triggers the pick up function, the item type (in this case “Key”) and the message that the item with this item type has been initialized successfully. 

![Screenshot_9](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/db9b7478-38dd-4fed-ac3e-fbd5bb60c14c)

Once the “Key” item has been used of the chest, it spawns different types of loot that the player can pick up and place in the inventory. Once spawned, the console will display that the loot has dropped and the same 3 messages as before, when the items are picked up. 

![Screenshot_11](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/aa31c882-178a-4c69-86ca-2db52ca290cb)

The “Loot” prefabs are stackable, meaning items of the same type can be grouped, rather than each item occupying a different inventory slot. This is both more aesthetically pleasing, as well as more familiar to the player. The gear items, once picked up, would automatically go into the gear slots, and unfortunately for now they don’t replace the placeholder sprites, and the 2 images just get assigned to the same slot, but that is an easy future fix. 

![Screenshot_12](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/48cb59c8-11a5-46ec-a03e-35cc6b6bcf33)

Accessing items can be done by using the 1-8 keys on the keyboard. The inventory slot would change color and the item would either disappear, if it’s one of its kind, or decrement the stack number if there are more of the same item in that slot.

Next, let’s talk about animation. It is pretty simple and for now the player and enemies go by the same animation scheme. Since I work with spritesheets for this project, I usually create 3 sprites for each direction in which the character is moving, altering them in the timeline in order to get smooth movement. Depending on the effect I want to achieve (the illusion of the moving speed, which is actually controlled by movement controllers), I would increase or decrease the samples of the animation. I usually create 5 animations for each character- the idle animation (most of the time just one sprite that’s running infinitely, until the character moves to another direction), and the movement to all 4 directions. 

![Screenshot_13](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/ea88b046-3d93-4e7d-bc94-382bdfd4ae30)

It is not enough to control the character movement through code - it will make the character move but it will not look “realistic”, since for all directions only one sprite will show. For the purpose of indicating which animation should the chara play depending on the direction they are moving in, I used the Animator controller that Unity automatically creates for each object you animate in the scene, as well as a blend tree. Below you can see the “CharacterMoveIdle” node, which basically just holds onto the idle animation. Going back and forth from this node, you can see the “Movement” blend tree.

![Screenshot_14](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/fd940779-9656-4935-8af5-4c514c6fb387)

The blend tree is a very useful tool that lets you tell Unity which animation it should play on a character when it’s moving in a direction. Using 2 variables - Horizontal and Vertical, I could indicate the axis that the character would move onto, and using values between -1 and 1 ok those axis, onto the 4 animations - create a map. It is pretty obvious when the character has to play the “move left” animation when it goes to the left, or “move right” when it moves to the right, but what about when it moves diagonally? Or move towards one direction rather than another? This is exactly why the blend tree is such a powerful tool, since it picks the right animation and makes it look organic, no matter the direction the character is moving to. 

![Screenshot_15](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/ce0bd3d3-f0c8-453f-862a-32cccb943156)

