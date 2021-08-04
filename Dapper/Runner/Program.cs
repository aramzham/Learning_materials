using System;
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

            GetAll_ShouldReturn6Results();
        }

        private static void GetAll_ShouldReturn6Results()
        {
            // arrange
            var repo = CreateRepository();

            // act
            var contacts = repo.GetAll();

            // assert
            Console.WriteLine($"Count: {contacts.Count}");
            Debug.Assert(contacts.Count == 6);
            contacts.Output();
        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
        }

        private static IContactRepository CreateRepository() => new ContactRepository(_config.GetConnectionString("Default"));
    }
}
