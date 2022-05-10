using System;
using System.Collections.Generic;

//Aqui fica somente operaçõe diretas do banco de dados!!  FLAGS NÂO FICAM AQUI NEM SERIALIZATION
namespace Mirna{
    public class DataBaseOperation
    {
        //Adicionar aqui as operações que serão digitadas no Terminal
        //Adicionar aqui umAc vertor de provaveis banco de dados a serem utilizados
        protected List<dynamic> _ProviderListAccess = new List<dynamic>();
        public void EntryQuery(string Query){
            //Aqui fica o metodo onde executa a Query que saiu do Banco de dados
        }
        public void CreateDatabaseAccess(dynamic DataProvider,string NickProvider){
            _ProviderListAccess.Add(NickProvider);
            _ProviderListAccess.Add(DataProvider);
        }
        public void Configure(){

        }
    }
}