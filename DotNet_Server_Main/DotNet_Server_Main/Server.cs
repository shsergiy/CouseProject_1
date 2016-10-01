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
    class Server
    {  
        #region Settings
        private static int Port = 80;
        private static IPAddress Ip = IPAddress.Parse("127.0.0.1");
        private static IPEndPoint IPEndPoint = new IPEndPoint(Ip, Port);
        private static TcpListener serverSocket = new TcpListener(Ip, Port);
        private int MAXCOUNT = Environment.ProcessorCount * 10;
        private static string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Sergiy\Documents\visual studio 2013\Projects\DotNet_Server_Main\DotNet_Server_Main\Database_Main.mdf;Integrated Security=True;Connect Timeout=30";
        static List<TcpClient> Clients = new List<TcpClient>();
        #endregion

        public Server()
        {
            try 
            {
                WriteInfo("Welcome. Server has been loaded");
                serverSocket.Start();
                while (true)
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();
                    WriteInfo("Clients #" + Clients.Count + " . Start processing");
                    Clients.Add(clientSocket);
                    new Thread(new ParameterizedThreadStart(ProcessingRequest)).Start(clientSocket);
                }
                serverSocket.Stop();
            }
            catch (SocketException ex)
            {
                WriteInfo(ex);
            }
            catch (Exception ex)
            {
                WriteInfo(ex);
            }
        }
        private void ProcessingRequest(object client)
        {
            TcpClient CurrentClient = client as TcpClient;
            byte[] Buffer = new byte[100000];
            NetworkStream networkStream = CurrentClient.GetStream();
            while ((true))
            {
                try 
                {
                    networkStream.Read(Buffer, 0, (int)CurrentClient.ReceiveBufferSize);
                    MemoryStream ms = new MemoryStream(Buffer);
                    BinaryFormatter bf1 = new BinaryFormatter();
                    ms.Position = 0;
                    Wrapper ComandAndInfo = (Wrapper)bf1.Deserialize(ms);
                    if (ComandAndInfo.Command=="SELECTALL")
                    {
                        if (ComandAndInfo.TypeOfClass == "Patient")
                        {
                            byte[] answer = DataServer.SelectAllPatients();
                            networkStream.Write(answer, 0, answer.Length); 
                        }
                        else if (ComandAndInfo.TypeOfClass == "Doctor")
                        {
                            byte[] answer = DataServer.SelectAllDoctors();
                            networkStream.Write(answer, 0, answer.Length); 
                        }
                        else if (ComandAndInfo.TypeOfClass == "Illness")
                        {
                            byte[] answer = DataServer.SelectAllIllness();
                            networkStream.Write(answer, 0, answer.Length);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Preparation")
                        {
                           byte[] answer= DataServer.SelectAllPreparation();
                           networkStream.Write(answer, 0, answer.Length);
                        }
                    }
                    else if (ComandAndInfo.Command == "ADD")
                    {
                        if (ComandAndInfo.TypeOfClass=="Patient")
                        {
                            DataServer.AddPatient((Patient)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Doctor")
                        {
                            DataServer.AddDoctor((Doctor)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Illness")
                        {
                            DataServer.AddIllness((Illness)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass=="Preparation")
                        {
                            DataServer.AddPreparation((Preparation)ComandAndInfo.ObjClass);
                        }
                        
                    }
                    else if (ComandAndInfo.Command=="DELETE")
                    {
                        if (ComandAndInfo.TypeOfClass == "Patient")
                        {
                            DataServer.DeletePatient((Patient)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Doctor")
                        {
                            DataServer.DeleteDoctor((Doctor)ComandAndInfo.ObjClass);

                        }
                        else if (ComandAndInfo.TypeOfClass == "Illness")
                        {
                            DataServer.DeleteIllness((Illness)ComandAndInfo.ObjClass);

                        }
                        else if (ComandAndInfo.TypeOfClass == "Preparation")
                        {
                            DataServer.DeletePreparation((Preparation)ComandAndInfo.ObjClass);
                        }
                        
                    }
                    else if (ComandAndInfo.Command == "Update")
                    {
                        if (ComandAndInfo.TypeOfClass == "Patient")
                        {
                            DataServer.UpdatePatient((Patient)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Doctor")
                        {
                            DataServer.UpdateDoctor((Doctor)ComandAndInfo.ObjClass);
                        }
                        else if (ComandAndInfo.TypeOfClass == "Illness")
                        {
                            DataServer.UpdateIllenss((Illness)ComandAndInfo.ObjClass);
                        }
                    }
                    networkStream.Flush();
                }
                catch (Exception ex)
                {
                    WriteInfo(ex);
                    
                }
            }

        }
        private void WriteInfo(dynamic msg)
        {
            Console.WriteLine(msg);
        }
    }
}
