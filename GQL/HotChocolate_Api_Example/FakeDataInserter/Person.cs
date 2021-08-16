using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeDataInserter
{
    public class Person
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name {get;set;} = Faker.Name.FullName();

        public Address Address {get;set;} = new();

        public string CompanyCode { get; set; }

        public DateTime DateOfBirth {get;set;} = Faker.Identification.DateOfBirth();

        public string SocialInsuranceNumber {get;set;} = Faker.Identification.UkNationalInsuranceNumber();

        public string PassportNumber {get;set;} = Faker.Identification.UsPassportNumber();

        public ICollection<Email> Emails { get; set; }

        public int PersonalCode {get;set;} = Faker.RandomNumber.Next();

        public string PhoneNumber {get;set;} = Faker.Phone.Number();
    }
}