using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer.Web
{
    public class HtmlWebLoaderProxy : IHtmlWebLoader
    {
        public HtmlDocument Load(string url)
        {
            return new HtmlWeb().Load(url);
        }
    }
}
