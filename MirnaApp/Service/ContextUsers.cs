using System;
using System.Collections.Generic;
using Interface;

namespace service{
    public class ContextUsers : IcontextUser
    {
        service.MongoSilverConnection consilver = new service.MongoSilverConnection();
        
        public bool SilverValidate(string token)
        {
            foreach (var item in consilver.connectionSilver())
            {
                if(item[2] == token){
                   return true;
                }
            }
           return  false;
        }
    }
}