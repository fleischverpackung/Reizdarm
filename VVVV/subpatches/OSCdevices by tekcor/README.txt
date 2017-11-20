OSCdecices by tekcor         |
symbioticcube.com            |
                             |
_____________________________|

Version 1.0

Feedback and bugreports to davidgann@symbioticcube.com

________________________

This is a set of VVVV modules / Max for Live devices cominations. Each module/device is ment to run as a pair.

The pack can be used to integrate VVVV generative animation capabilities into sound-production or it can be utilized as a sound-engine for VVVV applications like games or interactive installations.  

It runs over network so it is possible to distribute the applications over several computers.

These four set of modules/devices exist:

- OSC_ClipIn (m4l receive).v4p / OSC_ClipOut.amxd
Receives the data of a clip / sends data of a playing clip in specified track 

- OSC_TransportIn (m4l receive).v4p / OSC_TransportOut.amxd
Receives the transport sync from Ableton and produces synced bangs / sends the transport sync from Ableton
 
- OSC_MidiOut (m4l send).v4p / OSC_MidiIn.amxd
Send Midi notes over the network / receives the midi notes

- OSC_Out (m4l send).v4p / OSC_In.amxd
Send up to 8 parameter over the network / receive 8 parameter and map them to any device paramter in the track

________________________

Requirements:

- Ableton Live 8 or higher (Compatibility with Live 9 not tested)
- Max for Live
- VVVV (vvvv45_beta31 recommended) + addonpack

________________________

License:

The patches are open source and can be used according to the VVVV, Max for Live and Ableton Live licensing.

________________________

Installation:

The devices run in a portable Project as set up in the example project.

For using it in your library:

Put the content of /m4l.OSCdevices into your Live Library/Presets folder

Put the content of VVVV.OSCdevices into the /modules folder of your vvvv installationq

