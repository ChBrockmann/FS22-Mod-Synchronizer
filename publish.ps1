rm '.\FS22-Mod-Synchronizer - Application\' -R
dotnet publish LS-Mod-Synchronizer -o "FS22-Mod-Synchronizer - Application"
Compress-Archive -Path '.\FS22-Mod-Synchronizer - Application\*' -Destination "FS22-Mod-Synchronizer - Application" -Force