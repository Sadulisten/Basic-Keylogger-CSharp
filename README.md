# Basic-Keylogger-C-
A basic project showing how to compile pre-generated code into an executable on runtime. More info in readme.

This uses CODEDOM to compile C# code on the fly.
The code this project compiles can be found in the projects resources (in a file called 'source.txt')

Features:
  * Automatically get the current active window (hooking different apis) so that keystrokes in logs are synchronized to the correct windowname/process name
  * Keyboard hook, to recieve all pressed keys globaly

WARNING: Might show up as a virus by your antivirus.
This is due to this projects ability to send stuff via EMAIL (thats how this transmits captured logs to a host computer at a set interval)

