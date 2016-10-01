using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientClasses
{
    [Serializable]
    public class Wrapper
    {
        public string TypeOfClass;
        public string Command;
        public object ObjClass;
        public Wrapper(string typeofclass, string command, object objclass)
        {
            TypeOfClass = typeofclass;
            Command = command;
            ObjClass = objclass;
        }
        public override string ToString()
        {
            return TypeOfClass+", "+Command;
        }
    }
    [Serializable]
    public class WrapperBack
        
    {public string TypeOfClas;
        public List<object> Objects = new List<object>();
        public WrapperBack(string type, List<object> obj) { TypeOfClas = type; Objects = obj; }
    }
    [Serializable]
    public class Patient 
    {
        public static Dictionary<string, Patient> Patients_Dictionary = new Dictionary<string, Patient>();
        private string patientname;
        private string patientsurname;
        private string patientage;
        private string patientproblems;
        private  string patienttelephone;
        private string patientadress;
        private string patientpass;
        public string id;
        public List<string> Illnesses { get; set; }
        public List<string> Doctors { get; set; }
        public string Patientname
        {
            get { return patientname; }
            set { patientname = value;}
        }
        public string Patientsurname
        {
            get { return patientsurname; }
            set { patientsurname = value;}
        }
        public string Patientage 
        {
            get { return patientage; }
            set
            {
                   patientage=value;
            }
        }
        public string Patientproblems 
        {
            get { return patientproblems; }
            set { patientproblems = value;}
        }
        public string Patienttelephone
        {
            get { return patienttelephone; }
            set { patienttelephone = value;}
        }
        public string Patientadress
        {
            get { return patientadress; }
            set { patientadress = value;}
        }
        public string Patientpass 
        {
            get { return patientpass; }
            set { patientpass = value;}
        }
        public Patient(){}
        public Patient(string name, string surname, string age, string problems, string phone, string adress, string pass)
        {
            patientname = name;
            patientsurname = surname;
            patientage = age;
            patientproblems = problems;
            patienttelephone = phone;
            patientadress = adress;
            patientpass = pass;
            Illnesses = new List<string>();
            Doctors = new List<string>();
            id = Guid.NewGuid().ToString();
            Patients_Dictionary.Add(id, this);
        }
        public Patient(string name, string surname, string age, string problems, string phone, string adress, string pass,List<string> Ill,List<string> Doc,string ID,int i)
        {
            patientname = name;
            patientsurname = surname;
            patientage = age;
            patientproblems = problems;
            patienttelephone = phone;
            patientadress = adress;
            patientpass = pass;
            Illnesses = Ill;
            Doctors = Doc;
            id = ID;
            Patients_Dictionary[id] = this;
        }
        public Patient(string name, string surname, string age, string problems, string phone, string adress, string pass, List<string> Ill,List<string> Doc,string ID)
        {
            patientname = name;
            patientsurname = surname;
            patientage = age;
            patientproblems = problems;
            patienttelephone = phone;
            patientadress = adress;
            patientpass = pass;
            Illnesses = Ill;
            Doctors = Doc;
            id = ID;
            Patients_Dictionary.Add(id,this);
        }
        public override string ToString()
        {
            return Patientname + " " + Patientsurname;
        }
    }
    [Serializable]
    public class Doctor
    {
        public static Dictionary<string, Doctor> Doctors = new Dictionary<string, Doctor>();
        
        private string doctorname;
        private string doctorsurname;
        private string doctorage;
        private string doctoryear;
        private string doctorprofesion;
        private string doctorphone;
        public string index;
        public string Doctorname
        {
            get { return doctorname; }
            set { doctorname = value;}
        }
        public string Doctorsurname
        {
            get { return doctorsurname; }
            set { doctorsurname = value;}
        }
        public string Doctorage
        {
            get { return doctorage; }
            set { doctorage = value;}
        }
        public string Doctoryear
        {
            get { return doctoryear; }
            set { doctoryear = value;}
        }
        public string Doctorprofesion
        {
            get { return doctorprofesion; }
            set { doctorprofesion = value;}
        }
        public string Doctorphone
        {
            get { return doctorphone; }
            set { doctorphone = value;}
        }
        public Doctor() { }
        public Doctor(string name,string surname,string age,string year,string prof,string phone)
        {
            doctorname = name;
            doctorsurname = surname;
            doctorage = age;
            doctoryear = year;
            doctorprofesion = prof;
            doctorphone = phone;
            index = Guid.NewGuid().ToString();
            Doctors.Add(index, this);
        }
        public  Doctor(string name, string surname, string age, string year, string prof, string phone,string ID,int i)
        {
            doctorname = name;
            doctorsurname = surname;
            doctorage = age;
            doctoryear = year;
            doctorprofesion = prof;
            doctorphone = phone;
            index = ID;
            Doctors[ID] = this;
        }
        public Doctor(string name, string surname, string age, string year, string prof, string phone, string ID)
        {
            doctorname = name;
            doctorsurname = surname;
            doctorage = age;
            doctoryear = year;
            doctorprofesion = prof;
            doctorphone = phone;
            index = ID;
            Doctors.Add(index,this);
        }
        public override string ToString()
        {
            return Doctorname + " " + Doctorsurname + " " + Doctorage + " " + Doctoryear + " " + Doctorprofesion + " " + Doctorphone;
        }
    }
    [Serializable]
    public class Preparation
    {
        public static Dictionary<string,Preparation> Preparations = new Dictionary<string,Preparation>();
        private string name;
        private string contraindications;
        private string date;
        private string cost;
        private string manufactor;
        public string  index;
        public string Name 
        {
            get { return name; }
            set { name = value;}
        }
        public string Contraindications
        {
            get { return contraindications; }
            set { contraindications = value;}
        }
        public string Date 
        {
            get { return date; }
            set { date = value;}
        }
        public string Cost
        { 
            get { return cost; } 
            set { cost = value;} 
        }
        public string Manufactor 
        {
            get { return manufactor; }
            set { manufactor = value;}
        }
        public Preparation() { }
        public Preparation(string n, string contraindcation, string dat, string prise, string mfs)
        {
            name = n;
            contraindications = contraindcation;
            date = dat;
            cost = prise;
            manufactor = mfs;
            index = Guid.NewGuid().ToString();
            Preparations.Add(index,this);
        }
        public Preparation(string n, string contraindcation, string dat, string prise, string mfs,string ID)
        {
            name = n;
            contraindications = contraindcation;
            date = dat;
            cost = prise;
            manufactor = mfs;
            index = ID;
            Preparations.Add(index, this);
        }
        public override string ToString()
        {
            return Name + " " + Contraindications + " " + Date + " " + Cost + " " + Manufactor;
        }
    }
    [Serializable]
    public class Illness 
    {
        
        public static Dictionary<string, Illness> Illnesses = new Dictionary<string, Illness>();
        private string nameofill;
        private string simptoms;
        private string nodus;
        public string index;
        public string Nameofill
        {
            get { return nameofill; }
            set { nameofill = value;}
        }
        public string Simptoms
        {
            get { return simptoms; }
            set { simptoms = value;}
        }
        public string Nodus 
        {
            get { return nodus; }
            set { nodus = value;}
        }
        public Illness() { }
        public Illness(string name, string simp, string nod)
        {
            nameofill = name;
            simptoms = simp;
            nodus = nod;
            index = Guid.NewGuid().ToString();
            Illnesses.Add(index,this);
        }
        public Illness(string name, string simp, string nod,string ID)
        {
            nameofill = name;
            simptoms = simp;
            nodus = nod;
            index = ID;
            Illnesses.Add(index, this);
        }
        public override string ToString()
        {
            return Nameofill + " " + Simptoms + " " + Nodus;
        }
    }
    public class Coder
    {
        public void Code()
        {
            string password = Console.ReadLine();
            string key = Console.ReadLine();

            byte stepbyte;
            byte[] Passbyte = Encoding.Default.GetBytes(password);
            byte[] Keybyte = Encoding.Default.GetBytes(key);
            byte[] CodedPassbyte = new byte[200];
            for (int i = 0; i < Passbyte.Length; i++)
            {
                stepbyte = (byte)((Passbyte[i] ^ Keybyte[i]) + 70);
                CodedPassbyte[i] = stepbyte;
            }
            byte c = Convert.ToByte('a');
            byte u = Convert.ToByte('d');
            string CodedResult = "";
            byte[] passByte = System.Text.Encoding.Default.GetBytes(password);
            byte[] keyByte = System.Text.Encoding.Default.GetBytes(key);

            for (int index = 0; index < passByte.Length; index++)
            {
                string passHex = Convert.ToString(passByte[index], 2);
                string keyHex = Convert.ToString(keyByte[index], 2);
                char[] pastest = passHex.ToCharArray();
                char[] keytest = keyHex.ToCharArray();
                string pasXOR = "";
                for (int i = 0; i < passHex.Length; i++)
                {
                    char t1 = pastest[i];
                    char t2 = keytest[i];
                    int step = Convert.ToInt32(t1) ^ Convert.ToInt32(t2);
                    pasXOR += Convert.ToString(step);
                }

               
                int CallBack = Convert.ToInt32(pasXOR, 2);
                CodedResult += Convert.ToString(CallBack) + " ";
            }
           
        }
    }

}
