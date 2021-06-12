using System.Threading;
using System.Threading.Tasks;
using GaylordShop.API.Graph;
using HotChocolate;

namespace GaylordShop.API.Data
{
    public class Mutation
    {
        public async Task<AddManufacturerPayload> AddManufacturerAsync(AddManufacturerInput input, [Service] ShopDbContext context, CancellationToken cancellationToken)
        {
            var manufacturer = new Manufacturer { Name = input.Name };
            await context.Manufacturers.AddAsync(manufacturer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return new AddManufacturerPayload(manufacturer);
        }

        public async Task<AddProductPayload> AddProductAsync(AddProductInput input, [Service] ShopDbContext context, CancellationToken cancellationToken)
        {
            Manufacturer? manufacturer = await context.Manufacturers.FindAsync(input.ManufacturerId);
            var product = new Product { Name = input.Name, Price = input.Price, LastUpdated = input.LastUpdated, PrimaryManufacturer = manufacturer };
            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return new AddProductPayload(product);
        }
    }
}