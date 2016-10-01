using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientClasses;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow 
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded+=LoadStart;

        }
        public  static TcpClient clientSocket = new TcpClient();
        public  static NetworkStream serverStream;
        #region ListsObject
        public static  List<Preparation> PreparationToList = new List<Preparation>();
        public static List<Illness> IllnessToList = new List<Illness>();
        public static List<Doctor> DoctorsToList = new List<Doctor>();
        public static List<Patient> PatientToLIst = new List<Patient>();
        #endregion

        public void LoadStart(object sender, EventArgs e)
        {
           
            clientSocket.Connect("127.0.0.1", 80);
            serverStream = clientSocket.GetStream();
            new Thread(new ParameterizedThreadStart(Start)).Start(clientSocket);
            
        }
   
        private void Start(object client)
        {
            while (true)
            {
                try
                {
                    byte[] inStream = new byte[100000];
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                    MemoryStream ms = new MemoryStream(inStream);
                    BinaryFormatter bf1 = new BinaryFormatter();
                    ms.Position = 0;
                    WrapperBack ComandAndInfo = (WrapperBack)bf1.Deserialize(ms);
                    if (ComandAndInfo.TypeOfClas == "Preparation")
                    {
                        foreach (object obj in ComandAndInfo.Objects)
                        {
                            AddPreparat((Preparation)obj);
                        }

                    }
                    else if (ComandAndInfo.TypeOfClas == "Illness")
                    {
                        foreach (object obj in ComandAndInfo.Objects)
                        {
                            AddIllness((Illness)obj);
                        }
                    }
                    else if (ComandAndInfo.TypeOfClas == "Doctor")
                    {
                        foreach (object obj in ComandAndInfo.Objects)
                        {
                            AddDoctor((Doctor)obj);
                        }
                    }
                    else if (ComandAndInfo.TypeOfClas == "Patient")
                    {
                        foreach (object obj in ComandAndInfo.Objects)
                        {
                            AddPatient((Patient)obj);
                        }
                    }
                    serverStream.Flush();
                }

                catch (Exception ex)
                {
                }
            }
        }
        #region LoadInformation
        public void AddPreparat(Preparation obj)
        {
            int index = Preparation.Preparations.Count();
            Preparation Preparat = new Preparation(obj.Name, obj.Contraindications,obj.Date, obj.Cost,obj.Manufactor,obj.index);
            PreparationToList.Add(Preparat);
 
        }
        public void AddIllness(Illness obj)
        {
            int index = Illness.Illnesses.Count();
            Illness Illnes = new Illness(obj.Nameofill, obj.Simptoms, obj.Nodus,obj.index);
            IllnessToList.Add(Illnes);
        }
        public void AddDoctor(Doctor obj)
        {
            int index = Doctor.Doctors.Count();
            Doctor doc = new Doctor(obj.Doctorname, obj.Doctorsurname, obj.Doctorage, obj.Doctoryear, obj.Doctorprofesion, obj.Doctorphone, obj.index);
            DoctorsToList.Add(doc);
        }
        public void AddPatient(Patient obj)
        {
            int index = Patient.Patients_Dictionary.Count;
            Patient Pat = new Patient(obj.Patientname, obj.Patientsurname, obj.Patientage, obj.Patientproblems, obj.Patienttelephone, obj.Patientadress, obj.Patientpass, obj.Illnesses, obj.Doctors, obj.id);
            PatientToLIst.Add(Pat);
        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationMainWorm reg = new RegistrationMainWorm();
            reg.ShowDialog();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Wrapper Wrape = new Wrapper("Preparation", "SELECTALL", null);
            byte[] userDataBytesload;
            MemoryStream Memoryload = new MemoryStream();
            BinaryFormatter Formaterload = new BinaryFormatter();
            Formaterload.Serialize(Memoryload, Wrape);
            userDataBytesload = Memoryload.ToArray();
            serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            Chemist_s_Shop win2 = new Chemist_s_Shop();
            win2.ShowDialog();
            
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            this.Close();
        }
    }
}
