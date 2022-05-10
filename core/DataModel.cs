using System.Collections.Generic;

namespace Mirna
{
    public class DataModel
    {
        public List<FilterData> DataFilterList = new List<FilterData>();
        public struct FilterData
        {
            public List<dynamic> Values {get;set;}
            public string NameData {get;set;}
        }
        public void CreateNewFilterModel(string DataName){
            List<dynamic> NewList = new List<dynamic>();
            DataFilterList.Add(new FilterData() { NameData = DataName ,Values = NewList });
        }
        public string FoundModelString(string columnName){
            List<dynamic> Valor= new List<dynamic>();
            FilterData match = DataFilterList.Find((FilterData x) => {return x.NameData == columnName; });
            return match.NameData;
        }
        public List<dynamic> FoundModelData(string columnName){
            List<dynamic> Valor= new List<dynamic>();
            FilterData match = DataFilterList.Find((FilterData x) => {return x.NameData == columnName; });
            return match.Values;
        }
        public dynamic ReturnValuesFromModelData(string ColumName, int IndexValue){
            int Index_Values = 0;
            while(DataFilterList.Count > Index_Values){
                if(DataFilterList[Index_Values].NameData == ColumName){
                    break;
                }
                Index_Values+=1;
            }
            return DataFilterList[Index_Values].Values[0][IndexValue];
        }
        public List<dynamic> ReturnAllValuesModelData(string ColumName){
            int Index_Values = 0;
            while(DataFilterList.Count > Index_Values){
                if(DataFilterList[Index_Values].NameData == ColumName){
                    break;
                }
                Index_Values+=1;
            }
            return DataFilterList[Index_Values].Values;
        }        
        
    }
}