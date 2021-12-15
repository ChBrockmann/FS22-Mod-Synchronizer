using LS_Mod_Synchronizer.Model;

namespace LS_Mod_Synchronizer
{
    public static class Ressources
    {
        public static readonly string CONFIG_FILENAME = "config.json";

        //Online
        public static readonly string MOD_TABLE_EXTENSION = @"mods.html?lang=en";
        public static readonly string MOD_DOWNLOAD_URL_DELIMETER = "\"";

        //Indices of table
        public static readonly int INDEX_MOD_NAME = 1;
        public static readonly int INDEX_MOD_VERSION = 3;
        public static readonly int INDEX_MOD_URL = 7;

        //Local
        
        public static readonly string LOCAL_MOD_META_FILENAME = "modDesc.xml";
        public static readonly string LOCAL_TMP_DIRECTORY = Config.LOCAL_MOD_FOLDER_PATH + @"tmp\";

    }
}
