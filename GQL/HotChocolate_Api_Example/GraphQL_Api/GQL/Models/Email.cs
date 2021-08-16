using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQL_Api.GQL.Models
{
    [BsonIgnoreExtraElements]
    public class Email
    {
        public string DomainWord { get; set; }
        public string DomainName { get; set; }
        public string DomainSuffix { get; set; }
        public string FreeEmail { get; set; }
        public string EmailAddress { get; set; }
        public string PersonId { get; set; }
    }
}