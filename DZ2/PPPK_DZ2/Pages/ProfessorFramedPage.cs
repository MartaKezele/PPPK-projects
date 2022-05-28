using PPPK_DZ2.ViewModels;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    public class ProfessorFramedPage : Page
    {
        public ProfessorViewModel ProfessorViewModel { get; }
        public Frame Frame { get; set; }

        public ProfessorFramedPage(ProfessorViewModel professorViewModel)
        {
            ProfessorViewModel = professorViewModel;
        }
    }
}
