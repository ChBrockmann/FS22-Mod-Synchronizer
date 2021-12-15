namespace LS_Mod_Synchronizer.Model
{
    public class Config
    {
        //Example: http://23.109.253.164:8160/
        public static string BASE_URL = @"";
        //Example: C:\Users\brock\Documents\My Games\FarmingSimulator2022\mods\
        public static string LOCAL_MOD_FOLDER_PATH;
    }

    public class LocalConfig
    {
        public string ServerUrl { get; set; }
        public string ModFolderPath { get; set; }
    }
}
