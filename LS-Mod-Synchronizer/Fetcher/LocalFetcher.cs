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
    public class LocalFetcher
    {
        private readonly string Path;

        public LocalFetcher(string Path)
        {
            this.Path = Path;
        }

        public ICollection<Mod> Fetch()
        {
            DeleteDirectoryRecursivley(Ressources.LOCAL_TMP_DIRECTORY);

            ExtractMetaFile();

            return ParseMetaFiles(Ressources.LOCAL_TMP_DIRECTORY);
        }

        private void DeleteDirectoryRecursivley(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
            }
            catch (Exception) { }
        }

        private void ExtractMetaFile()
        {
            foreach (string file in Directory.GetFiles(Path, "*.zip"))
            {
                string saveMetaFileAt = Ressources.LOCAL_TMP_DIRECTORY + file.GetLocalFileName();
                using (ZipFile zf = new ZipFile(file))
                {
                    ZipEntry entry = zf[Ressources.LOCAL_MOD_META_FILENAME];
                    entry.Extract(saveMetaFileAt);
                }
            }
        }

        private ICollection<Mod> ParseMetaFiles(string directory)
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

                        mods.Add(new Mod()
                        {
                            Title = nameNode.InnerText,
                            Version = versionNode.InnerText,
                            ModType = ModType.Local,
                            Url = GetModZipFile(file)
                        });
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }

            return mods;
        }

        private string GetModZipFile(string file)
        {
            var listOfAllZips = Directory.GetFiles(Path, "*.zip");
            return listOfAllZips.First(a => a.Contains(a.GetPrevDirectoryName()));
        }
    }
}
