using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public class ContactRepositoryMySql
    {
        private readonly IDbConnection _db;

        public ContactRepositoryMySql(string connString)
        {
            _db = new MySqlConnection(connString);
        }

        public List<Contact> GetAll()
        {
            // dapper works directly with db provider => C# code will still be the same
            // you'll just need to tweak the sql dialect accordingly
            return _db.Query<Contact>("SELECT * FROM Contacts").ToList();
        }
    }
}
