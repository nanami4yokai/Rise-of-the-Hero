And so this is the final dev blog, where I will showcase the final product. In order to fully understand what I am referencing in this dev blog, I suggest you take a look at the playthrough of the game, and the Game Design Document following these links:

https://youtu.be/2y6RPrk7LKw

https://github.com/nanami4yokai/Rise-of-the-Hero/blob/main/GDD.md

Let’s first talk a bit about my original idea. Considering that we agreed with my teachers that it was ok to continue developing my personal project in the context of the GMD course, my first idea was to develop the demo of this game, that would give a first impression in the different aspects I planned to implement in the game. That included the dungeon, which I successfully completed developing, and 2 other levels - the Old City and the Old Church of Belobog, which unfortunately didn’t make it till the end. Nevertheless, the Dungeon part of the game showcases well some of the features - the overall style I was going for, UI elements, combat style and lore. Unfortunately though, I couldn’t expand as much on the lore as I would’ve liked, since my first idea was to make guidance frames that would pop up during the right point in the playthrough, but that would’ve mengelled too much with the code, and I couldn’t find a smart way of detaching this implementation from classes that have nothing to do with it, therefore I limited guidance just to one long quest. 

![Screenshot 2024-06-03 202729](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/e95b301c-29ca-43a0-a8d2-9a132b24145e)

While playing the game you might also encounter some bugs or just awkward scenes - the boss fight where the player and the boss collide strangely, the MP related things (UI stat and MP potion) are present in the game but not functional, the only interactable things are chests and gates, even though there are a bunch of book shelves, pots and barrels that the player could have interacted with too. But, in my opinion, since the functionality would’ve been the same as with the chests, there was no need to implement this feature in the context of this course. Sometimes, you might notice that the key is not always destroyed after being used - when you encounter this, I suggest you replay the game, because quite frankly, I have no idea why it happens and how to fix it.

Otherwise, let’s go through the game again and discuss the final product. 

There have been some changes made in the UI since the last dev blog where I talked about it. And mainly the stats bars, that are now usable and represent in real time the amount of health (HP), mana (MP) and energy the player has. For this course I decided to only focus on 2 - health and energy. And so, the player will lose health when he’s hit by projectiles launched by enemies, and will regenerate health by consuming HP potions, and will lose energy with every sword swing, which will regenerate slowly by being out of combat. 

![stats_bars](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/009464e2-61e3-457b-bde6-8c711bde2b79)

Also, you might notice the minimap in the top right corner of the screen, which follows the player around the dungeon and showcases enemies and chests, so it’s easier for the player to navigate and prepare for battle. 

![minimap](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/dbeb6fd1-e470-470a-b799-4572adb50550)

Another change is the different screens I added - the settings screen and the end screen that marks the finish of the dungeon. 

![Screenshot 2024-06-03 202518](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/014dae8e-942e-4a99-9fdb-749639183664)
![Screenshot 2024-06-03 202626](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/78a7a80a-e0a7-4e0e-9d08-2a33fe203b52)

There is also a set of interactions: player interactions with himself, in-scene objects and enemies, and enemies’ interactions with the player (following and triggering into combat) and containing loot bags. 

![chest interaction](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/f81992ee-122d-42e9-b8d0-878f479e3d34)
![gate interaction gif](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/905dd4ac-f540-4385-b380-7e472962fd69)
![enemy interaction](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/5973c2cb-b5df-4277-b2ba-5fbd8a109b06)
![potion drink](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/d5614c3e-c299-492f-a118-9a02e8bba42f)

