using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;

namespace DataLayer
{
    public class ContactRepositorySp : IContactRepository
    {
        private readonly IDbConnection _db;

        public ContactRepositorySp(string connString)
        {
            _db = new SqlConnection(connString);
        }

        public Contact Add(Contact contact)
        {
            var id = _db.Execute("AddContact", contact, commandType: CommandType.StoredProcedure);
            contact.Id = id;

            return contact;
        }

        public Contact Find(int id)
        {
            // even thought GetContact returns 2 results we'll take only one by SingleOrDefault()
            return _db.Query<Contact>("GetContact", new { Id = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public List<Contact> GetAll()
        {
            return _db.Query<Contact>("GetAllContacts", commandType: CommandType.StoredProcedure).ToList();
        }

        public Contact GetFullContact(int id)
        {
            using var multipleResults = _db.QueryMultiple("GetContact", new { Id = id }, commandType: CommandType.StoredProcedure);
            var contact = multipleResults.Read<Contact>().SingleOrDefault();

            var addresses = multipleResults.Read<Address>().ToList();
            contact?.Addresses.AddRange(addresses);

            return contact;
        }

        public void Remove(int id)
        {
            _db.Execute("DeleteContact", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public void Save(Contact contact)
        {
            using var txScope = new TransactionScope();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: contact.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@FirstName", contact.FirstName);
            parameters.Add("@LastName", contact.LastName);
            parameters.Add("@Company", contact.Company);
            parameters.Add("@Title", contact.Title);
            parameters.Add("@Email", contact.Email);
            _db.Execute("SaveContact", parameters, commandType: CommandType.StoredProcedure);
            contact.Id = parameters.Get<int>("@Id");

            foreach (var addr in contact.Addresses.Where(a => !a.IsDeleted))
            {
                addr.ContactId = contact.Id;

                var addrParams = new DynamicParameters(new
                {
                    addr.ContactId,
                    addr.AddressType,
                    addr.StreetAddress,
                    addr.City,
                    addr.StateId,
                    addr.PostalCode
                });
                addrParams.Add("@Id", addr.Id, DbType.Int32, ParameterDirection.InputOutput);
                _db.Execute("SaveAddress", addrParams, commandType: CommandType.StoredProcedure);
                addr.Id = addrParams.Get<int>("@Id");
            }

            foreach (var addr in contact.Addresses.Where(a => a.IsDeleted))
            {
                _db.Execute("DeleteAddress", new {addr.Id }, commandType: CommandType.StoredProcedure);
            }

            txScope.Complete();
        }

        public Contact Update(Contact contact)
        {
            _db.Execute("UpdateContact", contact, commandType: CommandType.StoredProcedure);
            return contact;
        }
    }
}
