using Microsoft.Win32;
using PPPK_DZ2.Models;
using PPPK_DZ2.Utils;
using PPPK_DZ2.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PPPK_DZ2.Pages
{
    /// <summary>
    /// Interaction logic for EditStudentPage.xaml
    /// </summary>
    public partial class EditStudentPage : StudentFramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Student student;
        private StudentCoursesViewModel studentCoursesViewModel;

        public EditStudentPage(StudentViewModel studentViewModel, Student student = null) : base(studentViewModel)
        {
            InitializeComponent();
            this.student = student ?? new Student();
            DataContext = student;
            if (student != null)
            {
                CourseViewModel courseViewModel = new CourseViewModel();
                lvAllCourses.ItemsSource = courseViewModel.Courses;
                studentCoursesViewModel = new StudentCoursesViewModel(student);
                lvStudentCourses.ItemsSource = studentCoursesViewModel.StudentCourses;
            }
            else
            {
                lblCourses.Visibility = Visibility.Hidden;
                lblAllCourses.Visibility = Visibility.Hidden;
                lvAllCourses.Visibility = Visibility.Hidden;
                lvStudentCourses.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                student.Ects = int.Parse(tbEcts.Text.Trim());
                student.Email = tbEmail.Text.Trim();
                student.FirstName = tbFirstName.Text.Trim();
                student.LastName = tbLastName.Text.Trim();
                student.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (student.IDPerson == 0)
                {
                    StudentViewModel.Students.Add(student);
                }
                else
                {
                    StudentViewModel.Update(student);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int ects))
                    || ("Email".Equals(e.Tag) && !ValidationUtils.IsValidEmail(tbEmail.Text.Trim())))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null)
            {
                pictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                pictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lvAllCourses.SelectedItem != null)
            {
                Course course = lvAllCourses.SelectedItem as Course;
                if (!lvStudentCourses.Items.Contains(course))
                {
                    studentCoursesViewModel.StudentCourses.Add(lvAllCourses.SelectedItem as Course);
                }
                else
                {
                    lblMessage.Content = "Student is already assigned to this course.";
                    DispatcherTimer timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromSeconds(3)
                    };
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblMessage.Content = string.Empty;
            (sender as DispatcherTimer).Stop();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvStudentCourses.SelectedItem != null)
            {
                studentCoursesViewModel.StudentCourses.Remove(lvStudentCourses.SelectedItem as Course);
            }
        }
    }
}
