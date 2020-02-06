# Unity-VR-Framework
Starting point with project settings set up, for working with new 2019 Unity XR Features

Basically, an almost empty project you can clone. Uses 2019.3 XR management features, but "legacy" (not "new") input system.
## Working with Rooms.
This toolkit has two ways to control rooms, scenes and gameObjects.

Scenes keeps all objects in one "room" in their own scene, that scene should not have a camera (maybe?) but otherwise is just all the objects. A sceneController lets you cycle through various scenes in a list.

A similar approach is implemented for GameObjects, which enabled/disabled a parent gameObject in a list, but only one scene exists.

Multi-scene editing can be tricky and slightly annoying to work with, but tends to work better for version control systems as people working on seperate parts of the project can work on independent scenes. Remember you can just load up all the scenes in the inspector at the same time to line things up.

Single scene editing is more convenient, but is more annoying to collaboratively work with, as basically only one person can make changes to a scene at a time.

## Working with Audio.

### General
Add an AudioSource (Unity) and an AudioSourceInterface (us) to something you want to play audio.

You can create, in the assets, an AudioGroup, and assign it in the interface. Groups will stop other audio sources in the same group when playing one audioSource.

Spatial Blend is the property of the "Audio Source" component we want to spend the most attention on. In VR, we want things to be spatialized properly, so we will usually set this property to one. For voice overs, this should be zero - the audio is non-diagetic. For background music, I think it's nice to set this to a blended (not 0 or 1) property, and have the source be at a speaker in the room. You can still hear it from anywhere, but it's louder near the speaker.

### Collision Audio Handler
The "CollisionAudioHandler" component should be attached to a GameObject that has both a rigidbody and an audio source. The audio source should have its "spatial blend" property set more-or-less all the way to 1. Give the array of source clips 1 or more source files to randomly pick between on collision.

I will, in the future, fix up the code so the volume changes on impact, and make that easier to adjust.

### PlayOnce Audio Handler
The "PlayOnceAudioHandler" component can be used as an interface to an audio source component, and it will keep track of being played and wont let the user play it a second time.

Clip can be set to be played only when the clip is done playing (so if it gets interupted, you can go back and listen to the clip).

Next is to auto-reset after a defined amount of time, to listen again, and some spam-prevention measures.

### Playlist audio Handler
A list of audio clips, will loop through them. Options to go to a new random one each time and to start at a random one.
