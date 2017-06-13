rd /s /q "Migrations"

dotnet ef migrations remove
dotnet ef migrations add inital
dotnet ef database update 
pause >nul