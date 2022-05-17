using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace service
{
    public class MongoSilverConnection
    {
        public List<dynamic> connectionSilver(){
             return readAlldata();
        }
        internal List<dynamic> readAlldata(){
            List<dynamic> results = new List<dynamic>();
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://mirnasilver:yvOvHUcprUzZYGuU@mirnasilvertoken.zo7gk.mongodb.net/Mirnasilvertoken?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("tokensilver");
            var collection = database.GetCollection<BsonDocument>("tokens");
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