using LS_Mod_Synchronizer.Model;
using System.Collections.Generic;
using System.Linq;

namespace LS_Mod_Synchronizer.Logic
{
    public class ModComparer
    {
        public IEnumerable<Mod> GetListOfAllModsToDownload(IEnumerable<Mod> localMods, IEnumerable<Mod> onlineMods)
        {
            onlineMods = onlineMods.OrderBy(m => m.Title);
            localMods = localMods.OrderBy(m => m.Title);

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
