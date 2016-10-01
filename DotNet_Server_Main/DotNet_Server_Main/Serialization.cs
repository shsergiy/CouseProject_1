using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using ClientClasses;

namespace DotNet_Server_Main
{
    public class Serialization
    {
        public static byte[] ConvertObjectToBynaryArray(string Preparation,List<object> classObject)
        {
            WrapperBack p = new WrapperBack(Preparation,classObject);
            byte[] userDataBytes;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);
            userDataBytes = ms.ToArray();
            return userDataBytes;
        }
    }
}
