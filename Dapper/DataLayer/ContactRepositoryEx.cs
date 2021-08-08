using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class ContactRepositoryEx
    {
        private readonly IDbConnection _db;

        public ContactRepositoryEx(string connString)
        {
            _db = new SqlConnection(connString);
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            var contacts = await _db.QueryAsync<Contact>("SELECT * FROM Contacts");
            return contacts.ToList();
        }

        public List<Contact> GetAllContactsWithAddresses()
        {
            var sql = "SELECT * FROM Contacts AS C INNER JOIN Addresses AS A ON A.ContactId = C.Id";

            var contactDict = new Dictionary<int, Contact>();

            // Contact - first table, Address - second table, Contact - return type
            var contacts = _db.Query<Contact, Address, Contact>(sql, (contact, address) =>
            {
                if (!contactDict.TryGetValue(contact.Id, out var currentContact))
                {
                    currentContact = contact;
                    contactDict.Add(currentContact.Id, currentContact);
                }

                currentContact.Addresses.Add(address);
                return currentContact;
            });

            // you'll get as many contacts as you had rows in your query result => need to distinct it
            return contacts.Distinct().ToList();
        }

        public List<Address> GetAddressesByState(int stateId)
        {
            // int and bool literal notation
            return _db.Query<Address>("SELECT * FROM Addresses WHERE StateId = {=stateId}", new { stateId }).ToList();
        }

        public int BulkInsertContacts(List<Contact> contacts)
        {
            // you just specify 1 insert row and dapper will turn it to 4 insert statements for each of list element
            // here we'll have 4 round-trips to db, so there's no performance optimization going on, just a query optimization
            var sql =
                "INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES(@FirstName, @LastName, @Email, @Company, @Title); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            return _db.Execute(sql, contacts);
        }

        public List<Contact> GetContactsById(params int[] ids)
        {
            // you don't need () in the clause, it will be added automatically
            return _db.Query<Contact>("SELECT * FROM Contacts WHERE ID IN @Ids", new { Ids = ids }).ToList();
        }

        public List<dynamic> GetDynamicContactsById(params int[] ids)
        {
            return _db.Query("SELECT * FROM Contacts WHERE ID IN @Ids", new { Ids = ids }).ToList();
        }
    }
}
