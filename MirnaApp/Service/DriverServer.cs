using System;
using service;

namespace service
{
    public class DriverService
    {
        private delegate dynamic Drivers(string connection,string dataBase,string table,string type);

        MongoSilverConnection mongCon = new MongoSilverConnection();
        ReturnDataMongoDb MongoDbReturn = new ReturnDataMongoDb();
       
        public void DriverCall(string connection,string dataBase,string table,string type)
        {
            Drivers cda = new Drivers(MongoDbReturn.ReturnAllData);
            List<dynamic> DriversAcess = new List<dynamic>()
            {
                "Mongo",
                cda
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
            DriversAcess[index_driver](connection,dataBase,table,type);
        }
    }
}