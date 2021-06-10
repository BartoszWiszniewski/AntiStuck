using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AntiStuck.Models;
using Flurl;
using Flurl.Http;

namespace AntiStuck
{
    
    public static class ApiService
    {
        public static string BaseUrl = "https://127.0.0.1:2999/liveclientdata";

        public static async Task<ActivePlayerData> GetActivePlayerData()
        {
            return await BaseUrl.AppendPathSegment("activeplayer").GetJsonAsync<ActivePlayerData>();
        }

        public static async Task<List<AllPlayerData>> GetAllPlayersData()
        {
            return await BaseUrl.AppendPathSegment("playerlist").GetJsonAsync<List<AllPlayerData>>();
        }

        public static async Task<GameStats> GetGameStats()
        {
            return await BaseUrl.AppendPathSegment("gamestats").GetJsonAsync<GameStats>();
        }
        
        public static async Task<bool> IsLiveGameRunning()
        {
            try
            {
                var result = await BaseUrl.AppendPathSegment("allgamedata").GetAsync();
                return result.StatusCode == 200;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}