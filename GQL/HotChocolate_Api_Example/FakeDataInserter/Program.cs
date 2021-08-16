using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeDataInserter
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new MongoClient("mongodb://localhost:27017");
            var testDb = driver.GetDatabase("testDb");
            testDb.DropCollection("Companies");
            testDb.DropCollection("People");
            testDb.DropCollection("Emails");

            var companiesCollection = testDb.GetCollection<Company>("Companies");
            var peopleCollection = testDb.GetCollection<Person>("People");
            var emailsCollection = testDb.GetCollection<Email>("Emails");

            var rnd = new Random();

            var companies = Enumerable.Range(0, 10).Select(_ => new Company()).ToList();
            companiesCollection.InsertMany(companies);

            var people = Enumerable.Range(0, 100).Select(_ => new Person()
            {
                CompanyCode = companies[rnd.Next(0, 10)].CompanyCode
            }).ToList();
            peopleCollection.InsertMany(people);

            var emails = Enumerable.Range(0, 300).Select(_ => new Email() { PersonId = people[rnd.Next(people.Count)].Id }).ToList();
            emailsCollection.InsertMany(emails);

            Console.ReadKey();
        }
    }
}
