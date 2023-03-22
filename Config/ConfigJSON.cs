using System.Text.Json;
using Newtonsoft.Json;

namespace HyBot;

public class ConfigJSON {

    [JsonProperty("token")]
    public string Token { get; set; }
    

}