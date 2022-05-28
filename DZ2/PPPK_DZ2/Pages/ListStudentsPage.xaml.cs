using PPPK_DZ2.Models;
using PPPK_DZ2.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    /// <summary>
    /// Interaction logic for ListStudentsPage.xaml
    /// </summary>
    public partial class ListStudentsPage : StudentFramedPage
    {
        public ListStudentsPage(StudentViewModel studentViewModel) : base(studentViewModel)
        {
            InitializeComponent();
            lvStudents.ItemsSource = studentViewModel.Students;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Content = null;

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new EditStudentPage(StudentViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvStudents.SelectedItem != null)
            {
                Frame.Navigate(new EditStudentPage(StudentViewModel, lvStudents.SelectedItem as Student) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvStudents.SelectedItem != null)
            {
                StudentViewModel.Students.Remove(lvStudents.SelectedItem as Student);
            }
        }
    }
}
