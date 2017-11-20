@echo off

echo starting Ableton Live
START %~dp0Ableton_ExampleProject\OSCdevices_by_tekcor_Project\OSCdevices_by_tekcor.als
timeout 5
echo starting vvvv
START %~dp0VVVV.OSCdevices\_Root_OSCdevices.v4p
timeout 5


timeout 30

