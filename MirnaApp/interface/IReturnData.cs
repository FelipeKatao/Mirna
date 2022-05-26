using System;

namespace Interface {
    public interface IReturnData
    {
        public dynamic ReturnAllData(string connection,string dataBase,string table,string type);
        public dynamic AssemblyData(dynamic value);
    }
}