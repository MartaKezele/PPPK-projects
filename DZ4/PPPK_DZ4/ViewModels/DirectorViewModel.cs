using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PPPK_DZ4.ViewModels
{
    public class DirectorViewModel
    {
        public Director Director { get; set; }
        public IEnumerable<int> SelectedMovieIDs { get; set; }
        [Display(Name = "All movies")]
        public IEnumerable<SelectListItem> AllMovies { get; set; }
    }
}