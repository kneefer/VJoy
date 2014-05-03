@ECHO OFF

SET mypath=%~dp0
REM the following is for .NET v4.5
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo Installing MyService...
echo ---------------------------------------------------
InstallUtil "%mypath%EgzotechVJoyService.exe"
echo ---------------------------------------------------
echo Done.
pause