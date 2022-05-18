using System;
using System.Collections.Generic;
using Interface;
using MongoDB.Driver;
using MongoDB.Bson;

namespace service{
    public class ReturnDataMongoDb : IReturnData
    {
        public dynamic ReturnAllData(string connection,string dataBase,string table,string type)
        {
            List<dynamic> results = new List<dynamic>();
            var settings = MongoClientSettings.FromConnectionString(connection);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(dataBase);
            var collection = database.GetCollection<BsonDocument>(table);
            var document = collection.Find(new BsonDocument()).ToList();
            var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var item in cursor.ToEnumerable())
            {
                results.Add(item);
            }
            return results;
        }
    }
}