using System;

namespace GraphQL_Api.GQL.Models.Inputs
{
    // records are new reference types introduced in C# 9.0 with built in value-based equality check
    public record AddPersonInput(string Name, string Country, DateTime DateOfBirth, string PhoneNumber);
}