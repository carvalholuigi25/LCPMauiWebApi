@echo off
setlocal enableextensions
set "pthproj=C:\Users\%username%\Documents\projects\all\LCPMaui\LCPMauiWebApi\Server"

REM dotnet add package Microsoft.EntityFrameworkCore.Design
REM dotnet tool install --global dotnet-ef
REM dotnet tool update --global dotnet-ef

REM pcm way:
REM cd %pthproj%
REM Add-Migration InitialCreate
REM Update-Database

cd %pthproj%

dotnet ef migrations remove --context DBContext --force
dotnet ef migrations remove --context ApplicationDbContext --force

dotnet ef migrations add InitialCreate --context DBContext
dotnet ef migrations add InitialCreate --context ApplicationDbContext

dotnet ef database update --context DBContext
dotnet ef database update --context ApplicationDbContext

pause
exit /b %errorlevel%
endlocal