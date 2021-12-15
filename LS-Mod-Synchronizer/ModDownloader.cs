using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer;

public class ModDownloader
{
    private readonly string Path;

    public ModDownloader(string Path)
    {
        this.Path = Path;
    }

    public void DownloadForceOverride(IEnumerable<Mod> mods)
    {
        foreach (var mod in mods)
        {
            Console.WriteLine($"Downloading {mod.Title}");
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(mod.Url, Path + "\\" + mod.Url.Replace($"{Config.BASE_URL}mods/", ""));
                }
                catch (System.Net.WebException e)
                {
                    Console.WriteLine($"Error downloading {mod.Title} " + e.Message);
                    Console.WriteLine("Start the program again to try again");
                    Console.WriteLine("Press enter to close...");
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

