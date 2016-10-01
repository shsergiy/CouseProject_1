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
    /// Interaction logic for ParientRegistrationForm.xaml
    /// </summary>
    public partial class ParientRegistrationForm : Window
    {
        public ParientRegistrationForm()
        {
            InitializeComponent();
        }

        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ReLoad();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            int DelIndex = PatientsViewListBox.SelectedIndex;
            Patient p = MainWindow.PatientToLIst[DelIndex];
            Wrapper Wrape = new Wrapper("Patient", "DELETE", p);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();
            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            Patient.Patients_Dictionary.Remove(MainWindow.PatientToLIst[DelIndex].id);
            MainWindow.PatientToLIst.RemoveAt(DelIndex);
            ReLoad();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            {
                Wrapper Wrape = new Wrapper("Doctor", "SELECTALL", null);
                byte[] userDataBytesload;
                MemoryStream Memoryload = new MemoryStream();
                BinaryFormatter Formaterload = new BinaryFormatter();
                Formaterload.Serialize(Memoryload, Wrape);
                userDataBytesload = Memoryload.ToArray();
                MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            }
         
            {
                Wrapper Wrape = new Wrapper("Illness", "SELECTALL", null);
                byte[] userDataBytesload;
                MemoryStream Memoryload = new MemoryStream();
                BinaryFormatter Formaterload = new BinaryFormatter();
                Formaterload.Serialize(Memoryload, Wrape);
                userDataBytesload = Memoryload.ToArray();
                MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            }

            AddPatientForm p = new AddPatientForm(0,PatientsViewListBox.SelectedIndex);
            p.ShowDialog();
            MessageBox.Show("Item Added !");
            ReLoad();
        }
        private void ReLoad()
        {
            PatientsViewListBox.ItemsSource = Patient.Patients_Dictionary.ToList();
            PatientsViewListBox.DisplayMemberPath = "Value";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            int indexLoad = PatientsViewListBox.SelectedIndex;
            ShowInfoPatient show = new ShowInfoPatient(indexLoad);
            show.ShowDialog();
            
        }

        private void PatientsViewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            int index = PatientsViewListBox.SelectedIndex;
            ViewPatient view = new ViewPatient(index);
            view.ShowDialog();
        }

        private void Btn_Updateinfo(object sender, RoutedEventArgs e)
        {
            {
                Wrapper Wrape = new Wrapper("Doctor", "SELECTALL", null);
                byte[] userDataBytesload;
                MemoryStream Memoryload = new MemoryStream();
                BinaryFormatter Formaterload = new BinaryFormatter();
                Formaterload.Serialize(Memoryload, Wrape);
                userDataBytesload = Memoryload.ToArray();
                MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            }

            {
                Wrapper Wrape = new Wrapper("Illness", "SELECTALL", null);
                byte[] userDataBytesload;
                MemoryStream Memoryload = new MemoryStream();
                BinaryFormatter Formaterload = new BinaryFormatter();
                Formaterload.Serialize(Memoryload, Wrape);
                userDataBytesload = Memoryload.ToArray();
                MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            }
            
            AddPatientForm p = new AddPatientForm(1,PatientsViewListBox.SelectedIndex);
            p.ShowDialog();
            MessageBox.Show("Item Added !");
            ReLoad();
        }
    }
}
