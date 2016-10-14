using System;
using System.Collections;

namespace Model
{
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

        public void EvaluateStudentsByGPA(double minGPA, double maxGPA)
        {
            clsStudent student = null;
            for (int i = List.Count - 1; i >= 0; --i)
            {
                student = (clsStudent)List[i];
                if (student.Grade < minGPA)
                {
                    List.Remove(student);
                    continue;
                }

                if (student.Grade > maxGPA)
                {
                    List.Remove(student);
                    continue;
                }
            }
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
    }
}
