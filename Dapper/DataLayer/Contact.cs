using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace DataLayer
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }

        [Computed] // computed fields won't be manipulated for the db
        public bool IsNew => Id == default;

        [Write(false)] // there's no such column in the db, so we specify false to omit it from writing to db
        public List<Address> Addresses { get; } = new List<Address>();
    }
}
