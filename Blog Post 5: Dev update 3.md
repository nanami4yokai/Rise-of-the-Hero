Following the topics discussed in class, in this dev blog I will continue with describing my graphics and audio choices, as well as talk a bit about optimization. For rendering I chose the Universal Render Pipeline, as it is an easy solution for introducing lightning and post processing in the project. It is also lightweight, which makes it a good fit for 2D games, since they don’t require high-end graphics, and considering the platform for which the project is developed in the scope of the GMD course (the arcade machine), it is the most suited. 

Since the game is about passing a dungeon, there is no need for too much light or ambience. Nevertheless I chose to set a Global Light component of a weak intensity, in the same color scheme as the entire level. I also set a very light bloom, with a threshold of 1 and intensity of 0.3. 

![Screenshot 2024-06-01 222441](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/57a92551-486b-4b51-a42d-683c15c14d23)
![Screenshot 2024-06-01 222550](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/579c4c20-55bd-48dd-a476-0d4a74d52de9)

Originally my plan was to create a glowing effect on a couple of game objects, the teleport and the projectiles that the enemies shoot at the player. Therefore, I made an unlit shader graph, from which I also created a material, which I attributed to the object. In this example I will talk about the teleport. To create the illusion of glow, I applied a black and white texture over the original sprite sheet. This way the B&W texture created a mask where emission “knows” where to show the color. 

![pentagram](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/a9637d6a-7d18-4c6e-95ff-4af2a42a11ad)
![pentagram_emissionMap](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/b014ffb8-4597-49cb-ab32-74bc14a27d26)
![Screenshot 2024-06-01 223642](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/9660fc00-5285-4d1a-b30c-c8b5c8374e98)

All of this paired with an animation concluded to this result:

![teleport](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/c62ed29b-1ea2-4f05-af3d-5b615b2b9ff8)

As for light components, I used spot lights for the candles and torches. To create a realistic feeling, each prefab of the candles has a different intensity, meaning less candles in a bundle would emit less light. 

![Screenshot 2024-06-01 231718](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/7cbbb258-9251-457d-9da5-765cc534ed5a)

With the torches I used the same spot light, but in this case every torch has the same stats. 

For this project I chose to use 2 cameras - a main camera and a camera for the minimap. Both would follow the player around the dungeon, but the main one is significantly closer to the player than the minimap camera, which makes it easier for the player to spot a nearby enemy or an intractable game object. For this exact reason I replaced the sprites of the player, chests and enemies, so they are easier to spot on the minimap. This was done using the culling mask in the rendering subcomponent of the camera and layers system. The only environment modification I did was to change the background color to black, since I found the default color highly unfit for the game aesthetic, plus it just gives a more neat look and delimits the dungeon well. 

![Screenshot 2024-06-02 173627](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/cdf1d6b2-6b28-4aa6-8843-50f9207c01f9)

I also made a use of the particles system for the death of the enemies and the player. 

![particle system](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/f9bc2f15-caf0-4086-824a-fb3acd41dff7)

It is a really simple particle system that I decided to include most for representation of the enemies’ or player’s death, as it is important to give the player the right amount of feedback after an action is done. 

![Screenshot 2024-06-02 184853](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/6c7133f4-f6c4-4f37-923b-e69173e28c1a)

I mainly played with the values I highlighted in the image above, so that there is as much randomness as possible in color, shape, size, rotation and also gravity, so some particles reach the bottom faster than the rest. My original idea also included a collider that would stop the particles from seemingly falling into nowhere, and instead create the illusion of falling onto the ground. But, that would’ve complicated things, as I did not like the collider the particle system was suggesting, so I created a separate polygon collider, that was the child of the particle prefab, and that would have to disappear once the coroutine of the particle system was done, which of course, involved coding and more trouble than necessary. Therefore, I settled with the particle system as is. 

I had particularly much fun with adding audio to the game. Thanks to the DIM course I already had a couple of audio clips done, which helped a lot. For editing audio clips I used Audacity - a very easy software that provides a wide range of modifications to be done on an audio clip. All audio sources I got from freesound.org. So let’s go through each component of managing audio. 

I only used one audio listener, which is the default one set into the main camera game object. The “AudioManager” script was the one that helped me a lot with managing which sound and when should play. I already talked about this aspect a bit in the past dev blog, when I mentioned coroutines, but now I will describe it in a bit more depth. 

![Screenshot 2024-06-02 185010](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/faee4bed-5afe-48ff-896a-c4a45f2476f1)

First, I created a serializable class “AudioClipInfo”, so that I could access and modify the volume and loop stat for each of the audio clips I was to attach to the audio manager. You can also notice that I created 3 AudioSource variables, that would refer to 3 AudioSource components I added to the AudioManager game object. That is done so that 2 audio clips would play constantly throughout the playthrough (the background melody and the burning fire sound effect for ambience), and the 3rd one would be the “spare” one that would occasionally play the necessary sound effect based on the action of the player. The slider variable would reference the Slider from the main menu, options menu, where the player can modify the master volume of the game. 

![Screenshot 2024-06-02 185040](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/2e7191d8-4bc4-480b-a3c2-3c2c07a627de)
![Screenshot 2024-06-02 181538](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/9ad22501-8ae2-488f-9d8a-c1489260676a)

In short, the methods do really simple things. The Awake() method initializes an instance of the class and calls for playing the 2 background sounds I talked about. The Start() method adds a listener for the slider in the main menu scene. The SetMasterVolume() method adjusts the volume of all 3 audio sources to the provided volume value.SetVolume() is similar to SetMasterVolume(), but the use is different. PlayAudio() plays an one-off audio clip after checking for the conditions. It is used for audio clips that only should play once on the execution of an action, like drinking a potion or swinging the sword. The PlayLoopingAudio() configures an audio source to play a looping audio clip, like the walking audio effect. StopLoopingAudio() just stops the audio source if found playing. 

With this script, I managed to implement pretty decent audio effects. 

Next, let’s talk a bit about optimization. And by “a bit” I really mean a bit, since the project is a 2D game and unless I did some crazy things, it is as optimized as it goes (considering everything I managed to implement). But let’s mention some of the aspects that come down to a good optimization. To manage sprites I used sprite sheets and tilemaps. 

![Screenshot 2024-06-02 183307](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/7921d40a-2437-4352-ad90-327796bb7af6)

Sprite sheets are an overall good practice for performance optimization and reduced load times, as they use way less memory to call on a single texture, and overall asset management. I also found it extremely comfortable to use while creating the sprites themselves in Adobe Photoshop, since it is easy to figure out the sizings of sprites, and also draw animation frames. 

![MainCh_movement_spriteSheet](https://github.com/nanami4yokai/Rise-of-the-Hero/assets/91677999/1ce097e4-7447-44f9-a8ba-b669ee61f6fb)

Another means of managing optimization are some of the points I mentioned in the past dev blog where I talked about game architecture, since good optimization practices are using prefabs and following OOP principles. 
