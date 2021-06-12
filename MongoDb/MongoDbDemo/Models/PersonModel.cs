using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDemo
{
    public class PersonModel
    {
        [BsonId] // _id
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel Address { get; set; }
        [BsonElement("dob")] // name in db
        public DateTime DateOfBirth { get; set; }
    }
}