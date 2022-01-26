using LS_Mod_Synchronizer.Model;
using System.Collections.Generic;

namespace LS_Mod_Synchronizer.Fetcher
{
    public interface IOnlineFetcher
    {
        ICollection<Mod> Fetch(string Url);
    }
}
