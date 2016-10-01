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
    
    public partial class ShowInfoPatient : Window
    {
        public ShowInfoPatient(int indexLoad)
        {
            InitializeComponent();
            this.indexLoad = indexLoad;
            Loaded += Load;
        }
        int indexLoad;
        public void Load(object sender, EventArgs e)
        {

            List<string> keys = MainWindow.PatientToLIst[indexLoad].Doctors;
            List<string> key = MainWindow.PatientToLIst[indexLoad].Illnesses;
            List<Doctor> Doctores= new List<Doctor>();
            List<Illness> Illn = new List<Illness>();
            foreach (string keyss in keys)
            {
                foreach (Doctor d1 in MainWindow.DoctorsToList)
                {
                    if (d1.index == keyss)
                    {
                        Doctores.Add(d1);
                    }
                }
            }
            foreach (string keyss in key)
            {
                foreach (Illness d1 in MainWindow.IllnessToList)
                {
                    if (d1.index == keyss)
                    {
                        Illn.Add(d1);
                    }
                }
            }
            IllnessViewListBoxShow.ItemsSource = Illn.ToList();
            DoctorsViewListBoxshow.ItemsSource = Doctores.ToList() ;

        }




    }
}
