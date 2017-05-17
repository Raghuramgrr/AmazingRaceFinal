using AmazingRace.Model;
using AmazingRace.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazingRace.Controllers
{
    public class LeaderBoardController : Controller
    {
        // GET: LeaderBoard
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeaderBoard
        public ActionResult LeaderBoardTicker()
        {
            return View();
        }

        [HttpPost]
        public void Update(String teamName, String position,String pitStpCrossed, String pitStpRem)
        {
            LeaderBoard leaderBoard = new LeaderBoard();
            leaderBoard.teamName = teamName;
            leaderBoard.Position = position;
            leaderBoard.PitstopsCrossed = pitStpCrossed;
            leaderBoard.PitstopsRemaining = pitStpRem;

            System.Diagnostics.Debug.WriteLine(teamName);
            System.Diagnostics.Debug.WriteLine(position);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<LeaderBoardHub>();
            hubContext.Clients.All.updateLeaderBoard(leaderBoard);


        }
    }
}