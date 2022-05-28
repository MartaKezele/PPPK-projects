using PPPK_DZ2.ViewModels;
using System.Windows.Controls;

namespace PPPK_DZ2.Pages
{
    public class CourseFramedPage : Page
    {
        public CourseViewModel CourseViewModel { get; }
        public Frame Frame { get; set; }

        public CourseFramedPage(CourseViewModel courseViewModel)
        {
            CourseViewModel = courseViewModel;
        }
    }
}
