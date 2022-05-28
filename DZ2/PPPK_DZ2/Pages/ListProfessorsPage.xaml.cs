using PPPK_DZ2.Models;
using PPPK_DZ2.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    /// <summary>
    /// Interaction logic for ListProfessorsPage.xaml
    /// </summary>
    public partial class ListProfessorsPage : ProfessorFramedPage
    {
        public ListProfessorsPage(ProfessorViewModel professorViewModel) : base(professorViewModel)
        {
            InitializeComponent();
            lvProfessors.ItemsSource = professorViewModel.Professors;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Content = null;

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new EditProfessorPage(ProfessorViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvProfessors.SelectedItem != null)
            {
                Frame.Navigate(new EditProfessorPage(ProfessorViewModel, lvProfessors.SelectedItem as Professor) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvProfessors.SelectedItem != null)
            {
                ProfessorViewModel.Professors.Remove(lvProfessors.SelectedItem as Professor);
            }
        }
    }
}
