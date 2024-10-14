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
    /// Логика взаимодействия для CourseView.xaml
    /// </summary>
    public partial class CourseView : UserControl
    {
        public Courses Courses => _course;
        private Courses _course;

        public CourseView()
        {
            InitializeComponent();
        }
        public CourseView(Courses courses)
        {
            InitializeComponent();
            _course = courses;
            CourseNameBlock.Text = courses.CourseName;
        }
    }
}
