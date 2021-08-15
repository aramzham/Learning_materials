using System.ComponentModel.DataAnnotations;

namespace GraphQL_Api.GQL.Models
{
    public class Email
    {
        [Key]
        public string Id { get; set; }
        public string DomainWord { get; set; }
        public string DomainName { get; set; }
        public string DomainSuffix { get; set; }
        public string FreeEmail { get; set; }
        public string EmailAddress { get; set; }
    }
}