@echo off
set /p var=Are you sure you want to continue? This will wipe all the data from your devices database? [y/n]: 
if %var%== y goto yes
if not %var%== y exit

:yes
cd ./Services/Netmon.DeviceManager/
echo "Deleting migrations"
DEL /S /Q .\Migrations\.

echo "Resetting database"
dotnet ef database drop --force

echo "Creating migration"
dotnet ef migrations add "Initial_Migration"

echo "Updating database"
dotnet ef database update

echo "Migration completed"
pause