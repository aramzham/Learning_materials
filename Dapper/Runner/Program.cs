﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DataLayer;
using Microsoft.Extensions.Configuration;

namespace Runner
{
    class Program
    {
        private static IConfigurationRoot _config;

        static void Main(string[] args)
        {
            Initialize();

            //GetAll_ShouldReturn6Results();

            //var id = Insert_should_assign_identity_to_new_entity();
            //Find_should_retrieve_existing_entity(id);
            //Modify_should_update_existing_entity(id);
            //Delete_should_remove_entity(id);

            //var repository = CreateRepository();
            //var mj = repository.GetFullContact(1);
            //mj.Output();

            //List_support_should_produce_correct_results();
            //Dynamic_support_should_produce_correct_results();

            //Bulk_insert_should_insert_4_rows();

            //GetIllinoisAddresses();

            Get_all_should_return_6_results_with_addresses();

            Get_all_should_return_6_results_mysql();
        }

        #region CRUD

        static void Get_all_should_return_6_results_mysql()
        {
            var repository = new ContactRepositoryMySql(_config.GetConnectionString("MySqlConnection"));

            // act
            var contacts = repository.GetAll();

            // assert
            Console.WriteLine($"Count: {contacts.Count}");
            Debug.Assert(contacts.Count == 6);
            contacts.Output();
        }

        static void Get_all_should_return_6_results_with_addresses()
        {
            var repository = CreateRepositoryEx();

            // act
            var contacts = repository.GetAllContactsWithAddresses();

            // assert
            Console.WriteLine($"Count: {contacts.Count}");
            contacts.Output();
            Debug.Assert(contacts.Count == 6);
            Debug.Assert(contacts[0].Addresses.Count == 2);
        }

        static void GetIllinoisAddresses()
        {
            // arrange
            var repository = CreateRepositoryEx();

            // act
            var addresses = repository.GetAddressesByState(17);

            // assert
            Debug.Assert(addresses.Count == 2);
            addresses.Output();
        }

        static void Bulk_insert_should_insert_4_rows()
        {
            // arrange
            var repository = CreateRepositoryEx();
            var contacts = new List<Contact>
            {
                new() { FirstName = "Charles", LastName = "Barkley" },
                new() { FirstName = "Scottie", LastName = "Pippen" },
                new() { FirstName = "Tim", LastName = "Duncan" },
                new() { FirstName = "Patrick", LastName = "Ewing" }
            };

            // act
            var rowsAffected = repository.BulkInsertContacts(contacts);

            // assert
            Console.WriteLine($"Rows inserted: {rowsAffected}");
            Debug.Assert(rowsAffected == 4);
        }

        static void Dynamic_support_should_produce_correct_results()
        {
            // arrange
            var repository = CreateRepositoryEx();

            // act
            var contacts = repository.GetDynamicContactsById(1, 2, 4);

            // assert
            Debug.Assert(contacts.Count == 3);
            Console.WriteLine($"First FirstName is: {contacts[0].FirstName}");
            contacts.Output();
        }

        static void List_support_should_produce_correct_results()
        {
            // arrange
            var repository = CreateRepositoryEx();

            // act
            var contacts = repository.GetContactsById(1, 2, 4);

            // assert
            Debug.Assert(contacts.Count == 3);
            contacts.Output();
        }

        static void Delete_should_remove_entity(int id)
        {
            // arrange
            IContactRepository repository = CreateRepository();

            // act
            repository.Remove(id);

            // create a new repository for verification purposes
            IContactRepository repository2 = CreateRepository();
            var deletedEntity = repository2.Find(id);

            // assert
            Debug.Assert(deletedEntity == null);
            Console.WriteLine("*** Contact Deleted ***");
        }

        static void Modify_should_update_existing_entity(int id)
        {
            // arrange
            IContactRepository repository = CreateRepository();

            // act
            //var contact = repository.Find(id);
            var contact = repository.GetFullContact(id);
            contact.FirstName = "Bob";
            contact.Addresses[0].StreetAddress = "456 Main Street";
            //repository.Update(contact);
            repository.Save(contact);

            // create a new repository for verification purposes
            IContactRepository repository2 = CreateRepository();
            //var modifiedContact = repository2.Find(id);
            var modifiedContact = repository2.GetFullContact(id);

            // assert
            Console.WriteLine("*** Contact Modified ***");
            modifiedContact.Output();
            Debug.Assert(modifiedContact.FirstName == "Bob");
            Debug.Assert(modifiedContact.Addresses[0].StreetAddress == "456 Main Street");
        }

        static void Find_should_retrieve_existing_entity(int id)
        {
            // arrange
            IContactRepository repository = CreateRepository();

            // act
            //var contact = repository.Find(id);
            var contact = repository.GetFullContact(id);

            // assert
            Console.WriteLine("*** Get Contact ***");
            contact.Output();
            Debug.Assert(contact.FirstName == "Joe");
            Debug.Assert(contact.LastName == "Blow");
            Debug.Assert(contact.Addresses.Count == 1);
            Debug.Assert(contact.Addresses[0].StreetAddress == "123 Main Street");
        }

        static int Insert_should_assign_identity_to_new_entity()
        {
            // arrange
            IContactRepository repository = CreateRepository();
            var contact = new Contact
            {
                FirstName = "Joe",
                LastName = "Blow",
                Email = "joe.blow@gmail.com",
                Company = "Microsoft",
                Title = "Developer"
            };
            var address = new Address
            {
                AddressType = "Home",
                StreetAddress = "123 Main Street",
                City = "Baltimore",
                StateId = 1,
                PostalCode = "22222"
            };
            contact.Addresses.Add(address);

            // act
            //repository.Add(contact);
            repository.Save(contact);

            // assert
            Debug.Assert(contact.Id != 0);
            Console.WriteLine("*** Contact Inserted ***");
            Console.WriteLine($"New ID: {contact.Id}");
            return contact.Id;
        }

        static void GetAll_ShouldReturn6Results()
        {
            // arrange
            var repository = CreateRepository();

            // act
            var contacts = repository.GetAll();

            // assert
            Console.WriteLine($"Count: {contacts.Count}");
            Debug.Assert(contacts.Count == 6);
            contacts.Output();
        } 
        #endregion

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
        }

        //private static IContactRepository CreateRepository() => new ContactRepository(_config.GetConnectionString("Default"));
        private static IContactRepository CreateRepository() => new ContactRepositorySp(_config.GetConnectionString("Default"));
        //private static IContactRepository CreateRepository() => new ContactRepositoryContrib(_config.GetConnectionString("Default"));

        private static ContactRepositoryEx CreateRepositoryEx() => new(_config.GetConnectionString("Default"));
    }
}
