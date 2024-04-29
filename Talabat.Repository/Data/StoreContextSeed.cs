using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {

            if (!context.Brands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await context.Brands.AddAsync(Brand);
                    }
                    
                }
                await context.SaveChangesAsync();
            }
            if (!context.Types.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await context.Types.AddAsync(Type);
                    }
                    
                }
                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var Products = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var ProductData = JsonSerializer.Deserialize<List<Product>>(Products);
                if (ProductData?.Count > 0)
                {
                    foreach (var product in ProductData)
                    {
                        await context.Products.AddAsync(product);
                    }
                 
                }
                await context.SaveChangesAsync();
            }
            //if (!context.DelivaryMethods.Any())
            //{

            //    var DelivaryMethods = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
            //    var MethodsData = JsonSerializer.Deserialize<List<DelivaryMethod>>(DelivaryMethods);
            //    if (MethodsData?.Count > 0)
            //    {
            //        foreach (var DelivaryMethod in MethodsData)
            //        {
            //            await context.DelivaryMethods.AddAsync(DelivaryMethod);
            //        }

            //    }
            //    await context.SaveChangesAsync();
            //}

        }
    }
}

