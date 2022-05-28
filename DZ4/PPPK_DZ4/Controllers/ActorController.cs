using PPPK_DZ4.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PPPK_DZ4.Controllers
{
    public class ActorController : Controller
    {
        ~ActorController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        private readonly ModelContainer db = new ModelContainer();

        // GET: Actor
        public ActionResult Index()
        {
            return View(db.Actors.OrderBy(actor => actor.FirstName + actor.LastName));
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Actor actor = db.Actors
                .Include(a => a.Movies)
                .SingleOrDefault(a => a.IDActor == id);

            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        // GET: Actor/Create
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
            ActorViewModel actorViewModel = new ActorViewModel()
            {
                AllMovies = allMoviesSelectList
            };

            return View(actorViewModel);
        }

        // POST: Actor/Create
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateConfirmed([Bind(Include = "Actor")] ActorViewModel actorViewModel, IEnumerable<int> selectedMovieIDs)
        {
            if (ModelState.IsValid)
            {
                if (selectedMovieIDs != null)
                {
                    foreach (var id in selectedMovieIDs)
                    {
                        Movie movie = db.Movies.Find(id);
                        actorViewModel.Actor.Movies.Add(movie);
                    }
                }

                db.Actors.Add(actorViewModel.Actor);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Actor actor = db.Actors
                .Include(a => a.Movies)
                .SingleOrDefault(a => a.IDActor == id);

            if (actor == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> allMoviesSelectList = new List<SelectListItem>();

            foreach (Movie movie in db.Movies)
            {
                bool selected = false;
                if (actor.Movies.Contains(movie))
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
            ActorViewModel actorViewModel = new ActorViewModel()
            {
                Actor = actor,
                AllMovies = allMoviesSelectList
            };

            return View(actorViewModel);

        }

        // POST: Actor/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int? id, ActorViewModel actorViewModel)
        {
            Actor actor = db.Actors.Find(id);
            actor.FirstName = actorViewModel.Actor.FirstName;
            actor.LastName = actorViewModel.Actor.LastName;

            if (TryUpdateModel(actor, "", new string[] { "FirstName", "LastName"}))
            {
                if (actorViewModel.SelectedMovieIDs != null)
                {
                    actor.Movies.ToList().ForEach(movie =>
                    {
                        if (!actorViewModel.SelectedMovieIDs.Contains(movie.IDMovie))
                        {
                            actor.Movies.Remove(movie);
                        }
                    });

                    foreach (var movieID in actorViewModel.SelectedMovieIDs)
                    {
                        Movie movie = db.Movies.Find(movieID);
                        if (!actor.Movies.Contains(movie))
                        {
                            actor.Movies.Add(movie);
                        }
                    }
                }
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Actor actors = db.Actors
                .Include(a => a.Movies)
                .SingleOrDefault(a => a.IDActor == id);

            if (actors == null)
            {
                return HttpNotFound();
            }

            return View(actors);
        }

        // POST: Actor/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);
            actor.Movies.Clear();
            db.Actors.Remove(actor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
