using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;

namespace DataLayer
{
    public class ContactRepository : IContactRepository
    {
        private IDbConnection _db;

        public ContactRepository(string connString)
        {
            _db = new SqlConnection(connString);
        }

        public Contact Add(Contact contact)
        {
            var sql =
                "INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES(@FirstName, @LastName, @Email, @Company, @Title); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _db.Query<int>(sql, contact).Single();
            contact.Id = id;
            return contact;
        }

        public Contact Find(int id)
        {
            return _db.Query<Contact>("SELECT * FROM Contacts WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public Contact GetFullContact(int id)
        {
            var sql =
                "SELECT * FROM Contacts WHERE Id = @Id; " +
                "SELECT * FROM Addresses WHERE ContactId = @Id";

            using (var multipleResults = _db.QueryMultiple(sql, new { Id = id }))
            {
                var contact = multipleResults.Read<Contact>().SingleOrDefault();

                var addresses = multipleResults.Read<Address>().ToList();
                if (contact != null && addresses != null)
                {
                    contact.Addresses.AddRange(addresses);
                }

                return contact;
            }
        }

        public void Save(Contact contact)
        {
            using var txScope = new TransactionScope();

            if (contact.IsNew)
            {
                Add(contact);
            }
            else
            {
                Update(contact);
            }

            foreach (var addr in contact.Addresses.Where(a => !a.IsDeleted))
            {
                addr.ContactId = contact.Id;

                if (addr.IsNew)
                {
                    Add(addr);
                }
                else
                {
                    Update(addr);
                }
            }

            foreach (var addr in contact.Addresses.Where(a => a.IsDeleted))
            {
                _db.Execute("DELETE FROM Addresses WHERE Id = @Id", new { addr.Id });
            }

            txScope.Complete();
        }

        public Address Add(Address address)
        {
            var sql =
                "INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(@ContactId, @AddressType, @StreetAddress, @City, @StateId, @PostalCode); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            // here we call query because in the end we do select for the id 
            var id = _db.Query<int>(sql, address).Single();
            address.Id = id;
            return address;
        }

        public Address Update(Address address)
        {
            _db.Execute("UPDATE Addresses " +
                "SET AddressType = @AddressType, " +
                "    StreetAddress = @StreetAddress, " +
                "    City = @City, " +
                "    StateId = @StateId, " +
                "    PostalCode = @PostalCode " +
                "WHERE Id = @Id", address);
            return address;
        }

        public List<Contact> GetAll()
        {
            // explicitly give alias names in query if your code objects' names don't match to ones in _db
            // SELECT FirstName as FName, LastName... FROM Contacts
            return _db.Query<Contact>("SELECT * FROM Contacts").ToList();
        }

        public void Remove(int id)
        {
            _db.Execute("DELETE FROM Contacts WHERE Id = @Id", new { id });
        }

        public Contact Update(Contact contact)
        {
            var sql =
                "UPDATE Contacts " +
                "SET FirstName = @FirstName, " +
                "    LastName  = @LastName, " +
                "    Email     = @Email, " +
                "    Company   = @Company, " +
                "    Title     = @Title " +
                "WHERE Id = @Id";
            _db.Execute(sql, contact);
            return contact;
        }
    }
}
