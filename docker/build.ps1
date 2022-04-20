Set-Location $PSScriptRoot/../src/
dotnet restore
dotnet publish -c Release -o ../out