using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Talabat.Core.Services;

namespace Talabat.Apis.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTimeInSeconds;

        public CachedAttribute(int ExpireTimeInSeconds)
        {
            _expireTimeInSeconds = ExpireTimeInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var CacheKey = GetCacheKeyFromRequest(context.HttpContext.Request);
            var CacheResponse = await CacheService.GetCachedData(CacheKey);  
            if(!string.IsNullOrEmpty(CacheResponse))
            {
                var ContentResult = new ContentResult()
                {
                    Content = CacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = ContentResult;
                return;
            }
            var ExecutedEndPointContext = await next.Invoke();  // will execute action
            if( ExecutedEndPointContext.Result is OkObjectResult result ) 
            {
                await CacheService.CacheResponseAsync(CacheKey, result.Value, TimeSpan.FromSeconds(_expireTimeInSeconds));
            }
            
        }

        private string GetCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);  // Api/Products
            foreach( var (key , value) in request.Query.OrderBy(o=>o.Key) )
            {
                KeyBuilder.Append($"|{key}-{value}");
            }
            return KeyBuilder.ToString();   

 
        }
    }
}
