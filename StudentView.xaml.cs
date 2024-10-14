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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsCoursesApp
{
    /// <summary>
    /// Логика взаимодействия для StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public Students Student => _student;
        private Students _student;

        public StudentView()
        {
            InitializeComponent();
        }

        public StudentView(Students student)
        {
            InitializeComponent();
            if (student == null) return;

            _student = student;
            FirstNameBlock.Text = student.FirstName;
            LastNameBox.Text = student.LastName;
        }
    }
}
