using HtmlAgilityPack;
using LS_Mod_Synchronizer;
using System.Net;
using Newtonsoft.Json;
using System.IO.Compression;
using Ionic.Zip;
using ZipFile = Ionic.Zip.ZipFile;
using System.Xml;
using LS_Mod_Synchronizer.Model;
using LS_Mod_Synchronizer.Fetcher;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Logic;

Console.WriteLine("LS-Mod-Synchronizer");

LoadConfigOrCreateExampleConfig();

List<Mod> onlineMods = new List<Mod>();
List<Mod> localMods = new List<Mod>();

OnlineFetcher onlineFetcher = new OnlineFetcher(Config.BASE_URL + Ressources.MOD_TABLE_EXTENSION);
LocalFetcher localFetcher = new LocalFetcher(Config.LOCAL_MOD_FOLDER_PATH);
ModComparer comparer = new ModComparer();
ModDownloader downloader = new ModDownloader(Config.LOCAL_MOD_FOLDER_PATH);

onlineMods = onlineFetcher.Fetch().ToList();

localMods = localFetcher.Fetch().ToList();

if (localMods.Count > 0)
{
    Console.WriteLine("\nLocal");
    localMods.DebugPrint();
}
else
{
    Console.WriteLine("No local mods found");
}

if (onlineMods.Count() > 0)
{
    Console.WriteLine("\nOnline");
    onlineMods.DebugPrint();
}
else
{
    Console.WriteLine("No online mods found");
}
localMods.AddRange(onlineMods);

IEnumerable<Mod> modsToDownload = comparer.GetListOfAllModsToDownload(localMods);

if (modsToDownload.Count() > 0)
{
    Console.WriteLine("\nMods to download");
    modsToDownload.DebugPrint();
    downloader.DownloadForceOverride(modsToDownload);
}

Console.WriteLine("\nAll Mods up to date");


Console.WriteLine("Press enter to close...");
Console.ReadLine();

void LoadConfigOrCreateExampleConfig()
{
    if (File.Exists(Ressources.CONFIG_FILENAME))
    {
        try
        {
            LocalConfig config = JsonConvert.DeserializeObject<LocalConfig>(File.ReadAllText(Ressources.CONFIG_FILENAME));

            Config.BASE_URL = config.ServerUrl;
            Config.LOCAL_MOD_FOLDER_PATH = config.ModFolderPath;
        }
        catch (Exception e)
        {
            Console.WriteLine("There is an error in your config." + e);
            CreateSample();
        }
    }
    else
    {
        CreateSample();
    }
}

void CreateSample()
{
    LocalConfig config = new LocalConfig();
    config.ModFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Games\\FarmingSimulator2022\\mods\\".Replace(@"\\", @"\");
    config.ServerUrl = $"http://Your-IP-Adress:Port/";

    Console.WriteLine("No Config found. Created an example configuration!");
    Console.WriteLine($"Don't forget the tailing '\\' at the end of the {nameof(LocalConfig.ModFolderPath)} and '/' at the end of the {nameof(LocalConfig.ServerUrl)}");

    File.WriteAllText(Ressources.CONFIG_FILENAME, JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented));
    Console.WriteLine("Press enter to close...");
    Console.ReadLine();
    Environment.Exit(0);
}