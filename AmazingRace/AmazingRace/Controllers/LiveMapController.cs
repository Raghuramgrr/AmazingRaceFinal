using AmazingRace.Data;
using AmazingRace.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazingRace.Controllers
{
    public class LiveMapController : Controller
    {

        AmazingRaceRepository rep = new AmazingRaceRepository();
        // GET: LiveMap/Index/5
        public ActionResult Index(String id)
        {

            System.Diagnostics.Debug.WriteLine(id);
            Event event1 = rep.GetEvent(Int32.Parse(id));
            List<Team> teamList = rep.GetTeamForEvent(event1.EventName);
            List<PitStop> pitList = rep.GetPitsForEvent(Int32.Parse(id));
            List<String> teamName = new List<string>();

            List<String> strtLoc = new List<String>();
            List<String> endLoc = new List<String>();
            List<String> wayPts = new List<String>();

            for (int strtInd = 0; strtInd < teamList.Count; strtInd++)
            {
                strtLoc.Add(pitList[0].PitStopLocation);
            }

            for (int endInd = 0; endInd < teamList.Count; endInd++)
            {
                endLoc.Add(pitList[pitList.Count-1].PitStopLocation);
            }

            for (int wayPtsInd = 1; wayPtsInd < pitList.Count-1; wayPtsInd++)
            {
                wayPts.Add(pitList[wayPtsInd].PitStopLocation);
            }

            for (int teamInd = 0; teamInd < teamList.Count; teamInd++)
            {
                Team team = teamList[teamInd];
                teamName.Add(team.TeamName);
            }

            ViewBag.strtLoc = strtLoc;
            ViewBag.endLoc = endLoc;
            ViewBag.wayPts = wayPts;
            ViewBag.teamName = teamName;

            return View();
        }
    }
}
