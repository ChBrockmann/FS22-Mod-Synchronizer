using Ionic.Zip;
using LS_Mod_Synchronizer.Extensions;
using LS_Mod_Synchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace LS_Mod_Synchronizer.Fetcher
{
    public class LocalFetcher : ILocalFetcher
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ICollection<Mod> Fetch(string path)
        {
            DeleteDirectoryRecursivley(Ressources.LOCAL_TMP_DIRECTORY);

            ExtractMetaFile(path);

            return ParseMetaFiles(Ressources.LOCAL_TMP_DIRECTORY, path);
        }

        private void DeleteDirectoryRecursivley(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
            }
            catch (Exception) { }
        }

        private void ExtractMetaFile(string path)
        {
            foreach (string file in Directory.GetFiles(path, "*.zip"))
            {
                try
                {
                    string saveMetaFileAt = Ressources.LOCAL_TMP_DIRECTORY + file.GetLocalFileName();
                    using (ZipFile zf = new ZipFile(file))
                    {
                        ZipEntry entry = zf[Ressources.LOCAL_MOD_META_FILENAME];
                        entry.Extract(saveMetaFileAt);
                    }
                }
                catch (Exception)
                {
                    Logger.Info($"Could not extract metafile from {file}");
                }
            }
        }

        private ICollection<Mod> ParseMetaFiles(string directory, string path)
        {
            List<Mod> mods = new List<Mod>();
            try
            {
                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    string file = Directory.GetFiles(subDir).First();

                    try
                    {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(file);

                        XmlNode versionNode = xmlDocument.SelectSingleNode("/modDesc/version");
                        XmlNode nameNode = xmlDocument.SelectSingleNode("/modDesc/title/en");
                        if(nameNode == null)
                        {
                            nameNode = xmlDocument.SelectSingleNode("/modDesc/title");
                        }

                        mods.Add(new Mod()
                        {
                            Title = nameNode.InnerText,
                            Version = versionNode.InnerText,
                            ModType = ModType.Local,
                            Url = GetModZipFile(file, path)
                        });
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }

            return mods;
        }

        private string GetModZipFile(string file, string path)
        {
            var listOfAllZips = Directory.GetFiles(path, "*.zip");
            return listOfAllZips.First(a => a.Contains(a.GetPrevDirectoryName()));
        }
    }
}
