using MongoDB.Driver;
using MongoDB.Bson;
using Spectre.Console;
using System.Collections.Generic;

namespace Mirna
{
    public class DbAccess : IdataSource
    {
        public DbAccess(string connection, string database, string document)
        {
            _connection_str = connection;
            _database_str = database;
            _document_str = document;
            _client_mgcli = new MongoClient(connection);
        }
        public string _connection_str = "";
        public string _database_str = "";
        public string _document_str = "";
        public string _queryResult_str = "";
        private MongoClient _client_mgcli { get; set; }
        private dynamic[] _queryConection_dy { get; set; }

        public string CreateConnectionData(string stringconnection, string database)
        {
            _client_mgcli = new MongoClient(stringconnection);
            _database_str = database;
            return "Create Connection";
        }
        public DataModel ReadAllDocument(string document, string databaseTarget,DataModel DataModelTarget)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            var documentObject = Collection.Find(new BsonDocument()).ToList();
            var cursor = Collection.Find(new BsonDocument()).ToCursor();

            List<dynamic> ListReturn = new List<dynamic>();
            foreach (var item in cursor.ToEnumerable())
            {
                List<dynamic> ValueData = new List<dynamic>();
                foreach(var itemValue in item){
                    ValueData.Add(itemValue.Value);
                }
                ListReturn.Add(ValueData);       
            }

            List<dynamic> Columns = new List<dynamic>();
            ColectAllcolumns(Columns,document,databaseTarget);
            foreach(var item in Columns){
                DataModelTarget.CreateNewFilterModel(item);
            }
            int Index_Values = 0;
            while(Columns.Count > Index_Values)
            {
                if(Columns[Index_Values] == DataModelTarget.DataFilterList[Index_Values].NameData)
                {
                    DataModelTarget.DataFilterList[Index_Values].Values.Add(ListReturn[Index_Values]);
                }
                Index_Values+=1;
            }
            return DataModelTarget;
        }

        public List<dynamic> ColectAllcolumns(List<dynamic> genericDynamic,string document,string DataBaseTarget)
        {
            var database = _client_mgcli.GetDatabase(DataBaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            var fristDocument = Collection
            .Find(new BsonDocument()).FirstOrDefault();
            foreach(var item in fristDocument)
            {
                genericDynamic.Add(item.Name);
            }
            return genericDynamic;
        }

        private List<dynamic> _ReadColumns(string document, string databaseTarget, DataModel DataModelTarget)
        {
            List<dynamic> Columns = new List<dynamic>();
            ColectAllcolumns(Columns,document,databaseTarget);
            foreach(var item in Columns){
                DataModelTarget.CreateNewFilterModel(item);
            }
            return Columns;
        }
        private DataModel _AddValues(List<dynamic> Columns, DataModel DataModelTarget,List<dynamic> ListReturn)
        {
            //Ajustar aqui para receber mais de um valor 
            int Index_Values = 0;
            while(Columns.Count > Index_Values)
            {
                if(Columns[Index_Values] == DataModelTarget.DataFilterList[Index_Values].NameData)
                {
                    DataModelTarget.DataFilterList[Index_Values].Values.Add(ListReturn[Index_Values]);
                }
                Index_Values+=1;
            }
            return DataModelTarget;
        }

        public string ReadSelectFieldsDocument(string databaseTarget,string documnent, string[] fields)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(documnent);
            var projection= Builders<BsonDocument>.Projection.Exclude("");
            foreach (var item in fields)
            {
                projection = Builders<BsonDocument>.Projection.Exclude("item");
            }
            var document = Collection.Find(new BsonDocument()).Project(projection).First();
            return documnent;
        }

        public string InsertValueDocument(string databaseTarget, dynamic document, dynamic valueToInsert,dynamic fields)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            int Index=0;
            var documentAdd = new BsonDocument();
            foreach(var item in fields){
                documentAdd.Add(item,valueToInsert[Index]);
                Index+=1;
            } 
            Collection.InsertOne(documentAdd);
            return "Registro Inserido com Sucesso!";
        }

        public string CreateConnection(string stringconnection, string database)
        {
            throw new System.NotImplementedException();
        }

        public DataModel ReadDocumentWhere(DataModel datamodel,string databaseTarget, string document, string field, string value)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            var documentObject = Collection.Find(filter).ToList();
            List<dynamic> Columns = _ReadColumns(document,databaseTarget,datamodel);
            List<dynamic> ValueData = new List<dynamic>();
            foreach (var item in documentObject)
            {
                
                foreach(var itemValue in item){
                    ValueData.Add(itemValue.Value);
                }
                _AddValues(Columns,datamodel,ValueData);
            }
            return datamodel;
        }

        public string ReadFristDocument(string databaseTarget, string document)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            var fristDocument = Collection.Find(new BsonDocument()).FirstOrDefault();
            return fristDocument.ToString();
        }
        public string UpdateValueDocument(string databaseTarget,string document,string field,string fieldModify,dynamic wherevalue,dynamic oldValue,dynamic newValue)
        {
            //Colocar  aqui uma condicional para fazer o WHERE 
            //verificar se existe e se existir da o Update espcifico ou se não houver where só fazer o update
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);  
            var filter = Builders<BsonDocument>.Filter.Eq(field,oldValue);
            var update = Builders<BsonDocument>.Update.Set(field,newValue);
            Collection.UpdateOne(filter,update);
            return "Update apply!!";
        }
        //Implementar aqui o metodo string ConsultAllDocument(string DocumentName)  
        public void DeleteDOcument(string databaseTarget,string document)
        {
            var database = _client_mgcli.GetDatabase(databaseTarget);
            var Collection = database.GetCollection<BsonDocument>(document);
            var fristDocument = Collection.Find(new BsonDocument()).FirstOrDefault();
           // var filter = Builders<BsonDocument>.Filter.eq("","");
        }
        //Onde todo o documento é decorrido 
    }
}