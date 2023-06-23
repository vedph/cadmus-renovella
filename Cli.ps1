# build
dotnet build -c Debug
# publish library
Remove-Item -Path .\Cadmus.Renovella.Services\bin\Debug\net7.0\publish -Recurse -Force
dotnet publish .\Cadmus.Renovella.Services\Cadmus.Renovella.Services.csproj -c Debug
# rename publish to Cadmus.Renovella.Services and zip
Rename-Item -Path .\Cadmus.Renovella.Services\bin\Debug\net7.0\publish -NewName Cadmus.Renovella.Services
compress-archive -path .\Cadmus.Renovella.Services\bin\Debug\net7.0\Cadmus.Renovella.Services\ -DestinationPath .\Cadmus.Renovella.Services\bin\Debug\net7.0\Cadmus.Renovella.Services.zip -Force
# rename back
Rename-Item -Path .\Cadmus.Renovella.Services\bin\Debug\net7.0\Cadmus.Renovella.Services -NewName publish
