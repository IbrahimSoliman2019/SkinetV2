using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class StoreContextSeedData
    {
        public static async Task SeedAsync(StoreContext store,ILoggerFactory factory)
        {
            try
            {
                if (!store.ProductBrands.Any())
                {
                    var brandsdata = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);
                    foreach(var item in brands)
                    {
                        store.ProductBrands.Add(item);
                    }
                    await store.SaveChangesAsync();
                }
                if (!store.ProductTypes.Any())
                {
                    var typesdata = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);
                    foreach (var item in types)
                    {
                        store.ProductTypes.Add(item);
                    }
                    await store.SaveChangesAsync();
                }
                if (!store.Products.Any())
                {
                    var productdata = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productdata);
                    foreach (var item in products)
                    {
                        store.Products.Add(item);
                    }
                  await  store.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = factory.CreateLogger<StoreContextSeedData>();
                logger.LogError(ex.Message);
            }

        }
    }
}
