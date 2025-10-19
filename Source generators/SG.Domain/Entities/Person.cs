using SG.Generator;

namespace SG.Domain.Entities;

[ToJsonSerializer(minified: true)]
public partial class Person
{
    public string EmailAddress { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string? PhoneNumber { get; set; }
}