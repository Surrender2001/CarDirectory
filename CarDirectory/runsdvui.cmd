cd /d "C:\Users\msi\Desktop\CarDirectory\CarDirectory" &msbuild "CarDirectory.csproj" /t:sdvViewer /p:configuration="Debug" /p:platform="Any CPU" /p:SolutionDir="C:\Users\msi\Desktop\CarDirectory" 
exit %errorlevel% 