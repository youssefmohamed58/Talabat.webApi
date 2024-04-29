using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket Basket)
        {
           var JsonBasket = JsonSerializer.Serialize(Basket);
           var CreateOrUpdateBasket = await _database.StringSetAsync(Basket.Id , JsonBasket , TimeSpan.FromDays(1));
            if (!CreateOrUpdateBasket) return null;
            return await GetCustomerBasket(Basket.Id);

        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetCustomerBasket(string BasketId)
        {
            var JsonBasket =await  _database.StringGetAsync(BasketId);
            return JsonBasket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(JsonBasket);   

        }
    }
}
