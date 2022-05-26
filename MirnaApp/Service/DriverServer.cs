using System;
using service;
using QueryEditor;

namespace service
{
    public class DriverService
    {
        private delegate dynamic Drivers(string connection,string dataBase,string table,string type);
        private delegate dynamic Driver_Assembly(dynamic value);

        MongoSilverConnection mongCon = new MongoSilverConnection();
        ReturnDataMongoDb MongoDbReturn = new ReturnDataMongoDb();
       
        public dynamic DriverCall(string connection,string dataBase,string table,string type,string query)
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
            if(query.ToLower()!="null")
            {
                Result  = new QueryExecuteMirna(query,Result);
                string[] QueryData = query.Split('-');
                //A partir daqui fazer o tratamento caso precise fazer as querys
            }
            return DriversAcess[index_driver+1](Result);
        }
    }
}