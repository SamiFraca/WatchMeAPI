Be sure you have sdfk NET 6.0 installed.
install sdfk NET 6.0
https://dotnet.microsoft.com/en-us/download/dotnet/6.0


docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=AVeryComplex(!)123Password" -p 6126:1433 -d mcr.microsoft.com/mssql/server:2022-latest

Add tools for dotnet ef core:
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialMigration
dotnet ef database update


How to connect database to azure data studio:
Server: localhost,1433 -> Be aware of the coma.
Username & Password: See appsettings.json file. (ConnectionString)

efcore include select examples
entityframeworktutorial

if database location from localhost to docker wants to be changed, connectionstring needs to be changed from "Server=localhost,6126" to "Server=sqlserver" 
so it can aim to the api server
Might need to comment line 26 to 36 on Program.cs, exluding the "var app = builder.Build();" line.

SIMPLE WAY TO START TO REPO WITH DOCKER COMPOSE

docker-compose build
docker-compose up