# Unity-VR-Framework
Starting point with project settings set up, for working with new 2019 Unity XR Features

Basically, an almost empty project you can clone. Uses 2019.3 XR management features, but "legacy" (not "new") input system.

## Working with Audio.

### General
Spatial Blend is the property of the "Audio Source" component we want to spend the most attention on. In VR, we want things to be spatialized properly, so we will usually set this property to one. For voice overs, this should be zero - the audio is non-diagetic. For background music, I think it's nice to set this to a blended (not 0 or 1) property, and have the source be at a speaker in the room. You can still hear it from anywhere, but it's louder near the speaker.

### Collision Audio Handler
The "CollisionAudioHandler" component should be attached to a GameObject that has both a rigidbody and an audio source. The audio source should have its "spatial blend" property set more-or-less all the way to 1. Give the array of source clips 1 or more source files to randomly pick between on collision.

I will, in the future, fix up the code so the volume changes on impact, and make that easier to adjust.

### PlayOnce Audio Handler
The "PlayOnceAudioHandler" component can be used as an interface to an audio source component, and it will keep track of being played and wont let the user play it a second time.

Next Step: Auto Reset when the clip is done playing (so they can't spam restart the clip by grabbing an object), and auto-reset after a short amount of time.

### ToDo
An component where you can give the audio an "audio group" and it will stop playing any audio source that has is with that group when you start a new one, but won't affect audio grouped elsewhere.

