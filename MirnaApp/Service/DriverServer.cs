using System;
using service;

namespace service
{
    public class DriverService
    {
        private delegate List<dynamic> Drivers();

        MongoSilverConnection mongCon = new MongoSilverConnection();
       
        public void DriverCall(string type)
        {
            Drivers cda = new Drivers(mongCon.readAlldata);
            List<dynamic> DriversAcess = new List<dynamic>()
            {
                "Mongo",
                cda
            };
            foreach (var item in DriversAcess)
            {
                if(item == "MONGO")
                {
                    
                }
            }
        }
    }
}