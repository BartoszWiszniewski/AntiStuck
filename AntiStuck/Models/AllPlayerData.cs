using Newtonsoft.Json;

namespace AntiStuck.Models
{
    
    public class AllPlayerData
    {
        [JsonProperty("championName")]
        public string ChampionName { get; set; }

        [JsonProperty("isBot")]
        public bool IsBot { get; set; }

        [JsonProperty("isDead")]
        public bool IsDead { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("rawChampionName")]
        public string RawChampionName { get; set; }

        [JsonProperty("respawnTimer")]
        public double RespawnTimer { get; set; }

        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
    }
}