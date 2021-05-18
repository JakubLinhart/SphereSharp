msbuild ..\SphereSharp.sln /t:Clean /p:Configuration=Release
msbuild ..\SphereSharp.sln /p:Configuration=Release

rmdir _ /s /q
mkdir _
mkdir _\SphereSharp.Cli
copy ..\src\SphereSharp.Cli\bin\Release\*.* _\SphereSharp.Cli

cd paket
paket.exe restore
cd ..

paket\packages\NuGet.CommandLine\tools\nuget.exe pack spheresharp.cli.nuspec -OutputDirectory .\_