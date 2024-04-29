using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> Options) : base(Options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }
        public DbSet<Product> Products { get; set; }    
        public DbSet<ProductBrand> Brands { get; set; } 
        public DbSet<ProductType> Types { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DelivaryMethod> DelivaryMethods { get; set; }
    }
}
