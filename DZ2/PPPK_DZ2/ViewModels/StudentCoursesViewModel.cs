using PPPK_DZ2.Dal;
using PPPK_DZ2.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPPK_DZ2.ViewModels
{
    public class StudentCoursesViewModel
    {
        public ObservableCollection<Course> StudentCourses { get; }
        private Student student;

        public StudentCoursesViewModel(Student student)
        {
            this.student = student;
            StudentCourses = new ObservableCollection<Course>(RepoFactory.GetRepo().GetStudentCourses(student.IDPerson));
            StudentCourses.CollectionChanged += StudentCourses_CollectionChanged;
        }

        private void StudentCourses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepoFactory.GetRepo().AddCourseStudent(StudentCourses[e.NewStartingIndex].IDCourse, student.IDPerson);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepoFactory.GetRepo().DeleteCourseStudent(e.OldItems.OfType<Course>().ToList()[0].IDCourse, student.IDPerson);
                    break;
            }
        }
    }
}
