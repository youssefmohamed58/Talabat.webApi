using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireDate)
        {
            if (Response is null)
                return;
            var Options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var SerilizedResponse = JsonSerializer.Serialize(Response, Options);

            await _database.StringSetAsync(CacheKey, SerilizedResponse , ExpireDate);  
        }

        public async Task<string?> GetCachedData(string CacheKey)
        {
            var CachedResponse = await _database.StringGetAsync(CacheKey);
            if(CachedResponse.IsNullOrEmpty)
                return null;
            return CachedResponse;
        }
    }
}
