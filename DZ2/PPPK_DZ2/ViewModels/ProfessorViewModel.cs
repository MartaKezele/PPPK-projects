using PPPK_DZ2.Dal;
using PPPK_DZ2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPPK_DZ2.ViewModels
{
    public class ProfessorViewModel
    {
        public ObservableCollection<Person> Professors { get; }
        public ProfessorViewModel()
        {
            Professors = new ObservableCollection<Person>(RepoFactory.GetRepo().GetProfessors());
            Professors.CollectionChanged += Professors_CollectionChanged;
        }

        private void Professors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepoFactory.GetRepo().AddProfessor(Professors[e.NewStartingIndex] as Professor);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepoFactory.GetRepo().DeleteProfessor(e.OldItems.OfType<Professor>().ToList()[0].IDPerson);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepoFactory.GetRepo().UpdateProfessor(e.NewItems.OfType<Professor>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Person professor) => Professors[Professors.IndexOf(professor)] = professor;
    }
}
