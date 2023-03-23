# dots-unity-example
A project in C# that explores Unity's new data-oriented technology stack (DOTS).

See "Assets/Scripts" for code.

# Why Make this Project?
Multi-threading can be challenging, and exploring Unity's DOTS system (which is still in pre-release and therefore does not have the best documentation) is a great challenge that showcases my abilities as a developer. 

With DOTS, it's possible to easily 100x the performance of a game -- because typical Unity projects rely on single-threading do not utilize the client's full, multi-core computing power. 

# What does this code do?
By implementing ISystem, this code spawns thousands of "army units" that move in formation until they are near an "enemy" army. Although it is possible to do this in a traditional monobehavior, the performance of thousands and thousands of moving units would not be robust enough. 

Over time, I intend to continue to evolve this project and simulate many thousands of entities. The goal is to fully explore Unity's DOT stack and showcase its capabilities at scale with a large, fairly complex simulation. 

# References
- https://github.com/Unity-Technologies/EntityComponentSystemSamples
