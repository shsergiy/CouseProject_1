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
    /// Interaction logic for RegistrationMainWorm.xaml
    /// </summary>
    public partial class RegistrationMainWorm : Window
    {
        public RegistrationMainWorm()
        {
            InitializeComponent();
        }

        private void Btn_Click_1(object sender, RoutedEventArgs e)
        {
            Wrapper Wrape = new Wrapper("Patient", "SELECTALL", null);
            byte[] userDataBytesload;
            MemoryStream Memoryload = new MemoryStream();
            BinaryFormatter Formaterload = new BinaryFormatter();
            Formaterload.Serialize(Memoryload, Wrape);
            userDataBytesload = Memoryload.ToArray();
            MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            ParientRegistrationForm win2 = new ParientRegistrationForm();
            win2.ShowDialog();
        }

        private void Btn_Click_2(object sender, RoutedEventArgs e)
        {
            Wrapper Wrape = new Wrapper("Illness", "SELECTALL", null);
            byte[] userDataBytesload;
            MemoryStream Memoryload = new MemoryStream();
            BinaryFormatter Formaterload = new BinaryFormatter();
            Formaterload.Serialize(Memoryload, Wrape);
            userDataBytesload = Memoryload.ToArray();
            MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            IllnessRegistrationForm win2 = new IllnessRegistrationForm();
            win2.ShowDialog();
                    
        }

        private void Btn_Click_3(object sender, RoutedEventArgs e)
        {
            Wrapper Wrape = new Wrapper("Doctor", "SELECTALL", null);
            byte[] userDataBytesload;
            MemoryStream Memoryload = new MemoryStream();
            BinaryFormatter Formaterload = new BinaryFormatter();
            Formaterload.Serialize(Memoryload, Wrape);
            userDataBytesload = Memoryload.ToArray();
            MainWindow.serverStream.Write(userDataBytesload, 0, userDataBytesload.Length);
            DoctorRegistrationForm win2 = new DoctorRegistrationForm();
            win2.ShowDialog();
        }

        private void Btn_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
