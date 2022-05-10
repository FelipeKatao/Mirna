
using System.Collections.Generic;

namespace Mirna{
    public interface IdataSource{
        //inserir aqui as operaçõe de banco de dados
        string CreateConnection(string stringconnection,string database);
        string CreateConnectionData(string stringconnection,string database);
        DataModel ReadAllDocument(string document, string databaseTarget,DataModel DataModelTarge);
        public string ReadSelectFieldsDocument(string databaseTarget,string documnent, string[] fields);
        string InsertValueDocument(string databaseTarget,dynamic document, dynamic valueToInsert,dynamic fields);
        DataModel ReadDocumentWhere(DataModel datamodel,string databaseTarget, string document, string field, string value);
        string ReadFristDocument(string databaseTarget,string document);
        void DeleteDOcument(string databaseTarget,string document);
        List<dynamic> ColectAllcolumns(List<dynamic> genericDynamic,string document,string DataBaseTarget);
        string UpdateValueDocument(string databaseTarget,string document,string field,string fieldModify,dynamic wherevalue,dynamic oldValue,dynamic newValue);
        
    }
}