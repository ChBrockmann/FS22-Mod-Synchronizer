using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Mod_Synchronizer.Web
{
    public interface IHtmlWebLoader
    {
        HtmlDocument Load(string url);
    }
}
