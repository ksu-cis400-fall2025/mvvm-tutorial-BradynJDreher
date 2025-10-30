using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Student _student { get; init; }

        public string FirstName => _student.FirstName;
        
        public string LastName => _student.LastName;

        public IEnumerator<CourseRecord> CourseRecord => (IEnumerator<CourseRecord>)_student.CourseRecords;

        public double GPA => _student. GPA;

        public double ComputerSciGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }

        private void HandelStudentPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_student.GPA))
            {
                PropertyChanged?.Invoke(this,e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerSciGPA)));
            }
        }

        public ComputerScienceStudentViewModel(Student s)
        {
            _student = s;
            s.PropertyChanged += HandelStudentPropertyChange;
        }

    }
}
