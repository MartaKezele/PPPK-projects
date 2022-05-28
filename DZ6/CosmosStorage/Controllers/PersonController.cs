using CosmosStorage.Dao;
using CosmosStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace CosmosStorage.Controllers
{
    public class PersonController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;

        // GET: Person
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM PERSON"));
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return await ShowPersonAsync(id);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, FirstName, LastName, Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.CreatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return await ShowPersonAsync(id);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, FirstName, LastName, Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return await ShowPersonAsync(id);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "Id, FirstName, LastName, Age")] Person person)
        {
            await service.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }

        private async Task<ActionResult> ShowPersonAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var person = await service.GetPersonAsync(id);

            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }
    }
}
