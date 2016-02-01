@echo off
setlocal

:: Restore NuGet
set "nugetdir=%~dp0bin"
set "nuget=%nugetdir%\nuget.exe"
:: Use v3 of the command line util, since v2 is still unable to work with PCLs
set "nugeturl=http://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

if not exist "%nuget%" (
    echo Restoring NuGet...
    if not exist "%nugetdir%" mkdir "%nugetdir%"
    powershell "iwr %nugeturl% -OutFile %nuget%"
)

:: Build the solution
call "%~dp0build.cmd" -p "Any CPU" -c Release

:: Create the packages
cd "%~dp0src"
for /d %%d in (*) do (
    cd "%%d"
    del /q *.nupkg > nul 2>&1
    "%nuget%" pack %%d.nuspec
    "%nuget%" push *.nupkg
    del /q *.nupkg > nul
    cd "%~dp0src"
)
