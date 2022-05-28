using PPPK_DZ4.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PPPK_DZ4.Controllers
{
    public class DirectorController : Controller
    {
        ~DirectorController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        private readonly ModelContainer db = new ModelContainer();

        // GET: Director
        public ActionResult Index()
        {
            return View(db.Directors.OrderBy(director => director.FirstName + director.LastName));
        }

        // GET: Director/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Director director = db.Directors
                .Include(d => d.Movies)
                .SingleOrDefault(d => d.IDDirector == id);

            if (director == null)
            {
                return HttpNotFound();
            }

            return View(director);
        }

        // GET: Director/Create
        public ActionResult Create()
        {
            List<SelectListItem> allMoviesSelectList = new List<SelectListItem>();

            foreach (Movie movie in db.Movies)
            {
                SelectListItem movieListItem = new SelectListItem()
                {
                    Text = movie.Title,
                    Value = movie.IDMovie.ToString(),
                };
                allMoviesSelectList.Add(movieListItem);
            }

            allMoviesSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            DirectorViewModel directorViewModel = new DirectorViewModel()
            {
                AllMovies = allMoviesSelectList
            };

            return View(directorViewModel);
        }

        // POST: Director/Create
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateConfirmed([Bind(Include = "Director")] DirectorViewModel directorViewModel, IEnumerable<int> selectedMovieIDs)
        {
            if (ModelState.IsValid)
            {
                if (selectedMovieIDs != null)
                {
                    foreach (var id in selectedMovieIDs)
                    {
                        Movie movie = db.Movies.Find(id);
                        directorViewModel.Director.Movies.Add(movie);
                    }
                }

                db.Directors.Add(directorViewModel.Director);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Director/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Director director = db.Directors
                .Include(d => d.Movies)
                .SingleOrDefault(d => d.IDDirector == id);

            if (director == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> allMoviesSelectList = new List<SelectListItem>();

            foreach (Movie movie in db.Movies)
            {
                bool selected = false;
                if (director.Movies.Contains(movie))
                {
                    selected = true;
                }
                SelectListItem movieListItem = new SelectListItem()
                {
                    Text = movie.Title,
                    Value = movie.IDMovie.ToString(),
                    Selected = selected
                };
                allMoviesSelectList.Add(movieListItem);
            }

            allMoviesSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            DirectorViewModel directorViewModel = new DirectorViewModel()
            {
                Director = director,
                AllMovies = allMoviesSelectList
            };

            return View(directorViewModel);
        }

        // POST: Director/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int? id, DirectorViewModel directorViewModel)
        {
            Director director = db.Directors.Find(id);
            director.FirstName = directorViewModel.Director.FirstName;
            director.LastName = directorViewModel.Director.LastName;

            if (TryUpdateModel(director, "", new string[] { "FirstName", "LastName" }))
            {
                if (directorViewModel.SelectedMovieIDs != null)
                {
                    director.Movies.ToList().ForEach(movie =>
                    {
                        if (!directorViewModel.SelectedMovieIDs.Contains(movie.IDMovie))
                        {
                            director.Movies.Remove(movie);
                        }
                    });

                    foreach (var movieID in directorViewModel.SelectedMovieIDs)
                    {
                        Movie movie = db.Movies.Find(movieID);
                        if (!director.Movies.Contains(movie))
                        {
                            director.Movies.Add(movie);
                        }
                    }
                }
                db.Entry(director).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(director);
        }

        // GET: Director/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Director director = db.Directors
                .Include(d => d.Movies)
                .SingleOrDefault(d => d.IDDirector == id);

            if (director == null)
            {
                return HttpNotFound();
            }

            return View(director);
        }

        // POST: Director/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = db.Directors.Find(id);
            director.Movies.Clear();
            db.Directors.Remove(director);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
