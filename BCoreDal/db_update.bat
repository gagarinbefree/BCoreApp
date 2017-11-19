rd /s /q "Migrations"

dnx ef migrations remove
dnx ef migrations add inital
dnx ef database update 
pause >nul