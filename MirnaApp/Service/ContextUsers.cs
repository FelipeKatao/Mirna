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
        public string ContextAssemblyString(string token,string data)
        {
            //mongodb://mirnasilver:<password>@mirnasilvertoken
            List<dynamic> ConSilverdata = consilver.readAlldata();
             foreach (var item in ConSilverdata)
            {
                //Ele verifica qual o token de string padrão definido pela chave
                // StringData dentro do banco de dados da Mirna. 
                if(item[5] == token){
                   return data+token;
                }
            }
            return "Error the token not exists in actual context";
        }
    }
}