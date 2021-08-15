using System;

namespace FakeDataInserter
{
    public class Company
    {
        public string CompanyCode {get;set;} = Guid.NewGuid().ToString(); 
        public string Name {get;set;} = Faker.Company.Name();
        public string BS {get;set;} = Faker.Company.BS();
        public string CatchPhrase {get;set;} = Faker.Company.CatchPhrase();
        public string Suffix {get;set;} = Faker.Company.Suffix();
    }
}