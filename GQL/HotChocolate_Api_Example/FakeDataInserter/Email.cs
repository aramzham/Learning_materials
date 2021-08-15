using System;
using System.ComponentModel.DataAnnotations;

namespace FakeDataInserter
{
    public class Email
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DomainWord { get; set; } = Faker.Internet.DomainWord();
        public string DomainName { get; set; } = Faker.Internet.DomainName();
        public string DomainSuffix { get; set; } = Faker.Internet.DomainSuffix();
        public string FreeEmail { get; set; } = Faker.Internet.FreeEmail();
        public string EmailAddress { get; set; } = Faker.Internet.Email();
    }
}