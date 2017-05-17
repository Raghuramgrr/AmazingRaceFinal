using System.Collections.Generic;
using System.Web.Mvc;
using AmazingRace.Model;
using AmazingRace.Data;
using System.Data.Entity.Validation;
using System;

namespace AmazingRace.Controllers
{
    public class EventController : Controller
    {
        AmazingRaceRepository rep = new AmazingRaceRepository();

        // GET: Event/Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            var user = rep.GetUser(login);
            if (null != user)
            {
                Session["userId"] = user.UserId;
                return RedirectToAction("Events");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect UserId or Password");
            }
            return View();
        }
    
        public ActionResult Events()
        {

            if (Session["userId"] != null)
            {
                IEnumerable<Event> events = rep.GetAllEvents();
                return View(events);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            Event event1 = rep.GetEvent(id);
            return View(event1);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            Event event1 = new Event();
            return View(event1);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event event1)
        {
            try
            {
                // TODO: Add insert logic here     
                rep.AddEvent(event1);
                rep.Save();
                return RedirectToAction("Index");

            }
            catch (System.Exception e)
            {
                if (e.GetType() != typeof(DbEntityValidationException))
                {
                    if (this.HttpContext.IsDebuggingEnabled)
                    {
                        ModelState.AddModelError(string.Empty, e.ToString());
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Some technical error happened.");
                    }
                }
            }
                return View(event1);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            Event event1 = rep.GetEvent(id);
            return View(event1);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Event event1 = rep.GetEvent(id);
                UpdateModel(event1);

                rep.Save();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {

            return View(rep.GetEvent(id));
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event event1)
        {
            try
            {
                // TODO: Add delete logic here
                rep.DeleteEvent(id);
                rep.Save();
                return RedirectToAction("Index");   
            }
            catch(Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }

                return View();
            }
        }
    }
}
