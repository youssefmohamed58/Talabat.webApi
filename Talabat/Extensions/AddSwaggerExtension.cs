using Talabat.Apis.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.Apis.Extensions
{
    public static class AddSwaggerExtension
    {
        public static WebApplication UseSwaggerMiddleware (this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;

        }
    }
}
