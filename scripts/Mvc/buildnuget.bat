echo OFF

:: directories
set DestinationDirectory=..\..\build\
set SourceDirectory=source

:: set commands
set MakeDirectoryCommand=mkdir
set DeleteDirectoryCommand=rmdir /s /q
set CopyFilesCommand=copy /y

:: delete existing build files and directories
IF EXIST %SourceDirectory% %DeleteDirectoryCommand% %SourceDirectory%

:: copy the files to the source directory
set SourceLocation=..\..\src\AppFail.Mvc\bin\Release\
set SourceFile=AppFailReporting.Mvc.dll

IF NOT EXIST %SourceLocation%%SourceFile% (
	echo Source files were not found. Did you compile in release?
	pause
	GOTO :EOF
) 

:: create the directory for future files
%MakeDirectoryCommand% %SourceDirectory%

%CopyFilesCommand% %SourceLocation%%SourceFile% %SourceDirectory%

:: copy the manifest
set ManifestLocation=..\..\nuspec\Mvc\
set ManifestFile=AppFail.Mvc.nuspec
set WebConfigTransformFile=web.config.transform
set ReadmeFile=readme.txt

%CopyFilesCommand% %ManifestLocation%%WebConfigTransformFile% %SourceDirectory%
%CopyFilesCommand% %ManifestLocation%%ReadmeFile% %SourceDirectory%

:: set the nuget file location
set NugetLocation=..\..\tools\nuget\
set NugetFileCommand=NuGet.exe pack

IF NOT EXIST %DestinationDirectory% %MakeDirectoryCommand% %DestinationDirectory%
 

:: build the package
%NugetLocation%%NugetFileCommand% %ManifestLocation%%ManifestFile% -BasePath %SourceDirectory% -Verbose -OutputDirectory %DestinationDirectory%  

:: delete source directory
%DeleteDirectoryCommand% %SourceDirectory%






