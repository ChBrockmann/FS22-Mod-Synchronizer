using FluentAssertions;
using LS_Mod_Synchronizer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void Test_GetLocalFileName()
        {
            string input = @"C:\Users\brock\Documents\My Games\FarmingSimulator2022\mods\FS22_Fliegl_DPW_180_Squarebale_Autoload_BAFM.zip";

            string expected = "FS22_Fliegl_DPW_180_Squarebale_Autoload_BAFM";

            string actual = input.GetLocalFileName();

            actual.Should().Be(expected);
        }

        [Fact]
        public void Test_GetPrevDirectoryName()
        {
            string input = @"C:\Users\brock\Documents\My Games\FarmingSimulator2022\mods\tmp\FS22_Fliegl_DPW_180_Squarebale_Autoload_BAFM\modDesc.xml";

            string expected = "FS22_Fliegl_DPW_180_Squarebale_Autoload_BAFM";

            string actual = input.GetPrevDirectoryName();

            actual.Should().Be(expected);
        }
    }
}
