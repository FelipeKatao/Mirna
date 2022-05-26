using System;
using service;

namespace service
{
    public class DriverService
    {
        private delegate dynamic Drivers(string connection,string dataBase,string table,string type);
        private delegate dynamic Driver_Assembly(dynamic value);

        MongoSilverConnection mongCon = new MongoSilverConnection();
        ReturnDataMongoDb MongoDbReturn = new ReturnDataMongoDb();
       
        public dynamic DriverCall(string connection,string dataBase,string table,string type)
        {
            Drivers ReturnMongo = new Drivers(MongoDbReturn.ReturnAllData);
            Driver_Assembly MongoAssembly = new Driver_Assembly(MongoDbReturn.AssemblyData);
            List<dynamic> DriversAcess = new List<dynamic>()
            {
                "MONGO",
                ReturnMongo,
                MongoAssembly
            };
            int index_driver = 0;
            foreach (var item in DriversAcess)
            {
                if(item == type)
                {
                    index_driver=+1;
                    break;
                }
                index_driver+=1;
            }
            var Result = DriversAcess[index_driver](connection,dataBase,table,type);
            return DriversAcess[index_driver+1](Result);
        }
    }
}