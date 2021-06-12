using System;

namespace MongoDbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new MongoCRUD("AddressBook");
            //var person = new PersonModel()
            //{
            //    LastName = "Ohanyan",
            //    FirstName = "Petros",
            //    Address = new AddressModel()
            //    {
            //        City = "Yerevan",
            //        Country = "Armenia",
            //        Street = "Raykom tex",
            //        ZipCode = "0021 երևի"
            //    }
            //};

            //db.Insert("Users", person);

            var persons = db.ReadAll<PersonModel>("Users");
            foreach (var person in persons)
            {
                Console.WriteLine($"_id:{person.Id},  {person.FirstName} {person.LastName}");
                if (person.Address != null)
                {
                    Console.Write($" lives at {person.Address.ZipCode} {person.Address.City}, {person.Address.Country}, {person.Address.Street}");
                }

                Console.WriteLine();
            }

            var personById = db.GetById<PersonModel>("Users", Guid.Parse("f4cfa4c3-8cad-495c-9aa4-93bb7c4b25c5"));
            personById.DateOfBirth = new DateTime(2020, 9, 27, 0, 0, 0, DateTimeKind.Utc);
            db.Upsert("Users", personById.Id, personById);

            // if you are tired of Petros, uncomment this line
            // db.Delete<PersonModel>("Users", personById.Id);

            // if you don't want other fields but names => use such a model
            var nameModels = db.ReadAll<NameModel>("Users");

            Console.ReadLine();
        }
    }
}
