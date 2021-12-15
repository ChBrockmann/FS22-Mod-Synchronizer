using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer.Extensions
{
    public static class ModCollectionExtension
    {
        public static void DebugPrint(this IEnumerable<Mod> mods)
        {
            foreach (Mod mod in mods)
            {
                Console.WriteLine($"{mod.Title} - {mod.Version} - {mod.Url} - {mod.ModType}");
            }
        }
    }
}
