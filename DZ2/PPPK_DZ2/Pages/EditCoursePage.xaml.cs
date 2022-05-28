using PPPK_DZ2.Models;
using PPPK_DZ2.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PPPK_DZ2.Pages
{
    /// <summary>
    /// Interaction logic for EditCoursePage.xaml
    /// </summary>
    public partial class EditCoursePage : CourseFramedPage
    {
        private readonly Course course;

        public EditCoursePage(CourseViewModel courseViewModel, Course course = null) : base(courseViewModel)
        {
            InitializeComponent();
            this.course = course ?? new Course();
            DataContext = course;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                course.Ects = int.Parse(tbEcts.Text.Trim());
                course.CourseName = tbName.Text.Trim();
                if (course.IDCourse == 0)
                {
                    CourseViewModel.Courses.Add(course);
                }
                else
                {
                    CourseViewModel.Update(course);
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
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int ects)))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });

            return valid;
        }
    }
}
