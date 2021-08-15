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

            var emails = Enumerable.Range(0, 300).Select(_ => new Email()).ToList();
            emailsCollection.InsertMany(emails);
            var emailIds = emails.Select(x => x.Id).ToList();

            var companies = Enumerable.Range(0, 10).Select(_ => new Company()).ToList();
            var companyIds = companies.Select(x => x.CompanyCode).ToList();
            companiesCollection.InsertMany(companies);

            var people = Enumerable.Range(0, 100).Select(_ => new Person()
            {
                CompanyCode = companyIds[rnd.Next(0, 10)],
                EmailIds = GetEmailIds(rnd, emailIds)
            });
            peopleCollection.InsertMany(people);

            Console.ReadKey();
        }

        private static IEnumerable<string> GetEmailIds(Random rnd, IReadOnlyList<string> emailIds)
        {
            var list = new string[rnd.Next(0, 4)];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = emailIds[rnd.Next(0, emailIds.Count)];
            }

            return list;
        }
    }
}
