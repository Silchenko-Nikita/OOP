using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{

    public class Student
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Comment { get; set; }

        public Student(string Name, int Score,
            DateTime TimeAdded, string Comment)
        {
            this.Name = Name;
            this.Score = Score;
            this.TimeAdded = TimeAdded;
            this.Comment = Comment;
        }
    }
    
    public class StudentsModel: ObservableCollection<Student>
    {
        private static object _threadLock = new Object();
        private static StudentsModel current = null;
 
        public static StudentsModel Current {
            get {
                lock (_threadLock)
                if (current == null)
                    current = new StudentsModel();
 
                return current;
            }
        }
 
        private StudentsModel() {
 
            Random rd = new Random();
            for (int Idex = 1; Idex <= 5; Idex++)
            {
                string Name = "Student Name No. " + Idex.ToString();
                int Score = 
                    System.Convert.ToInt16(60 + rd.NextDouble() * 40);
                DateTime TimeAdded = System.DateTime.Now;
                string Comment = "This student is added @ " +
                    TimeAdded.ToString();
 
                Student aStudent = new Student(Name, Score,
                    TimeAdded, Comment);
                Add(aStudent);
            }
        }
 
        public void AddAStudent(String Name,
            int Score, DateTime TimeAdded, string Comment) {
            Student aNewStudent = new Student(Name, Score,
                TimeAdded, Comment);
            Add(aNewStudent);
        }
    }
}
