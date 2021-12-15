# FS22-Mod-Synchronizer
A tool for synchronizing the mods from a Farming Simulator 22 server to your local pc. No FTP-access is needed. The application will download any mod to your local mod folder that is not already present or is present in a different version. Local mods with other versions than on the server will be overwritten All mods in your local folder that are not on the server remain unchanged.

## How execute
If you don't want to build the application yourself, you can use the provided application from the "Releases" Tab:

<details>
<summary>English version</summary>
<p>

1. Download and unzip the latest version of the `FS22-Mod-Synchronizer`. The application does not need to be installed.
2. Use a console of your choice (e.g. cmd, Windows Power Shell, Windows Terminal) and navigate to the directory of the unzipped file.
3. Run `LS-Mod-Synchronizer` with the `./LS-Mod-Synchronizer.exe` command.
4. When first running the application, there will be no config file. This is stated by `"No Config found. Created an example configuration!"`
5. Close the application, and navigate to `config.json` in the directory of the uzipped file. Open the `config.json` with an editor of your choice.
6. Change the ServerUrl to the IP and Port of your Farming Simulator 22 server. **It is important that the format matches!** If your IP is `12.34.56.78:9999`, config needs to be `http://12.34.56.78:9999/`, **including `http://` at the beginning and `/` at the end!**
7. ModFolderPath should be preconfigured correctly. If the displayed path is not the path to your mods-Folder, change the path accordingly. **It is important to use double `\\` as seperators! Singe `\` will corrupt the configuration** 
8. Safe the `config.json` file.
9. Use a console to run `LS-Mod-Synchronizer.exe` again. The application will now download all missing mods or mods with different version to your local mods folder.

</p>
</details>

<details>
<summary>German version</summary>
<p>

1. Lade dir die neuste Version des `FS22-Mod-Synchronizers` herunter und entpacke die ZIP-Datei. Die Anwendung muss nicht installiert werden.
2. Nutze eine Konsole deiner Wahl (z.B. cmd, Windows Power Shell, Windows Terminal) und navigiere zum Verzeichnis der entpackten Datei.
3. Führe den `LS-Mod-Synchronizer` mit dem Befehl `./LS-Mod-Synchronizer.exe` aus.
4. Beim ersten Start des Programmes existiert noch keine Konfigdatei. Dies wird durch `"No Config found. Created an example configuration!"` angezeigt.
5. Schließe das Programm und navigiere zur `config.json` Datei in Ordner der entpackten ZIP-Datei. Öffne die `config.json` mit einem Editor deiner Wahl.
6. Ändere die ServerURL zur IP und Port deines Farming Simulator 22 servers. **Es ist wichtig dass das Format übereinstimmt!** Wenn deine IP `12.34.56.78:9999` ist, muss in der Konfiguration `http://12.34.56.78:9999/` eingetragen werden, **wichtig ist das `http://` am Anfang und `/` am Ende!**
7. Der ModFolderPath sollte bereits richtig eingestellt sein. Ist der angezeigte Pfad nicht der deines Mod-Ordners, ändere den Pfad darauf ab. **Es ist wichtig, dass die Ordner mit einem doppelten `\\` getrennt werden. Einfache `\` beschädigen die Konfiguration!**
8.  ModFolderPath should be preconfigured correctly. If the displayed path is not the path to your mods-Folder, change the path accordingly. **It is important to use double `\\` as seperators! Singe `\` will corrupt the configuration** 
9. Speichere die `config.json` Datei ab.
10. Nutze eine Konsole um den `LS-Mod-Synchronizer.exe` erneut auszuführen. Das Programm wird jetzt alle fehlenden Mods und Mods mit einer anderen Version vom Server in deinen Mods-Ordner herunterladen. 

</p>
</details>


## How to build 

**You need to have the Dotnet Core SDK version 6.0 installed!**
1. Clone this repository
2. Run `dotnet restore`
3. Run `dotnet run`

## Issues
Feel free to open an issue to any problem that you might encounter. But please check if it might already have been answered!
Also open an issue for all feature request you might have!

## Contributers
Developed & Co-Developed by 
 - [ChBrockmann](https://github.com/ChBrockmann)
 - [18Nils00](https://github.com/18nils00)
 - [MaKas005](https://github.com/MaKas005)

## Licenses
We are realising this project under the MIT license. Look in the [README](README.md) file to get all the details!
We are using following libraries with licenses:
|Library|License|
|---|---|
|Newtonsoft.Json|[MIT](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)
|HTML-Agility-Pack|[MIT](https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE)
|DOTNET ZIP|[Microsoft Public License](https://github.com/haf/DotNetZip.Semverd/blob/master/LICENSE)|