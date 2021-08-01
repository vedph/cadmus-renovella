@echo off
echo BUILD Cadmus Renovella Packages
del .\Cadmus.Renovella.Parts\bin\Debug\*.snupkg
del .\Cadmus.Renovella.Parts\bin\Debug\*.nupkg

del .\Cadmus.Renovella.Services\bin\Debug\*.snupkg
del .\Cadmus.Renovella.Services\bin\Debug\*.nupkg

del .\Cadmus.Seed.Renovella.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Renovella.Parts\bin\Debug\*.nupkg

cd .\Cadmus.Renovella.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Renovella.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Renovella.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
