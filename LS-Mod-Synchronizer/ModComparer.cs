using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer
{
    public class ModComparer
    {
        public IEnumerable<Mod> GetListOfAllModsToDownload(IEnumerable<Mod> modList)
        {
            IEnumerable<Mod> onlineMods = modList.Where(m => m.ModType == ModType.Online).OrderBy(m => m.Title);
            IEnumerable<Mod> localMods = modList.Where(m => m.ModType == ModType.Local).OrderBy(m => m.Title);
            List<Mod> result = new List<Mod>();

            foreach(Mod onlineMod in onlineMods)
            {
                if(default == localMods.FirstOrDefault(localMod => localMod.Title == onlineMod.Title && localMod.Version == onlineMod.Version))
                {
                    result.Add(onlineMod);
                }
            }

            return result;
        }
    }
}
