using GraphQL_Api.GQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL_Api.GQL.SubscriptionTypes
{
    public class Subscription
    {
        // notify after person is added by mutation
        [Subscribe]
        [Topic]
        public Person OnPersonAdded([EventMessage] Person person)
        {
            return person;
        }
    }
}