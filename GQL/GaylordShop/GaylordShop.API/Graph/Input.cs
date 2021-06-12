using System;

namespace GaylordShop.API.Graph
{
    public record AddManufacturerInput(string Name);

    public record AddProductInput(string Name, float Price, DateTime LastUpdated, int ManufacturerId);
}