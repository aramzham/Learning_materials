using System.Collections.Generic;
using GraphQL_Api.GQL.Models;
using HotChocolate;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphQL_Api.GQL.QueryTypes
{
    public class Query
    {
        //[UseDbContext(typeof(AppDbContext))] in HotChocolate.Data to make use of pooled db context
        // then turn [Service] to [ScopedService]
        public IEnumerable<Person> GetAll([Service] IMongoClient client)
        {
            return client.GetDatabase("testDb").GetCollection<Person>("People").Find(new BsonDocument()).ToList();
        }

        public IEnumerable<Person> GetAllWithEmails([Service] IMongoClient client)
        {
            var lookup = new[] {
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        {"from", "Emails"},
                        {"localField", "EmailIds"},
                        {"foreignField", "_id"},
                        {"as", "Emails"}
                    })
            };
            return client.GetDatabase("testDb").GetCollection<Person>("People").Aggregate<Person>(lookup).ToList();
        }
    }
}