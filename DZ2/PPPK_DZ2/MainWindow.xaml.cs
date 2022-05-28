using PPPK_DZ2.Pages;
using PPPK_DZ2.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PPPK_DZ2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnManageStudents_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new ListStudentsPage(new StudentViewModel()) { Frame = Frame });

        private void BtnManageProfessors_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new ListProfessorsPage(new ProfessorViewModel()) { Frame = Frame });

        private void BtnManageCourses_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new ListCoursesPage(new CourseViewModel()) { Frame = Frame });
    }
}
