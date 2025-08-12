using System.Diagnostics;
using System.Text;

namespace SHARModChecklist;
public class ModConfig
{
    private static readonly HttpClient _httpClient = new();
    static ModConfig()
    {
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("SHARModChecklist/1.0");
        _httpClient.DefaultRequestHeaders.CacheControl = new()
        {
            NoCache = true,
            NoStore = true,
        };
    }

    public string ModName { get; set; }
    public string DisplayName { get; set; }
    public Dictionary<string, string> TextBibleDefaults { get; set; } = [];
    public List<Level> Levels { get; set; } = new(7);

    public static ModConfig? FromString(string json) => System.Text.Json.JsonSerializer.Deserialize<ModConfig>(json);

    public static ModConfig? FromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        string json = File.ReadAllText(filePath);
        return FromString(json);
    }

    public static async Task<ModConfig?> FromURL(string url)
    {
        string json = await _httpClient.GetStringAsync(url);
        return FromString(json);
    }

    public static async Task<ModConfig?> FromGitHub(string modName)
    {
        var url = $"https://api.github.com/repos/Hampo/SHARModChecklist/contents/ModConfigs/{Uri.EscapeDataString(modName)}.json?ref=main";
        string githubJson = await _httpClient.GetStringAsync(url);

        using var doc = System.Text.Json.JsonDocument.Parse(githubJson);
        if (!doc.RootElement.TryGetProperty("content", out var content))
        {
#if DEBUG
            Debugger.Break();
#endif
            return null;
        }
        
        var encodedContent = content.GetString();
        if (encodedContent == null)
        {
#if DEBUG
            Debugger.Break();
#endif
            return null;
        }

        var decodedBytes = Convert.FromBase64String(encodedContent);
        var json = Encoding.UTF8.GetString(decodedBytes);

        return FromString(json);
    }

    public class Level
    {
        public List<Card> Cards { get; set; } = new(7);
        public List<Gag> Gags { get; set; } = new(32);
        public List<Wasp> Wasps { get; set; } = new(50);
    }

    public class Card(string name, string location)
    {
        public string Name { get; set; } = name;
        public string Location { get; set; } = location;
    }

    public class Gag(string filename, string location)
    {
        public string Filename { get; set; } = filename;
        public string Location { get; set; } = location;
    }

    public class Wasp(string locator, string location)
    {
        public string Locator { get; set; } = locator;
        public string Location { get; set; } = location;
    }
}
