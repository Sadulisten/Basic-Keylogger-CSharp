# Basic-Keylogger-C-
A basic project showing how to compile pre-generated code into an executable on runtime. More info in readme.

![alt text](https://i.imgur.com/JDi71fo.png)

This uses CODEDOM to compile C# code on the fly.
The code this project compiles can be found in the projects resources (in a file called 'source.txt')

Features:
  * Automatically get the current active window (hooking different apis) so that keystrokes in logs are synchronized to the correct windowname/process name
  * Keyboard hook, to recieve all pressed keys globaly
  * Abillity to build executable with 'debugmode' which means you could test the functionality of your generated file without it trying to upload logs via SMTP + it prints out everything neatly in the console.
  
  * Compiling with 'readybuild' will compile the executable without 'debugmode' meaning it will attempt to upload logs at the set interval and not print out all keystrokes pressed/windows fetched + it will compile so that the console window is hidden on startup.

WARNING: Might show up as a virus by your antivirus.
This is due to this projects ability to send stuff via EMAIL (thats how this transmits captured logs to a host computer at a set interval) and ofcourse its abillity to capture all keypresses (lol)

I DO NOT TAKE RESPONSIBILITY FOR HOW THIS IS USED!
THE PURPOSE OF THIS WAS SIMPLY TO SHOW HOW ITS DONE AND HOW IT WORKS.

