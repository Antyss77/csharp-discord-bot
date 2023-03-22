using Newtonsoft.Json;

namespace HyBot;

public class BotConfig {
    public string Status { get; set; }
    public string Description { get; set; }

    public static BotConfig LoadFromFile(string filePath) {
        string jsonContent = File.ReadAllText("config.json");
        return JsonConvert.DeserializeObject<BotConfig>(jsonContent);
    }
}