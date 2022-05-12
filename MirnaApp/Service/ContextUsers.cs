using System;
using System.Collections.Generic;
using Interface;

namespace service{
    public class ContextUsers : IcontextUser
    {
        service.MongoSilverConnection consilver = new service.MongoSilverConnection();
        public struct User_context
        {
            bool SilverValidate;
            string TokenAcess;
            public User_context(){
                bool silverVal = false;
                this.SilverValidate = false;
                this.TokenAcess= "";
            }
        }
        public User_context UserData= new User_context();
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