using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;

namespace LS_Mod_Synchronizer.Extensions
{
    public static class ModCollectionExtension
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static void DebugPrint(this IEnumerable<Mod> mods)
        {
            foreach (Mod mod in mods)
            {
                Logger.Info($"{mod.Title} - {mod.Version} - {mod.Url} - {mod.ModType}");
            }
        }
    }
}
