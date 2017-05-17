using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazingRace.Model;
using AmazingRace.Data;
using System.Data.Entity.Validation;


namespace AmazingRace.Controllers
{
    public class TeamController : Controller
    {

        AmazingRaceRepository rep = new AmazingRaceRepository();
        // GET: Team
        public ActionResult Index()
        {
            IEnumerable<Team> team = null;
            if (Session["userId"] != null)
            {
                team = rep.GetAllTeams();
            }
            else
            {
                return RedirectToAction("Index", "Event");
            }
            return View(team);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            Team team = rep.GetTeam(id);
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            AmazingRaceContext db = new AmazingRaceContext();
            Team team = new Team();
            IEnumerable<SelectListItem> items = db.Event.Select(c => new SelectListItem
            {
                Value = c.EventName,
                Text = c.EventName
            });
            ViewBag.EventNameList = items;
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(Team team)
        {
            try
            {
                rep.Add(team);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(DbEntityValidationException))
                {
                    if (this.HttpContext.IsDebuggingEnabled)
                    {
                        ModelState.AddModelError(string.Empty,
                        e.ToString());
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Some technical error happened.");
                    }
                }
                return View(team);
            }
        }
      
          private AmazingRaceContext db = new AmazingRaceContext();
        // GET: Team/Edit/5
        public ActionResult Edit(int id = 0)
        {
            string selected = (from sub in db.Team
                               where sub.TeamId == id
                               select sub.EventName).FirstOrDefault();
            ViewBag.EventName = new SelectList(db.Event, "EventName", "EventName", selected);

            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Team team = rep.GetTeam(id);
                UpdateModel(team);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
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

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View(rep.GetTeam(id));
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Team team)
        {
            try
            {
                rep.Delete(id);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View(team);
            }
        }
    }
}
