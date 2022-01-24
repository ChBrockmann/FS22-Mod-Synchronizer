using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace LS_Mod_Synchronizer;

public class ModDownloader
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly string Path;

    public ModDownloader(string Path)
    {
        this.Path = Path;
    }

    public void DownloadForceOverride(IEnumerable<Mod> mods)
    {
        foreach (var mod in mods)
        {
            Logger.Info($"Downloading {mod.Title}");
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(mod.Url, Path + "\\" + mod.Url.Replace($"{Config.BASE_URL}mods/", ""));
                }
                catch (System.Net.WebException e)
                {
                    Logger.Info($"Error downloading {mod.Title} " + e.Message);
                    Logger.Info("Start the program again to try again");
                    Logger.Info("Press enter to close...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
    }

    private void TryDelete(string filepath)
    {
        try
        {
            File.Delete(filepath);
        }
        catch (Exception) { }
    }
}

