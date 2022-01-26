using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace LS_Mod_Synchronizer.Logic;

public class ModDownloader : IModDownloader
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public void DownloadModsAndSaveToPath(IEnumerable<Mod> mods, string Path)
    {
        foreach (var mod in mods)
        {
            Logger.Info($"Downloading {mod.Title}");
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(mod.Url, Path + "\\" + mod.Url.Replace($"{Config.BASE_URL}mods/", ""));
                    Thread.Sleep(1000);
                }
                catch (System.Net.WebException e)
                {
                    Logger.Info($"Error downloading {mod.Title} ");
                }
            }
        }
    }
}

