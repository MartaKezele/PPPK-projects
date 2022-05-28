using PPPK_DZ2.Dal;
using PPPK_DZ2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPPK_DZ2.ViewModels
{
    public class StudentViewModel
    {
        public ObservableCollection<Person> Students { get; }
        public StudentViewModel()
        {
            Students = new ObservableCollection<Person>(RepoFactory.GetRepo().GetStudents());
            Students.CollectionChanged += Students_CollectionChanged;
        }

        private void Students_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepoFactory.GetRepo().AddStudent(Students[e.NewStartingIndex] as Student);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepoFactory.GetRepo().DeleteStudent(e.OldItems.OfType<Student>().ToList()[0].IDPerson);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepoFactory.GetRepo().UpdateStudent(e.NewItems.OfType<Student>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Person student) => Students[Students.IndexOf(student)] = student;
    }
}
