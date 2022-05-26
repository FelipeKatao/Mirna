using System;

namespace Interface 
{
    public interface IdataServer
    {
        public List<dynamic> readAlldata();
        public dynamic AssemblyData(dynamic value);
    }
}