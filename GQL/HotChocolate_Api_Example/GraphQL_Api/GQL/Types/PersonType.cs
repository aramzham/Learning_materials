using GraphQL_Api.GQL.Models;
using HotChocolate;
using HotChocolate.Types;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL_Api.GQL.Types
{
    public class PersonType : ObjectType<Person>
    {
        protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
        {
            descriptor.Description("This is person's description from ObjectType");

            descriptor.Field(x => x.PassportNumber).Ignore();

            descriptor
                .Field(x => x.Emails)
                .ResolveWith<Resolvers>(x => x.GetEmails(default, default))
                .Description("email addresses of the person");
        }

        private class Resolvers
        {
            public IEnumerable<Email> GetEmails([Service] IMongoClient mongoClient, Person person)
            {
                var a = mongoClient.GetDatabase("testDb").GetCollection<Email>("Emails")
                    .Find(Builders<Email>.Filter.Eq("PersonId", person.Id)).ToList();
                return a;
            }
        }
    }
}