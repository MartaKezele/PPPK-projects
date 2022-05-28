using PPPK_DZ2.Dal;
using PPPK_DZ2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPPK_DZ2.ViewModels
{
    public class CourseViewModel
    {
        public ObservableCollection<Course> Courses { get; }
        public CourseViewModel()
        {
            Courses = new ObservableCollection<Course>(RepoFactory.GetRepo().GetCourses());
            Courses.CollectionChanged += Courses_CollectionChanged;
        }

        private void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepoFactory.GetRepo().AddCourse(Courses[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepoFactory.GetRepo().DeleteCourse(e.OldItems.OfType<Course>().ToList()[0].IDCourse);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepoFactory.GetRepo().UpdateCourse(e.NewItems.OfType<Course>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Course course) => Courses[Courses.IndexOf(course)] = course;
    }
}
