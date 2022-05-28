using PPPK_DZ2.Dal;
using PPPK_DZ2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPPK_DZ2.ViewModels
{
    class ProfessorCoursesViewModel
    {
        public ObservableCollection<Course> ProfessorCourses { get; }
        private Professor professor;

        public ProfessorCoursesViewModel(Professor professor)
        {
            this.professor = professor;
            ProfessorCourses = new ObservableCollection<Course>(RepoFactory.GetRepo().GetProfessorCourses(professor.IDPerson));
            ProfessorCourses.CollectionChanged += ProfessorCourses_CollectionChanged;
        }

        private void ProfessorCourses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepoFactory.GetRepo().AddCourseProfessor(ProfessorCourses[e.NewStartingIndex].IDCourse, professor.IDPerson);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepoFactory.GetRepo().DeleteCourseProfessor(e.OldItems.OfType<Course>().ToList()[0].IDCourse, professor.IDPerson);
                    break;
            }
        }
    }
}
