# CytenaStopWatch
Coding Challenge for interview on 22.10.

# How to run
To use the stopwatch the server needs to run and the (multiple) clients need to run on the same machine since currently 
only localhost can be used as address.
## PreBuilt
* Use the *StopwatchServer.exe* in *\executables\server* to start the server
* Use the *StopwatchClient.exe* in *\executables\client* to start as many stopwatch instances as you like.


## Build yourself
* Open *StopWatchService\Stopwatch.sln* with visual studio (I used VS19)
* Build the entire *Stopwatch* solution
* right click solution in VS -> *Set Startup Projects...* 
* In *Common Properties* click *Startup Project* select *Multiple Startup Projects*
* Select *GrpcServer* with action *Start* as first in order and *GrpcClientUi* with action *Start* below it.
* When clicking start now both the UI and the server should start.

# Coding Challenge

You shall develop a stop-watch client server solution satisfying the following requirements:

* The backend maintains a single stop watch instance
* Multiple clients can connect to the backend and start or stop the stopwatch.
* Every client is displaying the current stopwatch timer (One timer for all clients) and the current state (running, stopped)
* Client and server shall use REST, grpc or a comparable protocol for communication. Explain your choice (Experience is also a reason ;)
* The backend service shall be developed with C# .NET Core
* The client can be developed in any technology (C# Desktop, Browser based ....)
* Document how to run and demonstrate your solution in a readme file.
* Commit your code to a git repository and make it available to us
* You have one week to complete your task

Do not focus on UI. It is not important to present a nice UI. It's about software design, clean code and architecture. Think about testability. How would you test your solution. Be prepared to explain your solution.
