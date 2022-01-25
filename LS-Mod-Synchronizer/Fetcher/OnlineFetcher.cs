using HtmlAgilityPack;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LS_Mod_Synchronizer.Fetcher
{
    public class OnlineFetcher
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string Url;

        public OnlineFetcher(string Url)
        {
            this.Url = Url;
        }

        public ICollection<Mod> Fetch()
        {
            List<Mod> result = new List<Mod>();

            HtmlWeb web = new HtmlWeb();

            var htmlSite = web.Load(Url);

            HtmlNodeCollection nodes = htmlSite.DocumentNode.SelectNodes("//table");

            IEnumerable<HtmlNode> childNotes = nodes[0].ChildNodes
                .Where(n => n.Name == "tr")
                .Skip(1) //First one is table Head
                .SkipLast(1); //Skip the last element

            foreach (HtmlNode node in childNotes)
            {
                try
                {
                    string modName = node.ChildNodes[Ressources.INDEX_MOD_NAME].InnerText;
                    string modVersion = node.ChildNodes[Ressources.INDEX_MOD_VERSION].Attributes.First(a => a.Name == "title").Value;
                    string modDownloadLink = node.ChildNodes[15].ChildNodes[0].Attributes.First(a => a.Name == "href").Value;

                    string absUrl = Config.BASE_URL + modDownloadLink;

                    result.Add(new Mod()
                    {
                        Title = modName,
                        Version = modVersion,
                        Url = absUrl,
                        ModType = ModType.Online
                    });
                }
                catch(Exception e)
                {
                    Logger.Error($"Error trying to fetch information for mod.");
                    Logger.Error(e);
                }
            }
            return result;
        }
    }
}
