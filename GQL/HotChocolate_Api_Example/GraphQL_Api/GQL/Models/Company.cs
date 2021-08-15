using System.ComponentModel.DataAnnotations;

namespace GraphQL_Api.GQL.Models
{
    public class Company
    {
        [Key]
        public string CompanyCode {get;set;}
        [Required]
        public string Name {get;set;}
        public string BS {get;set;}
        public string CatchPhrase {get;set;}
        public string Suffix {get;set;}
    }
}