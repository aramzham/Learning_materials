using System.Threading;
using GraphQL_Api.GQL.Models;
using GraphQL_Api.GQL.Models.Inputs;
using GraphQL_Api.GQL.Models.Payloads;
using HotChocolate;
using MongoDB.Driver;
using System.Threading.Tasks;
using GraphQL_Api.GQL.SubscriptionTypes;
using HotChocolate.Subscriptions;

namespace GraphQL_Api.GQL.MutationTypes
{
    public class Mutation
    {
        public async Task<AddPersonPayload> AddPerson([Service] IMongoClient mongoClient, AddPersonInput personInput, [Service] ITopicEventSender eventSender, CancellationToken ct)
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

            await eventSender.SendAsync(nameof(Subscription.OnPersonAdded), newPerson, ct);

            return new AddPersonPayload(newPerson);
        }

        public async Task<AddEmailPayload> AddEmail([Service] IMongoClient mongoClient, AddEmailInput emailInput)
        {
            var newEmail = new Email()
            {
                DomainName = emailInput.DomainName,
                DomainSuffix = emailInput.DomainSuffix,
                DomainWord = emailInput.DomainWord,
                EmailAddress = emailInput.EmailAddress,
                FreeEmail = emailInput.FreeEmail,
                PersonId = emailInput.PersonId
            };

            await mongoClient.GetDatabase("testDb").GetCollection<Email>("Emails").InsertOneAsync(newEmail);

            return new AddEmailPayload(newEmail);
        }
    }
}
