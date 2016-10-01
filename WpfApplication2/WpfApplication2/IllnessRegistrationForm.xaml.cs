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
    /// Interaction logic for IllnessRegistrationForm.xaml
    /// </summary>
    public partial class IllnessRegistrationForm : Window
    {
        public IllnessRegistrationForm()
        {
            InitializeComponent();
        }

        private void MenuItem_1(object sender, RoutedEventArgs e)
        {
            AddIllnessForm p = new AddIllnessForm();
            p.ShowDialog();
            int index = Illness.Illnesses.Count;
            Illness Illnes = new Illness(p.TextBox1.Text, p.TextBox2.Text, p.TextBox3.Text);
            MainWindow.IllnessToList.Add(Illnes);
            Wrapper Wrape = new Wrapper("Illness", "ADD", Illnes);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();

            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            MessageBox.Show("Item Added !");
            ReLoad();
        }

        private void MenuItem_2(object sender, RoutedEventArgs e)
        {
            int DelIndex = IllnessViewListBox.SelectedIndex;
            Illness p = MainWindow.IllnessToList[DelIndex];
            Wrapper Wrape = new Wrapper("Illness", "DELETE", p);
            byte[] userDataBytes;
            MemoryStream Memory = new MemoryStream();
            BinaryFormatter Formater = new BinaryFormatter();
            Formater.Serialize(Memory, Wrape);
            userDataBytes = Memory.ToArray();

            MainWindow.serverStream.Write(userDataBytes, 0, userDataBytes.Length);
            //MainWindow.Send(userDataBytes);
            Illness.Illnesses.Remove(MainWindow.IllnessToList[DelIndex].index);
            MainWindow.IllnessToList.RemoveAt(DelIndex);
            ReLoad();
        }

        private void MenuItem_3(object sender, RoutedEventArgs e)
        {
            ReLoad();
        }

        private void MenuItem_4(object sender, RoutedEventArgs e)
        {

        }
        private void ReLoad()
        {
            IllnessViewListBox.ItemsSource = Illness.Illnesses.ToList();
            IllnessViewListBox.DisplayMemberPath = "Value";
        }
    }
}
