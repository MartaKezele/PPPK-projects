using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PPPK_DZ4.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<int> SelectedActorIDs { get; set; }
        [Display(Name = "All actors")]
        public IEnumerable<SelectListItem> AllActors { get; set; }

        public IEnumerable<int> SelectedDirectorIDs { get; set; }
        [Display(Name = "All directors")]
        public IEnumerable<SelectListItem> AllDirectors { get; set; }
    }
}