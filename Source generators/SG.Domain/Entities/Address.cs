using SG.Generator;

namespace SG.Domain.Entities;

[ToJsonSerializer(typeof(Address))]
internal partial record Address
{
    // comments won't regenerate the code because we had caching with ClassInfo
    // we are interested only in changes that are reflected in ClassInfo
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}