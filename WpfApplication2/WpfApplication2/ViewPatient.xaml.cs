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
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for ViewPatient.xaml
    /// </summary>
    public partial class ViewPatient : Window
    {
        public ViewPatient(int indexLoad)
        {
            InitializeComponent();
            this.indexLoad = indexLoad;
            Loaded += Load;
        }
        int indexLoad;
        public void Load(object sender, EventArgs e)
        {
        Patient p =  MainWindow.PatientToLIst[indexLoad];
        Name.Content = p.Patientname;
        Surname.Content = p.Patientsurname;
        Age.Content = p.Patientage;
        Adress.Content = p.Patientadress;
        Phone.Content = p.Patienttelephone;
        Passport.Content = p.Patientpass;
        }
    }
}
