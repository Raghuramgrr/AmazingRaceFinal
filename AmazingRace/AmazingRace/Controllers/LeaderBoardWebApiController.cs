using AmazingRace.Model;
using AmazingRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LeaderBoardWebApiController : ApiController
    {

        // PUT api/values/
        public void Put(List<string> val)
        {
            System.Diagnostics.Debug.WriteLine("teamName: " + val[0]);
            System.Diagnostics.Debug.WriteLine("Position: " + val[1]);
            System.Diagnostics.Debug.WriteLine("pitsCross: " + val[2]);
            System.Diagnostics.Debug.WriteLine("pitsRem: " + val[3]);

            LeaderBoard leaderBoard = new LeaderBoard();
            leaderBoard.teamName = val[0];
            leaderBoard.Position = val[1];
            leaderBoard.PitstopsCrossed = val[2];
            leaderBoard.PitstopsRemaining = val[3];

            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<LeaderBoardHub>();
            hubContext.Clients.All.updateLeaderBoard(leaderBoard);
        }
    }
}
