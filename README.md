### Description
The purpose of this task is to test your general knowledge of C#, WPF, Git/GitHub and OOP. Furthermore, it tests your knowledge level of [RhinoCommon SDK](https://developer.rhino3d.com/guides/rhinocommon/what-is-rhinocommon/) or your learning skills and ability to understand documentation.

1 Fork this project
2 Complete the task
3 Create a pull request to this main project's main branch

### Task
This project contains a Rhino plugin with some util classes and `Command` class. The `CreateLineCommand` asks user to input two points and creates rectangular profile sweep on that line.

![ezgif-5-d696ac642f](https://user-images.githubusercontent.com/86102616/220560063-90ecb6b8-a7c6-46da-ba1f-d2aa210b3e81.gif)

To accomplish this we are using a `RectangularProfile` class to generate the profile and then we are using `ProfileSweep` class to generate the 3d geometry and add it to the document.

Your goal is to create a similar command that generates a rectangular frame with I-beam profile.

<img width="239" alt="image" src="https://user-images.githubusercontent.com/86102616/220562503-e561ad38-f227-4e8a-b75f-92a17930a98a.png">

Expected behavior:
1. Input bottom-left point of rectangle
2. Input top-right point of rectangle
3. Open a simple WPF window where user can set Hight, Width, and Thickness of the profile
4. Klick "OK" and generate geometry

Requirements:
1. Make sure to use the `IProfileGenerator` interface and generate 3d geometry using `ProfileSweep.BakeSweep` method.
2. For the input window use MVVM pattern

### Resources
1. RhinoCommon documentation https://developer.rhino3d.com/api/RhinoCommon/html/R_Project_RhinoCommon.htm
