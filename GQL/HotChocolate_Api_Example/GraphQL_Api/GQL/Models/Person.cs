using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQL_Api.GQL.Models
{
    [BsonIgnoreExtraElements]
    [GraphQLDescription("Personal information of every person in the neighborhood")]
    public class Person
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string CompanyCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SocialInsuranceNumber { get; set; }

        [GraphQLDescription("passport number is strictly confidential")]
        public string PassportNumber { get; set; }
        
        public int PersonalCode { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}