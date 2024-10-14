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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _selectedStudentId = -1;
        private int _selectedCourseId = -1;
        private int _selectedEnrollmentId = -1;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new SchoolDBEntities())
            {
                StudentsDataGrid.ItemsSource = context.Students.ToList();
                CoursesDataGrid.ItemsSource = context.Courses.ToList();
                EnrollmentsDataGrid.ItemsSource= context.Enrollments.ToList();
            }
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (var context = new SchoolDBEntities())
            {
                StudentsComboBox.Items.Clear();
                CourseComboBox.Items.Clear();

                foreach(var i in context.Students.ToList())
                {
                    StudentsComboBox.Items.Add(new StudentView(i));
                }
                foreach(var i in context.Courses.ToList())
                {
                    CourseComboBox.Items.Add(new CourseView(i));
                }
            }
        }
        private void ClearField()
        {
            FirstNameBox.Text = string.Empty;
            LastNameBox.Text = string.Empty;
            DateOfBirthPicker.SelectedDate = null;
            _selectedStudentId = -1;

            CourseNameBox.Text = string.Empty;
            CreditsBox.Text = string.Empty;
            _selectedCourseId = -1;

            StudentsComboBox.SelectedItem = null;
            CourseComboBox.SelectedItem = null;
            GradeComboBox.SelectedItem = null;
            _selectedEnrollmentId = -1;
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(FirstNameBox.Text)) return;
            if (FirstNameBox.Text.Length < 2) return;
            if (String.IsNullOrEmpty(LastNameBox.Text)) return;
            if (LastNameBox.Text.Length < 2) return;
            if (DateOfBirthPicker.SelectedDate == null) return;

            using (var context = new SchoolDBEntities())
            {
                var student = new Students();
                student.FirstName = FirstNameBox.Text;
                student.LastName = LastNameBox.Text;
                student.DateOfBirth = DateOfBirthPicker.SelectedDate;

                context.Students.Add(student);

                context.SaveChanges();
            }
            ClearField();
            LoadData();
        }
        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStudentId == -1) return;
            using (var context = new SchoolDBEntities())
            {
                var student = context.Students.FirstOrDefault(s=> s.StudentID == _selectedStudentId);
                context.Students.Remove(student);
                context.SaveChanges();
            }
            ClearField();
            LoadData();
        }
        private void UpdateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStudentId == -1) return;
            if (String.IsNullOrEmpty(FirstNameBox.Text)) return;
            if (FirstNameBox.Text.Length < 2) return;
            if (String.IsNullOrEmpty(LastNameBox.Text)) return;
            if (LastNameBox.Text.Length < 2) return;
            if (DateOfBirthPicker.SelectedDate == null) return;

            using (var context = new SchoolDBEntities())
            {
                var student = context.Students.FirstOrDefault(s => s.StudentID == _selectedStudentId);
                if(student != null)
                {
                    student.FirstName = FirstNameBox.Text;
                    student.LastName = LastNameBox.Text;
                    student.DateOfBirth = DateOfBirthPicker.SelectedDate;
                    context.SaveChanges();
                }
            }
            ClearField();
            LoadData();
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Students)
            {
                var student = StudentsDataGrid.SelectedItem as Students;
                _selectedStudentId = student.StudentID;
                FirstNameBox.Text = student.FirstName;
                LastNameBox.Text = student.LastName;
                DateOfBirthPicker.SelectedDate = student.DateOfBirth;
            }
        }

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CourseNameBox.Text)) return;
            if (CourseNameBox.Text.Length < 2) return;

            if (Int32.TryParse(CreditsBox.Text, out int result))
            {
                using (var context = new SchoolDBEntities())
                {
                    var course = new Courses();
                    course.CourseName = CourseNameBox.Text;
                    course.Credits = result;

                    context.Courses.Add(course);
                    context.SaveChanges();
                }
                ClearField();
                LoadData();
            }
        }
        private void RemoveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCourseId == -1) return;
            using (var context = new SchoolDBEntities())
            {
                var course = context.Courses.FirstOrDefault(s => s.CourseID == _selectedCourseId);
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            ClearField();
            LoadData();
        }
        private void UpdateCourseButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CourseNameBox.Text)) return;
            if (CourseNameBox.Text.Length < 2) return;

            if (Int32.TryParse(CreditsBox.Text, out int result))
            {
                using (var context = new SchoolDBEntities())
                {
                    var course = context.Courses.FirstOrDefault(s => s.CourseID == _selectedCourseId);
                    if(course != null)
                    {
                        course.CourseName = CourseNameBox.Text;
                        course.Credits = result;

                        context.SaveChanges();
                    }
                }
                ClearField();
                LoadData();
            }

        }

        private void AddEnrollmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCourseId == -1 || _selectedStudentId == -1) return;
            if (!string.IsNullOrEmpty(GradeComboBox.Text))
            {
                using (var context = new SchoolDBEntities())
                {
                    var enrollments = new Enrollments();
                    enrollments.StudentID = _selectedStudentId;
                    enrollments.CourseID = _selectedCourseId;
                    enrollments.Grade = GradeComboBox.Text;

                    context.Enrollments.Add(enrollments);
                    context.SaveChanges();
                }
                ClearField();
                LoadData();
            }
        }
        private void DeleteEnrollmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEnrollmentId == -1) return;
            using (var context = new SchoolDBEntities())
            {
                var enrollments = context.Enrollments.FirstOrDefault(s => s.EnrollmentID == _selectedEnrollmentId);
                context.Enrollments.Remove(enrollments);
                context.SaveChanges();
            }
            ClearField();
            LoadData();
        }

        private void EnrollmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnrollmentsDataGrid.SelectedItem is Enrollments)
            {
                var enrollments = EnrollmentsDataGrid.SelectedItem as Enrollments;
                _selectedEnrollmentId = enrollments.EnrollmentID;
            }
        }

        private void CoursesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CoursesDataGrid.SelectedItem is Courses)
            {
                var courses = CoursesDataGrid.SelectedItem as Courses;
                _selectedCourseId = courses.CourseID;
                CourseNameBox.Text = courses.CourseName;
                CreditsBox.Text = courses.Credits.ToString();
            }
        }
        private void StudentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StudentsComboBox.SelectedItem == null) return;
            _selectedStudentId = ((StudentView)StudentsComboBox.SelectedItem).Student.StudentID;
        }
        private void CourseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CourseComboBox.SelectedItem == null) return;
            _selectedCourseId = ((CourseView)CourseComboBox.SelectedItem).Courses.CourseID;
        }
    }
}
