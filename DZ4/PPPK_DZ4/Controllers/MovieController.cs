using PPPK_DZ4.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PPPK_DZ4.Controllers
{
    public class MovieController : Controller
    {
        ~MovieController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        private readonly ModelContainer db = new ModelContainer();

        // GET: Movie
        public ActionResult Index()
        {
            return View(db.Movies.OrderBy(movie => movie.Title));
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            return ShowMovie(id);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            List<SelectListItem> allActorsSelectList = new List<SelectListItem>();
            List<SelectListItem> allDirectorsSelectList = new List<SelectListItem>();

            foreach (Actor actor in db.Actors)
            {
                SelectListItem actorListItem = new SelectListItem()
                {
                    Text = actor.FirstName + " " + actor.LastName,
                    Value = actor.IDActor.ToString(),
                };
                allActorsSelectList.Add(actorListItem);
            }

            foreach (Director director in db.Directors)
            {
                SelectListItem directorListItem = new SelectListItem()
                {
                    Text = director.FirstName + " " + director.LastName,
                    Value = director.IDDirector.ToString(),
                };
                allDirectorsSelectList.Add(directorListItem);
            }

            allActorsSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            allDirectorsSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            MovieViewModel movieViewModel = new MovieViewModel()
            {
                AllActors = allActorsSelectList,
                AllDirectors = allDirectorsSelectList
            };

            return View(movieViewModel);
        }

        // POST: Movie/Create
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateConfirmed(
            [Bind(Include = "Movie")] MovieViewModel movieViewModel, 
            IEnumerable<HttpPostedFileBase> files, 
            IEnumerable<int> selectedActorIDs,
            IEnumerable<int> selectedDirectorIDs)
        {
            if (ModelState.IsValid)
            {
                movieViewModel.Movie.UploadedFiles = new List<UploadedFile>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new UploadedFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };

                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }

                        movieViewModel.Movie.UploadedFiles.Add(picture);
                    }
                }

                if (selectedActorIDs != null)
                {
                    foreach (var id in selectedActorIDs)
                    {
                        Actor actor = db.Actors.Find(id);
                        movieViewModel.Movie.Actors.Add(actor);
                    }
                }

                if (selectedDirectorIDs != null)
                {
                    foreach (var id in selectedDirectorIDs)
                    {
                        Director director = db.Directors.Find(id);
                        movieViewModel.Movie.Directors.Add(director);
                    }
                }

                db.Movies.Add(movieViewModel.Movie);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies
                .Include(m => m.UploadedFiles)
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .SingleOrDefault(m => m.IDMovie == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> allActorsSelectList = new List<SelectListItem>();
            List<SelectListItem> allDirectorsSelectList = new List<SelectListItem>();

            foreach (Actor actor in db.Actors)
            {
                bool selected = false;
                if (movie.Actors.Contains(actor))
                {
                    selected = true;
                }
                SelectListItem actorListItem = new SelectListItem()
                {
                    Text = actor.FirstName + " " + actor.LastName,
                    Value = actor.IDActor.ToString(),
                    Selected = selected
                };
                allActorsSelectList.Add(actorListItem);
            }

            foreach (Director director in db.Directors)
            {
                bool selected = false;
                if (movie.Directors.Contains(director))
                {
                    selected = true;
                }
                SelectListItem directorListItem = new SelectListItem()
                {
                    Text = director.FirstName + " " + director.LastName,
                    Value = director.IDDirector.ToString(),
                    Selected = selected
                };
                allDirectorsSelectList.Add(directorListItem);
            }

            allActorsSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            allDirectorsSelectList.Sort((a, b) => a.Text.CompareTo(b.Text));
            MovieViewModel movieViewModel = new MovieViewModel()
            {
                Movie = movie,
                AllActors = allActorsSelectList,
                AllDirectors = allDirectorsSelectList
            };

            return View(movieViewModel);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id, IEnumerable<HttpPostedFileBase> files, MovieViewModel movieViewModel)
        {
            Movie movie = db.Movies.Find(id);
            movie.Title = movieViewModel.Movie.Title;
            movie.Description = movieViewModel.Movie.Description;
            if (movieViewModel.Movie.UploadedFiles == null)
            {
                movieViewModel.Movie.UploadedFiles = new List<UploadedFile>();
            }
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var picture = new UploadedFile
                    {
                        Name = System.IO.Path.GetFileName(file.FileName),
                        ContentType = file.ContentType
                    };

                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        picture.Content = reader.ReadBytes(file.ContentLength);
                    }

                    movie.UploadedFiles.Add(picture);
                }
            }
            if (movieViewModel.SelectedActorIDs != null)
            {
                movie.Actors.Clear();
                foreach (var actorID in (movieViewModel.SelectedActorIDs))
                {
                    Actor actor = db.Actors.Find(actorID);
                    movie.Actors.Add(actor);
                }
            }

            if (movieViewModel.SelectedDirectorIDs != null)
            {
                movie.Directors.Clear();
                foreach (var directorID in (movieViewModel.SelectedDirectorIDs))
                {
                    Director director = db.Directors.Find(directorID);
                    movie.Directors.Add(director);
                }
            }

            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            return ShowMovie(id);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.MovieIDMovie == id));

            Movie movie = db.Movies.Find(id);
            movie.Actors.Clear();
            movie.Directors.Clear();

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private ActionResult ShowMovie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies
                .Include(m => m.UploadedFiles)
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .SingleOrDefault(m => m.IDMovie == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

    }
}
