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
    /// Interaction logic for EditProfessorsPage.xaml
    /// </summary>
    public partial class EditProfessorPage : ProfessorFramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Professor professor;
        private ProfessorCoursesViewModel professorCoursesViewModel;

        public EditProfessorPage(ProfessorViewModel professorViewModel, Professor professor = null) : base(professorViewModel)
        {
            InitializeComponent();
            this.professor = professor ?? new Professor();
            DataContext = professor;

            if (professor != null)
            {
                CourseViewModel courseViewModel = new CourseViewModel();
                lvAllCourses.ItemsSource = courseViewModel.Courses;
                professorCoursesViewModel = new ProfessorCoursesViewModel(professor);
                lvProfessorCourses.ItemsSource = professorCoursesViewModel.ProfessorCourses;
            }
            else
            {
                lblCourses.Visibility = Visibility.Hidden;
                lblAllCourses.Visibility = Visibility.Hidden;
                lvAllCourses.Visibility = Visibility.Hidden;
                lvProfessorCourses.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                professor.Email = tbEmail.Text.Trim();
                professor.FirstName = tbFirstName.Text.Trim();
                professor.LastName = tbLastName.Text.Trim();
                professor.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (professor.IDPerson == 0)
                {
                    ProfessorViewModel.Professors.Add(professor);
                }
                else
                {
                    ProfessorViewModel.Update(professor);
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
                if (!lvProfessorCourses.Items.Contains(course))
                {
                    professorCoursesViewModel.ProfessorCourses.Add(lvAllCourses.SelectedItem as Course);
                }
                else
                {
                    lblMessage.Content = "Professor is already assigned to this course.";
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
            if (lvProfessorCourses.SelectedItem != null)
            {
                professorCoursesViewModel.ProfessorCourses.Remove(lvProfessorCourses.SelectedItem as Course);
            }
        }
    }
}
