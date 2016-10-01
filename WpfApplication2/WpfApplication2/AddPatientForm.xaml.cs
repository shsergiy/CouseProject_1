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
    /// Interaction logic for AddPatientForm.xaml
    /// </summary>
    public partial class AddPatientForm : Window
    {
        public AddPatientForm(int i,int indexL)
        {
            InitializeComponent();
            this.i = i;
            this.indexL = indexL;
            Loaded += Load;
        }
        public void Load(object sender, EventArgs e)
        {
            if (i == 1)
            {
                Patient p1 = MainWindow.PatientToLIst[indexL];

                TextBox1.Text = p1.Patientname;
                TextBox2.Text = p1.Patientsurname;
                TextBox3.Text = p1.Patientage;
                TextBox4.Text = p1.Patientproblems;
                TextBox5.Text = p1.Patienttelephone;
                TextBox6.Text = p1.Patientadress;
                TextBox7.Text = p1.Patientpass;
            }
        }
        int i;
        int indexL;
        List<string> IllnessBufId = new List<string>();
        List<string> DoctorsDufId = new List<string>();
        private void Button_Clk(object sender, RoutedEventArgs e)
        {
            if (i == 1)
            {   
                Patient patient = new Patient(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, IllnessBufId, DoctorsDufId,MainWindow.PatientToLIst[indexL].id,1);
                MainWindow.PatientToLIst[indexL] = patient;
                Wrapper Wrape = new Wrapper("Patient", "Update", patient);
                byte[] userDataBytes;
                MemoryStream Memory = new MemoryStream();
                BinaryFormatter Formater = new BinaryFormatter();
                Formater.Serialize(Memory, Wrape);
                userDataBytes = Memory.ToArray();
                MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
                this.Close();
            }
            else
            {
                Patient patient = new Patient(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text);
                patient.Illnesses = IllnessBufId;
                patient.Doctors = DoctorsDufId;
                MainWindow.PatientToLIst.Add(patient);
                Wrapper Wrape = new Wrapper("Patient", "ADD", patient);
                byte[] userDataBytes;
                MemoryStream Memory = new MemoryStream();
                BinaryFormatter Formater = new BinaryFormatter();
                Formater.Serialize(Memory, Wrape);
                userDataBytes = Memory.ToArray();
                MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
                this.Close();
            }
        }

        private void Button_Clk_2(object sender, RoutedEventArgs e)
        {
            int index = DoctorsViewListBox.SelectedIndex;
            Doctor d = MainWindow.DoctorsToList[index];
            DoctorsDufId.Add(d.index);
        }

        private void Button_Clk_1(object sender, RoutedEventArgs e)
        {
            int index = IllnessViewListBox.SelectedIndex;
            Illness d = MainWindow.IllnessToList[index];
            IllnessBufId.Add(d.index);
        }

        private void Button_Clk_5(object sender, RoutedEventArgs e)
        {
            
            DoctorsViewListBox.ItemsSource = Doctor.Doctors.ToList();
            DoctorsViewListBox.DisplayMemberPath = "Value";
            IllnessViewListBox.ItemsSource = Illness.Illnesses.ToList();
            IllnessViewListBox.DisplayMemberPath = "Value";
        }

       

        
    }
}
