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
    public class PitStopController : Controller
    {
        AmazingRaceRepository rep = new AmazingRaceRepository();
        AmazingRaceContext db = new AmazingRaceContext();

        // GET: PitStop
        public ActionResult Index()
        {
            IEnumerable<PitStop> pitstops = null;
            if (Session["userId"] != null)
            {
                pitstops = rep.GetAllPitStops();
            }
            else
            {
                return RedirectToAction("Index","Event");
            }
            return View(pitstops);
        }

        // GET: PitStop/Details/5
        public ActionResult Details(int id)
        {
            PitStop pitstop1 = rep.GetPitStop(id);
            return View();
        }

        // GET: PitStop/Create
        public ActionResult Create()
        {
           
            PitStop pitstop1 = new PitStop();
            IEnumerable<SelectListItem> items = db.Event.Select(c => new SelectListItem
            {
                Value = c.EventId.ToString(),
                Text = c.EventId.ToString()
            });
            ViewBag.EventNameList = items;
            return View();
        }

        [HttpPost]
        public ActionResult Create(String[] pitstops,String eventId)
        {

            rep.AddPitStops(pitstops, eventId);
            return View();
        }

        //// POST: PitStop/Create
        //[HttpPost]
        //public ActionResult Create(PitStop pitstop1)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        rep.AddPitstop(pitstop1);
        //        rep.Save();
        //        return RedirectToAction("Index");
        //    }

        //    catch (System.Exception e)
        //    {
        //        if (e.GetType() != typeof(DbEntityValidationException))
        //        {
        //            if (this.HttpContext.IsDebuggingEnabled)
        //            {
        //                ModelState.AddModelError(string.Empty, e.ToString());
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "Some technical error happened.");
        //            }
        //        }
        //    }
        //    return View();

        //}

        

        // GET: PitStop/Delete/5
        public ActionResult Delete(int id)
        {
            return View(rep.GetPitStop(id));
        }

        // POST: PitStop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PitStop pitstop1)
        {
            try
            {
                rep.DeletePitStop(id);
                rep.Save();
                // TODO: Add delete logic here

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

        public void GetPitStopLocations(int id)
        {
            var pits = rep.GetPitsForEvent(id);
            
        }

        // GET: PitStop/ViewEdit/5
        public ActionResult ViewEdit(int id)
        {
            List<PitStop> pitstops = rep.GetPitsForEvent(id);
            List<String> pits = new List<string>();

            Event event1 = rep.GetEvent(id);

            for (int ind = 0; ind < pitstops.Count; ind++)
            {
                pits.Add(pitstops[ind].PitStopLocation);
            }

            ViewBag.pitstops = pits;

            return View(event1);
        }

        [HttpPost]
        public ActionResult Edit(String[] pitstops, String eventId)
        {
            rep.DeletePitForEvent(Int32.Parse(eventId));
            rep.AddPitStops(pitstops, eventId);
            return RedirectToAction("Index");
        }
    }
}
