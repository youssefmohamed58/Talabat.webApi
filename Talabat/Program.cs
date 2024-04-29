using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Apis.Extensions;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities.Identity_Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddDbContext<IdentityModuleDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }
       );
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            }
            );

            builder.Services.AddServices();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddCors( Options =>
            {
                Options.AddPolicy("MyPolicy", options =>

                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });
            var app = builder.Build();
            // Update Database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = services.GetRequiredService<StoreContext>();
                await DbContext.Database.MigrateAsync();
                var IdentityContext = services.GetRequiredService<IdentityModuleDbContext>();
                await IdentityContext.Database.MigrateAsync();
                var usermanager = services.GetRequiredService<UserManager<AppUser>>();
                await IdentityDbContextSeed.UserSeedAsync(usermanager);
                await StoreContextSeed.SeedAsync(DbContext);
            }
            catch (Exception ex)
            {
              var Logger = LoggerFactory.CreateLogger<Program>();
              Logger.LogError(ex, "An Error Occured Ehile applying Migration");
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}