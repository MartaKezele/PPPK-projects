using PPPK_DZ2.Models;
using PPPK_DZ2.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    /// <summary>
    /// Interaction logic for ListCoursesPage.xaml
    /// </summary>
    public partial class ListCoursesPage : CourseFramedPage
    {
        public ListCoursesPage(CourseViewModel courseViewModel) : base(courseViewModel)
        {
            InitializeComponent();
            lvCourses.ItemsSource = courseViewModel.Courses;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Content = null;

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new EditCoursePage(CourseViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvCourses.SelectedItem != null)
            {
                Frame.Navigate(new EditCoursePage(CourseViewModel, lvCourses.SelectedItem as Course) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCourses.SelectedItem != null)
            {
                CourseViewModel.Courses.Remove(lvCourses.SelectedItem as Course);
            }
        }
    }
}
