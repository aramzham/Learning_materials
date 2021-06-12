using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbDemo
{
    public class MongoCRUD
    {
        private IMongoDatabase _db;

        public MongoCRUD(string dbName)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(dbName);
        }

        public void Insert<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public IEnumerable<T> ReadAll<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList(); // when giving a blank bson => means select *
        }

        public T GetById<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        public void Upsert<T>(string table, Guid id, T record)
        {
            var collection = _db.GetCollection<T>(table);
            var result = collection.ReplaceOne(new BsonDocument("_id", id), record, new UpdateOptions() { IsUpsert = true });
        }

        public void Delete<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}