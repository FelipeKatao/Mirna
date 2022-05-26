using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Interface;

namespace service
{
    public class MongoSilverConnection : IdataServer
    {
        public List<dynamic> connectionSilver()
        {
            return readAlldata();
        }

        public List<dynamic> readAlldata()
        {
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
        public dynamic AssemblyData(dynamic value)
        {
            string Data = "{\"Data\":{";
            int DataBaseIndex = 0;

            foreach (var item in value)
            {
                Data += "\"Value " + DataBaseIndex + "\":[{";
                foreach (var itemX in item)
                {
                    Data += "\"" + itemX.Name + "\":";
                    Data += "\"" + itemX.Value + "\",";
                }
                Data = Data.Remove(Data.Length - 1);
                Data += "}],";
                DataBaseIndex += 1;
            }
            Data = Data.Remove(Data.Length - 1);
            Data += "}}";
            return Data;
        }
    }
}