using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Fetcher;
using LS_Mod_Synchronizer.Logic;
using LS_Mod_Synchronizer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer
{
    public class Synchronizer
    {
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOnlineFetcher _onlineFetcher;
        private readonly ILocalFetcher _localFetcher;
        private readonly IModDownloader _modDownloader;
        private readonly ModComparer _modComparer;

        public Synchronizer(IOnlineFetcher onlineFetcher, ILocalFetcher localFetcher, IModDownloader modDownloader, ModComparer modComparer)
        {
            _onlineFetcher = onlineFetcher;
            _localFetcher = localFetcher;
            _modDownloader = modDownloader;
            _modComparer = modComparer;
        }

        public void Run()
        {
            try
            {
                Logger.Info("LS-Mod-Synchronizer");

                ConfigFileHelper ConfigFileHelper = new ConfigFileHelper();

                LoadConfigOrCreateExampleConfig();


                List<Mod> onlineMods = _onlineFetcher.Fetch(Config.BASE_URL + Ressources.MOD_TABLE_EXTENSION).ToList();
                List<Mod> localMods = _localFetcher.Fetch(Config.LOCAL_MOD_FOLDER_PATH).ToList();


                Logger.Info("Local");
                localMods.DebugPrint();

                Logger.Info("Online");
                onlineMods.DebugPrint();

                List<Mod> allMods = new();
                allMods.AddRange(onlineMods);
                allMods.AddRange(localMods);

                IEnumerable<Mod> modsToDownload = _modComparer.GetListOfAllModsToDownload(localMods, onlineMods);

                if (modsToDownload.Count() > 0)
                {
                    Logger.Info("\nMods to download");
                    modsToDownload.DebugPrint();
                    _modDownloader.DownloadModsAndSaveToPath(modsToDownload, Config.LOCAL_MOD_FOLDER_PATH);
                }

                Logger.Info("\nAll Mods up to date");


                Logger.Info("Press enter to close...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                Logger.Fatal($"Current config. URL: \"{Config.BASE_URL}\"; FOLDER_PATH: \"{Config.LOCAL_MOD_FOLDER_PATH}\"");
            }
        }

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
                    Logger.Info("There is an error in your config." + e);
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

            Logger.Info("No Config found. Created an example configuration!");
            Logger.Info($"Don't forget the tailing '\\' at the end of the {nameof(LocalConfig.ModFolderPath)} and '/' at the end of the {nameof(LocalConfig.ServerUrl)}");

            File.WriteAllText(Ressources.CONFIG_FILENAME, JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented));
            Logger.Info("Press enter to close...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
