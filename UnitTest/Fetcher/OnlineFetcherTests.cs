using FluentAssertions;
using HtmlAgilityPack;
using LS_Mod_Synchronizer.Fetcher;
using LS_Mod_Synchronizer.Model;
using LS_Mod_Synchronizer.Web;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Fetcher
{
    public class OnlineFetcherTests
    {
        private OnlineFetcher onlineFetcher;
        private IHtmlWebLoader webLoader = Substitute.For<IHtmlWebLoader>();

        public OnlineFetcherTests()
        {
            onlineFetcher = new OnlineFetcher(webLoader);
        }

        [Fact]
        public void TestCase_1()
        {
            //Arrange
            string baseUrl = string.Empty;
            
            HtmlDocument mockedResponse = new HtmlDocument();
            mockedResponse.LoadHtml("<!DOCTYPE html>\n\n\n<html><head>\n    <title>Farming Simulator Dedicated Server | Mods</title>\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n\t   <script type=\"text/javascript\">\n\t\t\tvar areActiveModsSelected = false;\n\t\t\tvar areInactiveModsSelected = false;\n\t\t\tfunction SetActiveSelection()\n\t\t\t{\n\t\t\t\tvar checkboxes = document.ActiveMods.getElementsByClassName(\"ModSelection\");\n\t\t\t\tareActiveModsSelected = !areActiveModsSelected;\n\t\t\t    SetSelection(checkboxes, areActiveModsSelected);\n\t\t\t}\n\t\t\tfunction SetInactiveSelection()\n\t\t\t{\n\t\t\t\tvar checkboxes = document.InactiveMods.getElementsByClassName(\"ModSelection\");\n\t\t\t\tareInactiveModsSelected = !areInactiveModsSelected;\n\t\t\t    SetSelection(checkboxes, areInactiveModsSelected);\n\t\t\t}\n\t\t\tfunction SetSelection(checkboxes, isSelected)\n\t\t\t{\n\t\t\t    for(var i=0, n=checkboxes.length;i<n;i++)\n\t\t\t\t{\n\t\t\t        checkboxes[i].checked = isSelected;\n\t\t\t    }\n\t\t\t}\n\t\t\tfunction checkSavegame()\n\t\t\t{\n\t\t\t    var e = document.getElementById(\"savegame\");\n             if (e != null) {\n\t\t\t        var savegame = e.options[e.selectedIndex].text;\n\t\t\t        var isEmpty = isEmptySavegame(savegame);\n\t\t\t        document.getElementById(\"mapSelector\").disabled = !isEmpty;\n\t\t\t        document.getElementById(\"difficultySelector\").disabled = !isEmpty;\n\t\t\t    }\n\t\t\t}\n\t\t\tfunction isEmptySavegame(value)\n\t\t\t{\n\t\t\t    var ret = value.match(/SAVEGAME \\d\\d* - Empty/g) || value.match(/SPIELSTAND \\d\\d* - Leer/g) || value.match(/SAUVEGARDE \\d\\d* - Vide/g);\n\t\t\treturn ret != null;\n\t\t\t}\n\t\t    window.onload = function()\n\t\t\t{\n\t\t\t    checkSavegame();\n\t\t\t}\n\t   </script>\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\" />\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"css/monitor.css\" />\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"css/fontawesome.min.css\" async=\"true\"/>\n    <link rel=\"shortcut icon\" href=\"favicon.ico\" type=\"image/x-icon\" />\n    <link rel=\"icon\" href=\"favicon.ico\" type=\"image/x-icon\" />\n    <script type=\"text/javascript\" src=\"js/all.js\"></script>\n</head>\n<body>\n    <div class=\"white-bg clearfix\">\n    <header class=\"main-header\" role=\"banner\" data-module=\"sticky\">\n    <div class=\"header-bar clearfix\">    <div class = \"row column\">    <div class = \"top-bar-right\">    <ul class = \"menu float-left\"><li><a target=\"_newTwitter\" href=\"https://twitter.com/farmingsim\"><span class=\"icon fab fa-twitter\"></span></a></li><li><a target=\"_newFacebook\" href=\"https://www.facebook.com/giants.farming.simulator\"><span class=\"icon fab fa-facebook\"></span></a></li><li><a target=\"_newYoutube\" href=\"https://www.youtube.com/user/giantssoftware\"><span class=\"icon fab fa-youtube\"></span></a></li><li><a target=\"_newInstagram\" href=\"https://www.instagram.com/giantsfarmingsimulator\"><span class=\"icon fab fa-instagram\"></span></a></li><li><a target=\"_newInstagram\" href=\"https://www.twitch.tv/giantssoftware\"><span class=\"icon fab fa-twitch\"></span></a></li><li><a target=\"_newInstagram\" href=\"https://discord.gg/giantssoftware\"><span class=\"icon fab fa-discord\"></span></a></li>\t   </ul>\n<ul class=\"dropdown menu float-right\"><li class=\"is-dropdown-submenu-parent is-down-arrow menu-flags\"><a href=\"#\"><img src=\"img/icons/flag-en.png\" alt=\"lang\"></a><ul class=\"menu submenu is-dropdown-submenu first-sub vertical\"><li><a href=\"?lang=en\"><img style=\"border:1px solid #000;\" src=\"img/icons/flag-en.png\" alt=\"English\"><span class=\"country\">English</span> <span>(en)</span></a></li><li><a href=\"?lang=de\"><img style=\"border:1px solid #000;\" src=\"img/icons/flag-de.png\" alt=\"Deutsch\"><span class=\"country\">Deutsch</span> <span>(de)</span></a></li><li><a href=\"?lang=fr\"><img style=\"border:1px solid #000;\" src=\"img/icons/flag-fr.png\" alt=\"Francais\"><span class=\"country\">Francais</span> <span>(fr)</span></a></li></ul></li></ul>    </div>\n    </div>\n    </div>\n<div class=\"top-bar\">        <div class=\"row column\">            <div class=\"top-bar-left\">                <a href=\"index.html?lang=en\" class=\"logo float-left\">                    <img src=\"img/logo.png\"></a>            </div>            <div class=\"top-bar-right\">                <ul class=\"menu float-left\">    <li><a href=\"index.html?lang=en\"><span>HOME</span></a></li>\n    <li><a href=\"savegames.html?lang=en\"><span>SAVEGAMES</span></a></li>\n    <li class=\"active\"><a href=\"mods.html?lang=en\"><span>MODS</span></a></li>\n    <li><a href=\"journal.html?lang=en\"><span>JOURNAL</span></a></li>\n<li><div class=\"status-indicator online\"><span>ONLINE</span></div></li></ul></div></div></div></header><section class=\"content-wrap\"><div class=\"row\"><h2>Mods</h2>\t<table>\n\t<colgroup>\n\t\t<col width=\"28%\">\n\t\t<col width=\"7%\">\n\t\t<col width=\"16%\">\n\t\t<col width=\"24%\">\n\t\t<col width=\"8%\">\n\t\t<col width=\"5%\">\n\t\t<col width=\"5%\">\n\t\t<col width=\"3%\">\n\t\t<col width=\"3%\">\n\t</colgroup>\n\t\t<tr>\n\t\t\t<td><b>Name</b></td>\n\t\t\t<td><b>Version</b></td>\n\t\t\t<td><b>Author</b></td>\n\t\t\t<td><b>Filename</b></td>\n\t\t\t<td align=\"right\"><b>Size</b></td>\n\t\t\t<td align=\"right\"><b>Issues</b></td>\n\t\t\t<td align=\"center\"><b>Active</b></td>\n\t\t\t<td>&nbsp;</td>\n\t\t\t<td>&nbsp;</td>\n\t\t</tr>\n\t\t<tr>\n\t\t\t\t<td title=\"AVANT-Series\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=0\">AVANT-Series</a></td>\n\t\t\t\t<td title=\"1.0.0.0\"><span style=\"color:orange;\">1.0.0.0</span></td>\n\t\t\t\t<td title=\"ITS\"><span style=\"color:orange;\">ITS</span></td>\n\t\t\t\t<td title=\"FS22_ITS_AVANT.zip\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=0\">FS22_ITS_AVANT.zip</a></td>\n\t\t\t\t<td align=\"right\"><span style=\"color:orange;\">38.04 MB</span></td>\n\t\t\t\t<td title=\"Unknown file type 'sounds/desktop.ini'\n\" align=\"right\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=0\">1</a></td>\n\t\t\t\t<td align=\"center\"><span style=\"color:orange;\">Yes</span></td>\n\t\t\t\t<td><a title=\"Download FS22_ITS_AVANT.zip\" href=\"mods/FS22_ITS_AVANT.zip\"><img class=\"icon\" src=\"img/icons/saveIcon.png\"></a></td>\n\t\t\t\t<td></td>\n\t\t</tr>\n\t\t<tr>\n\t\t\t\t<td title=\"Reisch Agrimaxx 1060 + RSDY-14\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=1\">Reisch Agrimaxx 1060 + RSDY-14</a></td>\n\t\t\t\t<td title=\"1.0.0.0\"><span style=\"color:orange;\">1.0.0.0</span></td>\n\t\t\t\t<td title=\"VertexDezign\"><span style=\"color:orange;\">VertexDezign</span></td>\n\t\t\t\t<td title=\"FS22_reischAgriMaxxPack.zip\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=1\">FS22_reischAgriMaxxPack.zip</a></td>\n\t\t\t\t<td align=\"right\"><span style=\"color:orange;\">30.68 MB</span></td>\n\t\t\t\t<td title=\"DDS texture file 'vehicles/agriMaxx1060/agriMaxx1060_normal.dds' is too big. Size 42.67 MB (max. 12.00 MB)\n\" align=\"right\"><a style=\"color:orange;\" href=\"mod.html?lang=en&mod_index=1\">1</a></td>\n\t\t\t\t<td align=\"center\"><span style=\"color:orange;\">Yes</span></td>\n\t\t\t\t<td><a title=\"Download FS22_reischAgriMaxxPack.zip\" href=\"mods/FS22_reischAgriMaxxPack.zip\"><img class=\"icon\" src=\"img/icons/saveIcon.png\"></a></td>\n\t\t\t\t<td></td>\n\t\t</tr>\n\t\t<tr>\n\t\t\t\t<td title=\"BR72 Bag Lifter\"><a href=\"mod.html?lang=en&mod_index=3\">BR72 Bag Lifter</a></td>\n\t\t\t\t<td title=\"1.0.0.0\">1.0.0.0</td>\n\t\t\t\t<td title=\"FredModding\">FredModding</td>\n\t\t\t\t<td title=\"FS22_BagLifter_BR72_Agrimanutention.zip\"><a href=\"mod.html?lang=en&mod_index=3\">FS22_BagLifter_BR72_Agrimanutent...</a></td>\n\t\t\t\t<td align=\"right\">1.16 MB</td>\n\t\t\t\t<td>&nbsp;</td>\n\t\t\t\t<td align=\"center\">Yes</td>\n\t\t\t\t<td><a title=\"Download FS22_BagLifter_BR72_Agrimanutention.zip\" href=\"mods/FS22_BagLifter_BR72_Agrimanutention.zip\"><img class=\"icon\" src=\"img/icons/saveIcon.png\"></a></td>\n\t\t\t\t<td></td>\n\t\t</tr><tr>\n\t\t<td><b>Total 3 Mods</b></td>\n\t\t<td>&nbsp;</td>\n\t\t<td>&nbsp;</td>\n\t\t<td>&nbsp;</td>\n\t\t<td align=\"right\"><b>69.88 MB </b></td>\n\t\t<td align=\"right\"><b>2</b></td>\n\t\t<td align=\"center\" title=\"Download all active mods\"><a href=\"all_mods_download?onlyActive=true\"><img class=\"icon\" src=\"img/icons/activeSaveIcon.png\"></a></td>\n\t\t<td>&nbsp;</td>\n\t\t<td></td>\n\t\t</tr>\n\t</table>\n</div>\n</section>\n  <div class=\"partners clearfix text-center\">   <div class=\"row\">    <div class=\"columns\">\n<img style=\"vertical-align:text-top;\" src=\"template/bottomLogo.jpg\" />    </div>   </div>  </div>  <footer class=\"main-footer clearfix\">\n  <div class=\"row\">\n    <div class=\"medium-8 columns\">\n<div class=\"copyright\"><a href=\"https://giants-software.com\" target=\"_newGiantsSoftware\"><img class=\"copyright__logo\" src=\"img/logos/footer-logo.png\"></a><div>&copy; 2018 GIANTS Software GmbH All Rights Reserved.<br>All other trademarks are properties of their respective owners.</div></div>    </div>\n    <div class=\"medium-4 small-12 columns\">\n    <ul class=\"menu menu-footer float-right\">\n<li><a href=\"https://www.farming-simulator.com\">9.0.0.0 (Non-commercial&nbsp;use&nbsp;only)</a></li>\n    </ul>\n    </div>\n  </div>\n  </footer>\n    <script type=\"text/javascript\" src=\"js/jquery.min.js\"></script>\n    <script type=\"text/javascript\" src=\"js/frontend.js\"></script>\n<script>$(document).foundation();</script>\n</div>\n</body>\n</html>\n");

            webLoader.Load(Arg.Any<string>()).Returns(mockedResponse);

            List<Mod> expected = new List<Mod>()
            {
                new Mod()
                {
                    Title = "AVANT-Series",
                    Url = baseUrl + "mods/FS22_ITS_AVANT.zip",
                    Version = "1.0.0.0",
                    ModType = ModType.Online
                },
                new Mod()
                {
                    Title = "Reisch Agrimaxx 1060 + RSDY-14",
                    Url = baseUrl + "mods/FS22_reischAgriMaxxPack.zip",
                    Version = "1.0.0.0",
                    ModType = ModType.Online
                },
                new Mod()
                {
                    Title = "BR72 Bag Lifter",
                    Url = baseUrl + "mods/FS22_BagLifter_BR72_Agrimanutention.zip",
                    Version = "1.0.0.0",
                    ModType = ModType.Online
                },
            };

            //Act
            List<Mod> actual = onlineFetcher.Fetch(baseUrl).ToList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
