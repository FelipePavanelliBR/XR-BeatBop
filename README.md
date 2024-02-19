# BeatBop

BeatBop is an XR Rhythm Game that puts your groove to the test. Starting in augmented reality, the game invites you to use music as a weapon to fight the Big Boss Stereo, a futuristic computer virus that traps you in their fully virtual ecosystem.

[Video Demo](https://drive.google.com/file/d/14hkKyZLXe5q0SO3h5PJn13tFGCSJuQ-0/view?usp=sharing)

In this collaborative project with designer Jaden Halevi ‘26. A lot of effort and planning went into finding the right formula that would allow for the combination of rhythm game mechanics and an exciting Boss to be present. Capturing the essence of Rhythm Games was the main priority for this project, and so synchronizing game events to the beats of the background track was the main and most challenging component that gave birth to the gameplay. 

Here are some additional mechanics implemented: 
1) Musical StateMachine: event triggers that sync with a song for changes in dialogue, textures, animation triggers, and scene transitions;
2) Ranking: elaborate score system with multiple types of input (Perfect, Good, OK, Miss), combo scores (sequence of Perfect hits), and multiplier effects. The scoring system also implements data permanence in between scenes for the scoreboard and ranking when a player is back in the AR environment.
3) Modular Level Design: responsive and modularized tools exposed to partners for the collaborative creation of different kinds of levels, dialogues, UI interactions, animations, textures, and spawning of target notes using the Unity Editor.
4) ARVR: XR interactions were implemented using the Unity XR Interaction Toolkit and consisted of object collision with controllers and raycasting for UI interactions. 


The syncing is done via reading and parsing a MIDI file with the DryWetMIDI asset 

