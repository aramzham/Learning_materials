using HotChocolate;

namespace GraphQL_Api.GQL.Models
{
    [GraphQLDescription("Address of the person")]
    public class Address
    {
        [GraphQLDescription("full country name")]
        public string Country {get;set;}
        public string AddressCountry {get;set;}
        public string ZipCode {get;set;}
        public string City {get;set;}
    }
}