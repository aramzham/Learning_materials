namespace FakeDataInserter
{
    public class Address
    {
        public string Country {get;set;} = Faker.Country.Name();
        public string AddressCountry {get;set;} = Faker.Address.Country();
        public string ZipCode {get;set;} = Faker.Address.ZipCode();
        public string City {get;set;} = Faker.Address.City();
    }
}