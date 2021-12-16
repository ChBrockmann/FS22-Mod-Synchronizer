using System.Linq;

namespace LS_Mod_Synchronizer.Extensions;

public static class StringExtension
{
    public static string GetStringBetweenTwoDelimeters(this string input, string delimeter)
    {
        int pFrom = input.IndexOf(delimeter) + delimeter.Length;
        int pTo = input.LastIndexOf(delimeter);

        return input.Substring(pFrom, pTo - pFrom);
    }

    public static string GetLocalFileName(this string input)
    {
        return input.Split('\\').Last().Split('.').First();
    }

    public static string GetPrevDirectoryName(this string input)
    {
        var splitted = input.Split('\\');

        return splitted[splitted.Length - 2];
    }
}

