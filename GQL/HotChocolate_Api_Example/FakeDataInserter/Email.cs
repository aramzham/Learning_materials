using System;
using System.ComponentModel.DataAnnotations;

namespace FakeDataInserter
{
    public class Email
    {
        public string DomainWord { get; set; } = Faker.Internet.DomainWord();
        public string DomainName { get; set; } = Faker.Internet.DomainName();
        public string DomainSuffix { get; set; } = Faker.Internet.DomainSuffix();
        public string FreeEmail { get; set; } = Faker.Internet.FreeEmail();
        public string EmailAddress { get; set; } = Faker.Internet.Email();
        public string PersonId { get; set; }
    }
}