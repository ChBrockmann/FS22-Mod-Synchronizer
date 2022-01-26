using LS_Mod_Synchronizer.Model;
using System.Collections.Generic;

namespace LS_Mod_Synchronizer.Fetcher
{
    public interface ILocalFetcher
    {
        ICollection<Mod> Fetch(string Path);
    }
}
