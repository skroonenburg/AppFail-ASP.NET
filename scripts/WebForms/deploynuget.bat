:: compile nuget package
call buildnuget.bat

:: copy to dropbox from builds folder
xcopy /Y %DestinationDirectory%*.nupkg %USERPROFILE%\DropBox\AppFail\Nuget\