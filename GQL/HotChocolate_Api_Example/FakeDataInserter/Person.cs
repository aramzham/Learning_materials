using System;
using System.Collections.Generic;

namespace FakeDataInserter
{
    public class Person
    {
        public string Name {get;set;} = Faker.Name.FullName();

        public Address Address {get;set;} = new();

        public string CompanyCode { get; set; }

        public DateTime DateOfBirth {get;set;} = Faker.Identification.DateOfBirth();

        public string SocialInsuranceNumber {get;set;} = Faker.Identification.UkNationalInsuranceNumber();

        public string PassportNumber {get;set;} = Faker.Identification.UsPassportNumber();

        public IEnumerable<string> EmailIds { get; set; } = new List<string>();

        public int PersonalCode {get;set;} = Faker.RandomNumber.Next();

        public string PhoneNumber {get;set;} = Faker.Phone.Number();
    }
}