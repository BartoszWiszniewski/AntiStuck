using Newtonsoft.Json;

namespace AntiStuck.Models
{
    public class GameStats
    {
        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("gameTime")]
        public double GameTime { get; set; }

        [JsonProperty("mapName")]
        public string MapName { get; set; }

        [JsonProperty("mapNumber")]
        public int MapNumber { get; set; }

        [JsonProperty("mapTerrain")]
        public string MapTerrain { get; set; }
    }
}