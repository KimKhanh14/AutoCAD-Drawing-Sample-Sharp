# Introduction
 This is a plug-in of AutoCAD software that supports users to put models of .skp files (SketchUp software) into AutoCAD. This project we did while studying Applied Graphics course at VNUHCM-University of Science. The project is written in C# language on Visual Studio 2019 and AutoCAD 2021 software.

# Contributors
This project is carried out by group 3 members, including:
- [Kimkhanh14](https://github.com/KimKhanh14)
- [pnaquoc19](https://github.com/pnaquoc19)
- [NgHoangSon](https://github.com/NgHoangSon)

# How to setup project AutoCAD by Visual Studio
There are 2 ways to create an Add-in programming project for AutoCAD. Method 1 we use directly to install AutoCAD programming support available on Visual Studio. Method 2 will be more complicated, but it is the foundation for plug-in programming for other software.

Step 1:
Create a “New Project” using “Class Library”. Choosing library .Net Framework (newest).

Step 2:
Insert API library of AutoCAD into Visual Studio. Change “Copy local” into “False”.

Step 3:
Change name of Visual’s default class (Pressing “Yes” if asked)
Add the initial code as following:
-	Insert reference library (using keyword)
```sh
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
```
-	Create Test method
```sh
public class Commands
    {
        [CommandMethod("TEST")]

        public void Test()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
            using (var tr = db.TransactionManager.StartTransaction())
            {
                tr.Commit();
            }    
        }
    }
```
Step 4:
Create a script to load the application when starting AutoCAD
-	Create a file of type Text File named "start.scr"
-	Insert the following line of code into the file "start.scr" and save it:
netload “<name of project>.dll”
-	In the Properties section of the "start.scr" file, change the "Copy in the Output Directory" property to "Always Copy"

 Step 5:
Change the MSBuild file (.csproj) to run AutoCAd in Debug mode
-	Find the .csproj file of the current project and open it with notepad
-	Insert the following line of code in the PropertyGroup section (2nd - the Debug section). Change the AutoCAD path if necessary
```sh
<StartAction>Program</StartAction>
<StartProgram>C:\Program Files\Autodesk\AutoCAD 2021\acad.exe</StartProgram>
<StartArguments>/nlogo /b "start.scr"</StartArguments>
```
-	The entire PropertyGroup node will look like this:
```sh
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <StartAction>Program</StartAction>
    <StartProgram>C:\Program Files\Autodesk\AutoCAD 2021\acad.exe</StartProgram>
    <StartArguments>/nlogo /b "start.scr"</StartArguments>
  </PropertyGroup>
```
-	In the ItemGroup node you may need to change the path of the reference files (if necessary)
```sh
<Reference Include="accoremgd">
      <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\AutoCAD 2021\accoremgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="acdbmgd">
      <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\AutoCAD 2021\acdbmgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="acmgd">
      <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\AutoCAD 2021\acmgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
```
-	Save this file again. The above changes will appear in Visual Studio. The Debug tab of the Properties panel

 Step 6:
Export template
-	Với With AutoCAD 2016 and later, the “LEGACYCODESEARCH” variable value must be changed to 1 (done in AutoCAD software)
-	Open the project in Visual Studio and try debugging (F5)
-	Go to menu Project -> Export Template
-	Select Project Template -> Continue
-	Enter the parameters then finish

# Implementation document
The repository contains 2 main folders: Document, Source.

# Acknowledgments
We gratefully thank the below open-source repo, which greatly boost our research.
- [eRSVN](https://www.youtube.com/playlist?list=PLcUq_rc1Vk1LZ4QyasX-2zVr6O56Ouzc8)

# License
- Simple software for learning purposes
- Not for any business reason
