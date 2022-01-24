using LS_Mod_Synchronizer;
using Newtonsoft.Json;
using LS_Mod_Synchronizer.Model;
using LS_Mod_Synchronizer.Fetcher;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Logic;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace LS_Mod_Synchronizer;

public class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    
    public static void Main(string[] args)
    {
        try
        {
            Logger.Info("LS-Mod-Synchronizer");

            ConfigFileHelper ConfigFileHelper = new ConfigFileHelper();

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
                Logger.Info("\nLocal");
                localMods.DebugPrint();
            }
            else
            {
                Logger.Info("No local mods found");
            }

            if (onlineMods.Count() > 0)
            {
                Logger.Info("\nOnline");
                onlineMods.DebugPrint();
            }
            else
            {
                Logger.Info("No online mods found");
            }
            localMods.AddRange(onlineMods);

            IEnumerable<Mod> modsToDownload = comparer.GetListOfAllModsToDownload(localMods);

            if (modsToDownload.Count() > 0)
            {
                Logger.Info("\nMods to download");
                modsToDownload.DebugPrint();
                downloader.DownloadForceOverride(modsToDownload);
            }

            Logger.Info("\nAll Mods up to date");


            Logger.Info("Press enter to close...");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            
        }
    }

    static void LoadConfigOrCreateExampleConfig()
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
                Logger.Info("There is an error in your config." + e);
                CreateSample();
            }
        }
        else
        {
            CreateSample();
        }
    }

    static void CreateSample()
    {
        LocalConfig config = new LocalConfig();
        config.ModFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Games\\FarmingSimulator2022\\mods\\".Replace(@"\\", @"\");
        config.ServerUrl = $"http://Your-IP-Adress:Port/";

        Logger.Info("No Config found. Created an example configuration!");
        Logger.Info($"Don't forget the tailing '\\' at the end of the {nameof(LocalConfig.ModFolderPath)} and '/' at the end of the {nameof(LocalConfig.ServerUrl)}");

        File.WriteAllText(Ressources.CONFIG_FILENAME, JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented));
        Logger.Info("Press enter to close...");
        Console.ReadLine();
        Environment.Exit(0);
    }
}