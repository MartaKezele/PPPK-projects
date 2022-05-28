using PPPK_DZ2.ViewModels;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    public class StudentFramedPage : Page
    {
        public StudentViewModel StudentViewModel { get; }
        public Frame Frame { get; set; }

        public StudentFramedPage(StudentViewModel studentViewModel)
        {
            StudentViewModel = studentViewModel;
        }
    }
}
