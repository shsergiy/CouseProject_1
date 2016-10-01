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
    /// Interaction logic for DoctorRegistrationForm.xaml
    /// </summary>
    public partial class DoctorRegistrationForm : Window
    {
        public DoctorRegistrationForm()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddDoctorForm p = new AddDoctorForm();
            p.ShowDialog();
            int index = Doctor.Doctors.Count;
            Doctor Doc = new Doctor(p.TextBox1.Text, p.TextBox2.Text, p.TextBox3.Text,p.TextBox4.Text, p.TextBox5.Text, p.TextBox6.Text);
            MainWindow.DoctorsToList.Add(Doc);
            Wrapper Wrape = new Wrapper("Doctor", "ADD", Doc);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();

            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            MessageBox.Show("Item Added !");
            ReLoad();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            int DelIndex = DoctorsViewListBox.SelectedIndex;
            Doctor p = MainWindow.DoctorsToList[DelIndex];
            Wrapper Wrape = new Wrapper("Doctor", "DELETE", p);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();

            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            //MainWindow.Send(userDataBytes);
            Doctor.Doctors.Remove(MainWindow.DoctorsToList[DelIndex].index);
            MainWindow.DoctorsToList.RemoveAt(DelIndex);
            ReLoad();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ReLoad();
        }
        private void ReLoad()
        {

            DoctorsViewListBox.ItemsSource = Doctor.Doctors.ToList();
            DoctorsViewListBox.DisplayMemberPath = "Value";
        }

        private void DoctorsViewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
      
            AddDoctorForm p = new AddDoctorForm();
            p.ShowDialog();
            int index = Doctor.Doctors.Count;
            string ID = MainWindow.DoctorsToList[DoctorsViewListBox.SelectedIndex].index;
            Doctor Doc = new Doctor(p.TextBox1.Text, p.TextBox2.Text, p.TextBox3.Text,p.TextBox4.Text, p.TextBox5.Text, p.TextBox6.Text,ID,1);
            MainWindow.DoctorsToList[DoctorsViewListBox.SelectedIndex] = Doc;
            Wrapper Wrape = new Wrapper("Doctor", "Update", Doc);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();

            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            MessageBox.Show("Item Added !");
            ReLoad();
        }

   
    }
}
