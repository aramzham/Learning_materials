using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQL_Api.GQL.Models
{
    [BsonIgnoreExtraElements]
    public class Person
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public string CompanyCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SocialInsuranceNumber { get; set; }

        public string PassportNumber { get; set; }

        public ICollection<Email> Emails { get; set; } = new List<Email>();

        public int PersonalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}