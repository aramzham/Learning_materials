using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL_Api.GQL.Models;
using GraphQL_Api.GQL.Models.Inputs;
using GraphQL_Api.GQL.Models.Payloads;
using HotChocolate;
using MongoDB.Driver;

namespace GraphQL_Api.GQL.MutationTypes
{
    public class Mutation
    {
        public async Task<AddPersonPayload> AddPerson([Service] IMongoClient mongoClient, AddPersonInput personInput)
        {
            var newPerson = new Person()
            {
                Name = personInput.Name,
                Address = new Address()
                {
                    Country = personInput.Country
                },
                DateOfBirth = personInput.DateOfBirth,
                PhoneNumber = personInput.PhoneNumber
            };

            await mongoClient.GetDatabase("testDb").GetCollection<Person>("People").InsertOneAsync(newPerson);

            return new AddPersonPayload(newPerson);
        }
    }
}
