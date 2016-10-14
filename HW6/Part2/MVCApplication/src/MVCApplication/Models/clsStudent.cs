using System;
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
}
