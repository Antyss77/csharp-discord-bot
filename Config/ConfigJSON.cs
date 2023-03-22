using System.Text.Json;
using Newtonsoft.Json;

namespace HyBot;

public class ConfigJSON {

    [JsonProperty("token")]
    public string Token { get; set; }
    
    [JsonProperty("prefix")]
    public string Prefix { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    
}