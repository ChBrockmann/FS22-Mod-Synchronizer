using FluentAssertions;
using LS_Mod_Synchronizer.Logic;
using LS_Mod_Synchronizer.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class MopComparerTests
    {
        private ModComparer comparer;

        public MopComparerTests()
        {
            comparer = new ModComparer();
        }

        [Fact]
        public void NoLocalMods_ShouldReturnAll()
        {
            List<Mod> input = new List<Mod>()
            {
                new Mod()
                {
                    Title = "Online Mod #1",
                    Version = "1.0.0",
                    Url = "",
                    ModType = ModType.Online
                },
                new Mod()
                {
                    Title = "Online Mod #2",
                    Version = "2.0.0",
                    Url = "",
                    ModType = ModType.Online
                }
            };

            IEnumerable<Mod> output = comparer.GetListOfAllModsToDownload(input);

            output.Should().HaveCount(2);
        }

        [Fact]
        public void NoOnlineMods_ShouldReturnEmptyList()
        {
            List<Mod> input = new List<Mod>()
            {
                new Mod()
                {
                    Title = "Mod #1",
                    Version = "1.0.0",
                    Url = "",
                    ModType = ModType.Local
                },
                new Mod()
                {
                    Title = "Mod #2",
                    Version = "2.0.0",
                    Url = "",
                    ModType = ModType.Local
                }
            };

            IEnumerable<Mod> output = comparer.GetListOfAllModsToDownload(input);

            output.Should().HaveCount(0);
        }

        [Fact]
        public void TwoOnlineMods_OneLocalMod_ShouldReturnOneMod_VersionAreEqual()
        {
            List<Mod> input = new List<Mod>()
            {
                new Mod()
                {
                    Title = "Mod #1",
                    Version = "1.0.0",
                    Url = "",
                    ModType = ModType.Online
                },
                new Mod()
                {
                    Title = "Mod #2",
                    Version = "2.0.0",
                    Url = "",
                    ModType = ModType.Online
                },
                new Mod()
                {
                    Title = "Mod #1",
                    Version = "1.0.0",
                    Url = "",
                    ModType = ModType.Local
                }
            };

            IEnumerable<Mod> output = comparer.GetListOfAllModsToDownload(input);

            output.Should().HaveCount(1);
            output.First().Title.Should().Be("Mod #2");
        }

        [Fact]
        public void OnlineModIsDifferentVersion_ShouldReturnOnlineMod()
        {
            List<Mod> input = new List<Mod>()
            {
                new Mod()
                {
                    Title = "Mod #1",
                    Version = "1.0.0",
                    Url = "",
                    ModType = ModType.Local
                },
                new Mod()
                {
                    Title = "Mod #1",
                    Version = "2.0.0",
                    Url = "",
                    ModType = ModType.Online
                }
            };

            IEnumerable<Mod> output = comparer.GetListOfAllModsToDownload(input);

            output.Should().HaveCount(1);
            output.First().Title.Should().Be("Mod #1");
        }
    }
}
