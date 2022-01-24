using LS_Mod_Synchronizer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer.Logic
{
    internal class ConfigFileHelper
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public void CreateExampleFile(string filename)
        {
            if (!File.Exists(Ressources.CONFIG_FILENAME))
            {
                throw new Exception("Your config file does not exist!");
            }

            try
            {
                LocalConfig config = JsonConvert.DeserializeObject<LocalConfig>(File.ReadAllText(Ressources.CONFIG_FILENAME));

                Config.BASE_URL = config.ServerUrl;
                Config.LOCAL_MOD_FOLDER_PATH = config.ModFolderPath;
            }
            catch (Exception e)
            {
                Logger.Info("There is an error loading your config." + e);
            }

        }

        public bool TryLoadConfig()
        {
            return true;
        }

        public bool ValidateConfig(LocalConfig config)
        {
            if(!Directory.Exists(config.ModFolderPath))
                return false;

            return true;
        }
    }
}
