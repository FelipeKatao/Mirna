using System;
using System.Collections.Generic;
using Interface;

namespace service{
    public class ContextUsers : IcontextUser
    {
        service.MongoSilverConnection consilver = new service.MongoSilverConnection();
        public struct Context_users{
            bool silverValidation;
            public bool SilverValidation(bool value){
                this.silverValidation =value;
                return this.silverValidation;
            }
        }
        public Context_users _configurationCon = new Context_users();
        public bool SilverValidate(string token)
        {
            foreach (var item in consilver.connectionSilver())
            {
                if(item[2] == token){
                   return _configurationCon.SilverValidation(true);
                }
            }
           return  _configurationCon.SilverValidation(false);
        }
    }
}