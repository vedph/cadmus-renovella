@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Renovella.Parts\bin\Debug\Cadmus.Renovella.Parts.7.0.1.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Renovella.Services\bin\Debug\Cadmus.Renovella.Services.7.0.1.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Renovella.Parts\bin\Debug\Cadmus.Seed.Renovella.Parts.7.0.1.nupkg -source C:\Projects\_NuGet
pause
