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

    public partial class Chemist_s_Shop : Window 
    {
        public Chemist_s_Shop()
        {
            InitializeComponent();
           
        }
  
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           
            AddPreparationForm p = new AddPreparationForm();
            p.ShowDialog();
            Preparation Preparat = new Preparation(p.TextBox1.Text,p.TextBox2.Text,p.TextBox3.Text,p.TextBox4.Text,p.TextBox5.Text);
            MainWindow.PreparationToList.Add(Preparat);
            Wrapper Wrape = new Wrapper("Preparation", "ADD", Preparat);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();
            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            ReLoad();
        }
        private void ReLoad()
        {
            
            PraparationViewListBox.ItemsSource = Preparation.Preparations.ToList();
            PraparationViewListBox.DisplayMemberPath = "Value";
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            int DelIndex = PraparationViewListBox.SelectedIndex;
            Preparation p = MainWindow.PreparationToList[DelIndex];
            Wrapper Wrape = new Wrapper("Preparation", "DELETE", p);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();
            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            Preparation.Preparations.Remove(MainWindow.PreparationToList[DelIndex].index);
            MainWindow.PreparationToList.RemoveAt(DelIndex);
            ReLoad();
            
        }
            

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
           //todo Update
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ReLoad();
        }
    }
}
