using System.Linq;
using GaylordShop.API.Data;
using HotChocolate;

namespace GaylordShop.API.Graph
{
    public class Query
    {
        public IQueryable<Product> GetProducts([Service] ShopDbContext context) => context.Products;
        public IQueryable<Manufacturer> GetManufacturers([Service] ShopDbContext context) => context.Manufacturers;
    }
}