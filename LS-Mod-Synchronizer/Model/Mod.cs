namespace LS_Mod_Synchronizer.Model;

public class Mod
{
    public string Title { get; set; }
    public string Version { get; set; }
    public string Url { get; set; }
    public ModType ModType { get; set; }
}

public enum ModType
{
    Local,
    Online
}
