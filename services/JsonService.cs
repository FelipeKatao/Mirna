using System;
using System.Collections.Generic;

namespace Mirna{
    public class JsonService{
        public List<dynamic> ReadColluns(List<dynamic> ListTarget,string Source){
            //{ "_id" : ObjectId("61396c78f93a168084ef3e04"),
            // "Name" : "MirnaBeta", 
            //"ID[key]" : 3424, 
            //"Description" : "NoSQL with relationshiop basics" }
            int index=0;
            string ColumName="";
            while(Source.Length>=index){
                if(Source[index]=='"'){
                    index+=1;
                    while(Source[index]!='"'){
                        ColumName+=Source[index];
                        index+=1;
                    }
                    while(Source[index]!=','){
                        index+=1;
                        if(index>=Source.Length){
                            break;
                        }
                        
                    }
                    if(ColumName!=""){
                        ListTarget.Add(ColumName);
                        ColumName="";
                    }
                }
                index+=1;
              }
            return ListTarget;
        }
    }

}