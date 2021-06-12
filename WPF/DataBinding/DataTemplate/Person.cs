using System.Collections.Generic;

namespace DataTemplate
{
    public class Person
    {
        public int Age { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Position { get; set; }

        public Person(int age, string firstName, string lastName, string position)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
        }
    }

    public class PersonList : List<Person>
    {
        public PersonList()
        {
            this.Add(new Person(20,"Adam", "Evans", "blogger"));
            this.Add(new Person(34,"Derek", "Alvarado", "trainer"));
            this.Add(new Person(24,"John", "Stockton", "fighter"));
            this.Add(new Person(27,"Ebecilio", "Martinez", "dumper"));
            this.Add(new Person(55,"Kerry", "Jones", "starter"));
        }
    }
}