using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class clsStudent
    {
        private string _strFirstName;
        private string _strLastName;
        private string _strAddress;
        private double _dblGrade;
        public string FirstName
        {
            set
            {
                _strFirstName = value;
            }
            get
            {
                return _strFirstName;
            }
        }
        public string LastName
        {
            set
            {
                _strLastName = value;
            }
            get
            {
                return _strLastName;
            }
        }
        public string Address
        {
            set
            {
                _strAddress = value;
            }
            get
            {
                return _strAddress;
            }
        }
        public double Grade
        {
            set
            {
                _dblGrade = value;
            }
            get
            {
                return _dblGrade;
            }
        }
    }

    public class clsStudents : CollectionBase
    {
        public clsStudents()
        {
        }

        public void LoadStudents()
        {
            Add("Dino", "Konstantopoulos", "BU", 4.0);
            Add("Bill", "Gates", "Microsoft", 2.0);
            Add("Marc", "Zukerberg", "Facebook", 3.0);
            Add("Sergei", "Brin", "Google", 2.5);
            Add("Larry", "Page", "Google", 3.5);
            Add("Steve", "Jobs", "Apple", 3.0);
            Add("Elon", "Musk", "SpaceX", 1.5);
            Add("Jack", "Dorsey", "Twitter", 1.5);
            Add("Jeff", "Bezos", "Amazon", 3.5);
            Add("Pierre", "Omidyar", "eBay", 4.0);
        }

        void Add(string strFirstName, string strLastName, string strAddress, double dblGrade)
        {
            clsStudent objStudent = new clsStudent();
            objStudent.FirstName = strFirstName;
            objStudent.LastName = strLastName;
            objStudent.Address = strAddress;
            objStudent.Grade = dblGrade;
            List.Add(objStudent);
        }

        public void Add(clsStudent s)
        {
            List.Add(s);
        }
    }
}
