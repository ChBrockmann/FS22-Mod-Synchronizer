using HtmlAgilityPack;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Model;

namespace LS_Mod_Synchronizer.Fetcher
{
    public class OnlineFetcher
    {
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

            HtmlWeb detailClient = new HtmlWeb();

            foreach (HtmlNode node in childNotes)
            {
                string detailUrl = Config.BASE_URL + node.ChildNodes[Ressources.INDEX_MOD_NAME].InnerHtml.GetStringBetweenTwoDelimeters(Ressources.MOD_DOWNLOAD_URL_DELIMETER);

                try
                {

                    var detailSite = detailClient.Load(detailUrl);
                    HtmlNodeCollection detailNodes = detailSite.DocumentNode.SelectNodes("//table");

                    string detailNodeName = detailNodes[0].ChildNodes[3].ChildNodes[1].InnerText;
                    string detailNodeVersion = detailNodes[0].ChildNodes[5].ChildNodes[1].InnerText;
                    string detailDownloadRelLink = detailNodes[0].ChildNodes[9].ChildNodes[1].ChildNodes[0].Attributes[1].Value;


                    string url = node.ChildNodes[Ressources.INDEX_MOD_URL].InnerHtml;

                    string absUrl = Config.BASE_URL + detailDownloadRelLink;

                    result.Add(new Mod()
                    {
                        Title = detailNodeName,
                        Version = detailNodeVersion,
                        Url = absUrl,
                        ModType = ModType.Online
                    });
                }
                catch (System.Net.WebException e)
                {
                    Console.WriteLine("Error while fetching mod detail " + e.Message);
                    Console.WriteLine("Start the program again to try again");
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            return result;
        }
    }
}
