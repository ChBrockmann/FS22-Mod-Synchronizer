using LS_Mod_Synchronizer.Model;
using System.Collections.Generic;

namespace LS_Mod_Synchronizer.Logic
{
    public interface IModDownloader
    {
        void DownloadModsAndSaveToPath(IEnumerable<Mod> mods, string Path);
    }
}
