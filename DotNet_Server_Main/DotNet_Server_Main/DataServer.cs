using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ClientClasses;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

namespace DotNet_Server_Main
{
   public class DataServer
    {
       private static string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Sergiy\Documents\visual studio 2013\Projects\DotNet_Server_Main\DotNet_Server_Main\Database_Main.mdf;Integrated Security=True;Connect Timeout=30";
        #region SelectFunctions
        public static byte[] SelectAllPatients()
       {
           List<object> patientssend = new List<object>();
            List<object> patients = new List<object>();
            string query = @"Select * from [Patient]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                
                                Patient pat = new Patient();
                                pat.id = reader.GetString(reader.GetOrdinal("Patient_Id"));
                                pat.Patientname = (reader.GetString(reader.GetOrdinal("PatientName")));
                                pat.Patientsurname = (reader.GetString(reader.GetOrdinal("PatientSurname")));
                                pat.Patientage = (reader.GetString(reader.GetOrdinal("PatientAge")));
                                pat.Patientproblems = (reader.GetString(reader.GetOrdinal("PatientProblems")));
                                pat.Patienttelephone = (reader.GetString(reader.GetOrdinal("PatientTelephone")));
                                pat.Patientadress = (reader.GetString(reader.GetOrdinal("PatientAdress")));
                                pat.Patientpass = (reader.GetString(reader.GetOrdinal("PatientPass")));
                                pat.Doctors = new List<string>();
                                pat.Illnesses = new List<string>();
                                patients.Add(pat);
                            }
                        }
                    }
                 }
             }
            try
            {
                List<Patient> s1 = new List<Patient>();
                foreach (object translete in patients)
                {
                    s1.Add((Patient)translete);
                }
                foreach (Patient temp in s1)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand idcommand = new SqlCommand(@"SELECT Illness_Id,Patient_Id FROM [PatientToIllness] WHERE Patient_Id='" + temp.id + "'", connection))
                        {
                            connection.Open();
                            using (SqlDataReader idreader = idcommand.ExecuteReader())
                            {
                                if (idreader.HasRows)
                                {
                                    while (idreader.Read())
                                    {
                                        temp.Illnesses.Add(idreader.GetString(idreader.GetOrdinal("Illness_Id")));

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                List<Patient> s1 =new  List<Patient>();
                foreach (object translete in patients)
                {
                    s1.Add((Patient)translete);
                    
                }
                
                foreach (Patient temp in s1)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand idcommand = new SqlCommand(@"SELECT Doctor_Id FROM [PatientToDoctor] WHERE Patient_Id='" + temp.id + "'", connection))
                        {
                            connection.Open();
                            using (SqlDataReader idreader = idcommand.ExecuteReader())
                            {
                                if (idreader.HasRows)
                                {
                                    while (idreader.Read())
                                    {

                                     
                                        temp.Doctors.Add(idreader.GetString(idreader.GetOrdinal("Doctor_Id")).Trim());

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Serialization.ConvertObjectToBynaryArray("Patient",patients);
        }
        public static byte[] SelectAllDoctors()
        {
            List<object> Doctors = new List<object>();
            string query = @"Select * from  [Doctor]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Doctor doc = new Doctor();
                                doc.index =(reader.GetString(reader.GetOrdinal("Doctor_Id")));
                                doc.Doctorname = (reader.GetString(reader.GetOrdinal("DoctorName")));
                                doc.Doctorsurname = (reader.GetString(reader.GetOrdinal("DoctorSurname")));
                                doc.Doctorage = (reader.GetString(reader.GetOrdinal("DoctorAge")));
                                doc.Doctoryear=(reader.GetString(reader.GetOrdinal("DoctorYear")));
                                doc.Doctorprofesion = (reader.GetString(reader.GetOrdinal("DoctorProfesion")));
                                doc.Doctorphone = (reader.GetString(reader.GetOrdinal("DoctorPhone")));
                                Doctors.Add(doc);
                            }
                        }
                    }
                }
            }
            return Serialization.ConvertObjectToBynaryArray("Doctor",Doctors);
        }
        public static byte[] SelectAllIllness()
        {
            List<object> illnesss = new List<object>();
            string query = @"SELECT * FROM [Illness]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Illness Ill = new Illness();
                                Ill.index = (reader.GetString(reader.GetOrdinal("Illness_Id")));
                                Ill.Nameofill = (reader.GetString(reader.GetOrdinal("IllnessName")));
                                Ill.Simptoms = (reader.GetString(reader.GetOrdinal("IllnessSimptoms")));
                                Ill.Nodus = (reader.GetString(reader.GetOrdinal("IllnessNodus")));
                                illnesss.Add(Ill);
                            }
                        }
                    }
                }
            }
            return Serialization.ConvertObjectToBynaryArray("Illness",illnesss); 
        }

        public static byte[] SelectAllPreparation()
        {
            List<object> preparations = new List<object>();
            string query = @"SELECT * FROM [Preparation]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Preparation prep = new Preparation();
                                prep.index = (reader.GetString(reader.GetOrdinal("Preparation_Id")));
                                prep.Name = (reader.GetString(reader.GetOrdinal("PreparationName")));
                                prep.Contraindications = (reader.GetString(reader.GetOrdinal("PreparationContraindcations")));
                                prep.Date = (reader.GetString(reader.GetOrdinal("PreparationDate")));
                                prep.Cost = (reader.GetString(reader.GetOrdinal("PreparationCost")));
                                prep.Manufactor = (reader.GetString(reader.GetOrdinal("PreparationManufactor")));
                                preparations.Add(prep);
                            }
                        }
                    }
                }
            }
            return Serialization.ConvertObjectToBynaryArray("Preparation",preparations);
        }


        #endregion 
        #region AddFunctions
        public static void AddPatient(Patient obj)
        {
            string query = @"insert into [Patient](Patient_Id,PatientName,PatientSurname,PatientAge,PatientProblems,PatientTelephone,PatientAdress,PatientPass) values (@Patient_Id,@PatientName,@PatientSurname,@PatientAge,@PatientProblems,@PatientTelephone,@PatientAdress,@PatientPass)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    command.Parameters.AddWithValue("@PatientName", obj.Patientname);
                    command.Parameters.AddWithValue("@PatientSurname", obj.Patientsurname);
                    command.Parameters.AddWithValue("@PatientAge", obj.Patientage);
                    command.Parameters.AddWithValue("@PatientProblems", obj.Patientproblems);
                    command.Parameters.AddWithValue("@PatientTelephone", obj.Patienttelephone);
                    command.Parameters.AddWithValue("@PatientAdress", obj.Patientadress);
                    command.Parameters.AddWithValue("@PatientPass", obj.Patientpass);
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                }
            }
            if(obj.Illnesses.Count !=0)
            {
                string queryList = @"insert into [PatientToIllness] (Patient_Id,Illness_Id) values (@Patient_Id,@Illness_Id)";
                obj.Illnesses.ForEach(s =>
                  {
                      using (SqlConnection connection = new SqlConnection(ConnectionString)) 
                      {
                          using(SqlCommand command = new SqlCommand(queryList,connection))
                          {
                              command.Parameters.AddWithValue("@Patient_Id", obj.id);
                              command.Parameters.AddWithValue("@Illness_Id", s);
                              connection.Open();
                              command.ExecuteNonQuery();
                          }
                      }
                  } );
            }
            if (obj.Doctors.Count !=0)
            {
                string querylist = @"insert into [PatientToDoctor] (Patient_Id,Doctor_Id) values (@Patient_Id,@Doctor_Id)";
             obj.Doctors.ForEach(s=>{
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                { 
                    using (SqlCommand command = new SqlCommand(querylist,connection))
                    {
                        command.Parameters.AddWithValue("@Patient_Id", obj.id);
                        command.Parameters.AddWithValue("@Doctor_Id", s);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
             });
            }
        }
        public static void AddDoctor(Doctor obj)
        {
            string query = @"insert into [Doctor](Doctor_Id,DoctorName,DoctorSurname,DoctorAge,DoctorYear,DoctorProfesion,DoctorPhone) values (@Doctor_Id,@DoctorName,@DoctorSurname,@DoctorAge,@DoctorYear,@DoctorProfesion,@DoctorPhone)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Doctor_Id", obj.index);
                    command.Parameters.AddWithValue("@DoctorName", obj.Doctorname);
                    command.Parameters.AddWithValue("@DoctorSurname", obj.Doctorsurname);
                    command.Parameters.AddWithValue("@DoctorAge", obj.Doctorage);
                    command.Parameters.AddWithValue("@DoctorYear", obj.Doctoryear);
                    command.Parameters.AddWithValue("@DoctorProfesion", obj.Doctorprofesion);
                    command.Parameters.AddWithValue("@DoctorPhone", obj.Doctorphone);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }
        public static void AddIllness(Illness obj)
        {
            string query = @"insert into [Illness](Illness_Id,IllnessName,IllnessSimptoms,IllnessNodus) values (@Illness_Id,@IllnessName,@IllnessSimptoms,@IllnessNodus)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Illness_Id", obj.index);
                    command.Parameters.AddWithValue("@IllnessName", obj.Nameofill);
                    command.Parameters.AddWithValue("@IllnessSimptoms", obj.Simptoms);
                    command.Parameters.AddWithValue("@IllnessNodus", obj.Nodus);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }
        public static void AddPreparation(Preparation obj)
        {
            string query = @"insert into [Preparation] (Preparation_Id,PreparationName,PreparationContraindcations,PreparationDate,PreparationCost,PreparationManufactor) values (@Preparation_Id,@PreparationName,@PreparationContraindcations,@PreparationDate,@PreparationCost,@PreparationManufactor)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                  
                    command.Parameters.AddWithValue("@Preparation_Id", obj.index);
                    command.Parameters.AddWithValue("@PreparationName", obj.Name);
                    command.Parameters.AddWithValue("@PreparationContraindcations", obj.Contraindications);
                    command.Parameters.AddWithValue("@PreparationDate", obj.Date);
                    command.Parameters.AddWithValue("@PreparationCost", obj.Cost);
                    command.Parameters.AddWithValue("@PreparationManufactor", obj.Manufactor);
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                    

                }
            }
        }

        #endregion
        #region DeleteFunctions
        public static void DeletePatient(Patient obj)
        {
            string query = @"DELETE FROM [Patient] WHERE Patient_Id=@Patient_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            query = @"DELETE FROM [PatientToIllness] WHERE Patient_Id=@Patient_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
                query = @"DELETE FROM [PatientToDoctor] WHERE Patient_Id=@Patient_Id";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {   
            using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                }
            }

        public static void DeleteDoctor(Doctor obj)
        {
            string query = @"delete from [Doctor] where Doctor_Id=@Doctor_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Doctor_Id", obj.index);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            query = @"DELETE FROM [PatientToDoctor] WHERE Doctor_Id=@Doctor_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Doctor_Id", obj.index);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteIllness(Illness obj)
        {
            string query = @"delete from [Illness] where Illness_Id=@Illness_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Illness_Id", obj.index);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            query = @"DELETE FROM [PatientToIllness] WHERE Illness_Id=@Illness_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Illness_Id", obj.index);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
       
        public static void DeletePreparation(Preparation obj)
        {
            string query = @"delete from [Preparation] where Preparation_Id=@Preparation_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Preparation_Id", obj.index);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
          
        }
        #endregion
        #region UpdateFunctions
        public static void UpdatePatient(Patient obj)
        {
            string query = @"update [Patient] set PatientName=@PatientName,PatientSurname=@PatientSurname,PatientAge=@PatientAge,PatientProblems=@PatientProblems,PatientTelephone=@PatientTelephone,PatientAdress=@PatientAdress,PatientPass=@PatientPass where Patient_Id=@Patient_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    command.Parameters.AddWithValue("@PatientName", obj.Patientname);
                    command.Parameters.AddWithValue("@PatientSurname", obj.Patientsurname);
                    command.Parameters.AddWithValue("@PatientAge", obj.Patientage);
                    command.Parameters.AddWithValue("@PatientProblems", obj.Patientproblems);
                    command.Parameters.AddWithValue("@PatientTelephone", obj.Patienttelephone);
                    command.Parameters.AddWithValue("@PatientAdress", obj.Patientadress);
                    command.Parameters.AddWithValue("@PatientPass", obj.Patientpass);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
            query = @"DELETE FROM [PatientToIllness] WHERE Patient_Id=@Patient_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            query = @"DELETE FROM [PatientToDoctor] WHERE Patient_Id=@Patient_Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_Id", obj.id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            if (obj.Illnesses.Count != 0)
            {
                string queryList = @"insert into [PatientToIllness] (Patient_Id,Illness_Id) values (@Patient_Id,@Illness_Id)";
                obj.Illnesses.ForEach(s =>
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(queryList, connection))
                        {
                            command.Parameters.AddWithValue("@Patient_Id", obj.id);
                            command.Parameters.AddWithValue("@Illness_Id", s);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                });
            }
            if (obj.Doctors.Count != 0)
            {
                string querylist = @"insert into [PatientToDoctor] (Patient_Id,Doctor_Id) values (@Patient_Id,@Doctor_Id)";
                obj.Doctors.ForEach(s =>
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(querylist, connection))
                        {
                            command.Parameters.AddWithValue("@Patient_Id", obj.id);
                            command.Parameters.AddWithValue("@Doctor_Id", s);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                });
            }
        }
        public static void UpdateDoctor(Doctor obj)
        {
            string query = @"update [Doctor] set DoctorName=@DoctorName,DoctorSurname=@DoctorSurname,DoctorAge=@DoctorAge,DoctorYear=@DoctorYear,DoctorProfesion=@DoctorProfesion,DoctorPhone=@DoctorPhone where Doctor_Id=@Doctor_Id";
           
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Doctor_Id", obj.index);
                    command.Parameters.AddWithValue("@DoctorName", obj.Doctorname);
                    command.Parameters.AddWithValue("@DoctorSurname", obj.Doctorsurname);
                    command.Parameters.AddWithValue("@DoctorAge", obj.Doctorage);
                    command.Parameters.AddWithValue("@DoctorYear", obj.Doctoryear);
                    command.Parameters.AddWithValue("@DoctorProfesion", obj.Doctorprofesion);
                    command.Parameters.AddWithValue("@DoctorPhone", obj.Doctorphone);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }
       
        public static void UpdateIllenss(Illness obj)
        {
            string query = @"update [Illness] set IllnessName=@IllnessName,IllnessSimptoms=@IllnessSimptoms,IllnessNodus=@IllnessNodus where Illness_Id=@Illness_Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Illness_Id", obj.index);
                    command.Parameters.AddWithValue("@IllnessName", obj.Nameofill);
                    command.Parameters.AddWithValue("@IllnessSimptoms", obj.Simptoms);
                    command.Parameters.AddWithValue("@IllnessNodus", obj.Nodus); 
                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }


        #endregion
    }
}