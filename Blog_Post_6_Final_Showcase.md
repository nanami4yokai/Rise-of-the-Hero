And so this is the final dev blog, where I will showcase the final product. In order to fully understand what I am referencing in this dev blog, I suggest you take a look at the playthrough of the game, and the Game Design Document following these links, as well as the previous blog posts:

https://youtu.be/2y6RPrk7LKw

https://github.com/nanami4yokai/Rise-of-the-Hero/blob/main/GDD.md

The game is about a guy that got spawned into a dungeon. His main objective is to fight all enemies and find a way out. This project is the first part of the demo, and does not feature the Old City and the Old Church of Belobog (see GDD for reference), but only the dungeon. In the dungeon you will use different features and functionalities, as well as get accustomed to the theme of the game. 

On your way out, you will encounter 4 types of enemies - slimes, bats, skeletons and leshys. The final boss is also a bat, with much more HP and larger in size, that you have to fight to be able to get out of the dungeon. You will discover that some monsters will drop loot - mainly keys that will permit you to open gates/chests, so don’t miss any monster, who knows which one holds onto the desired key. 

![slime_idle1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/54546579-6aa8-43f3-868b-562c4e80d072)
![bat_1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/bda56b8b-bf87-4118-a764-33679d2ecb21)
![skeleton_idle](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/259db2d4-3e14-43bc-9003-cead524394e7)
![leshy_left_1](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/3a9a9e4c-9a57-4016-b776-ed485ea26fb5)

![enemy interaction](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/88a2ef38-2863-4d88-b46b-e0376b81fa19)

Besides monsters, chests also contain loot. The very first chest, the key for which you will pick up at the very start of your playthrough, will give you all the items you will need - gear and potions. These will help you to defeat the monsters and complete the levels. The following 2 chests implement an RNG functionality, meaning that it really depends on how lucky you are whether or not you will get the necessary amount of HP potions to survive in the dungeon. Or who knows, maybe you are so skilled in combat that you won’t even need any backup. 

![chest interaction](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/00e81112-a062-4648-8dbe-c68443c59947)
![potion drink](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/1d087741-4528-4b37-bb9d-120bb12c7923)

Be careful though, and always keep an eye out for your stats. Health and energy are resources you lose in combat, and while you can quickly replenish health points by drinking potions, energy is regenerated over time while not in combat. So if you spot a group of enemies, or bigger enemies on the minimap, I would advise you to rest a bit and enjoy the scenery, while you rest.

![stats_bars](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/e47e90f2-e2a3-4a12-8f24-3adfe5c05570)

Talking about scenery - the dungeon has a dark, yet comfortable atmosphere, with a lot of light points and areas and not so gloomy colors. That was done so that the player can fully enjoy the playthrough and not stress about being killed by every monster that comes at him. The visuals also include a comfortable and almost retro looking UI. It features stats bars in the top left corner, a minimap in the top right corner, and a task bar at the bottom of the screen.

![image](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/eeb400d5-8509-40dc-b989-2d116c30c782)

Now, to access the items in the inventory and the inventory itself, as well as other aspects of the game, you must be aware of the 2 inputs that the game was adapted to - VIA arcade machine inputs (xbox controller) and default keyboard inputs. Some features are limited on the arcade machine, as they would be difficult to implement, for example, the drag and drop of items through the inventory slots, therefore, for the arcade machine only the taskbar is accessible, and you can easily use the items inside their corresponding slots. To move around, use the stick, and to attack, use the Left Trigger button (white button on the arcade machine console).

On a PC with regular keyboard input, use the usual WASD keys to move around, the number keys 1 to 8 to access taskbar slots, or the LMB to interact with chests and gates, or drag and drop items between the inventory slots. Also use the LBM to attack monsters. 

The audio used for this project was selected from freesound.org and edited in Audacity. Audio clips, audio sources and audio management scripts were used in order to build ambience (play 2 audio clips at the same time), and put sound effects on the player, monsters and items. For more technical information on audio implementation, I suggest reading the 5th blog post.


===============================Disclaimer=================================

While playing the game you might also encounter some bugs or just awkward scenes - the boss fight where the player and the boss layer on each other strangely, the MP related things (UI stat and MP potion) are present in the game but not functional, the only interactable things are chests and gates, even though there are a bunch of book shelves, pots and barrels that the player could have interacted with too. But, in my opinion, since the functionality would’ve been the same as with the chests, there was no need to implement this feature in the context of this course. Sometimes, you might notice that the key is not always destroyed after being used - when you encounter this, I suggest you replay the game, because quite frankly, I have no idea why it happens and how to fix it.
