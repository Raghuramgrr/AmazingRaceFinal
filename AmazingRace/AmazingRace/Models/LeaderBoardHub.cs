using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AmazingRace.Model;

namespace AmazingRace.Models
{
    [HubName("leaderBoardHub")]
    public class LeaderBoardHub : Hub
    {
        private readonly LeaderBoardTicker _leaderBoardTicker;

        public LeaderBoardHub() : this(LeaderBoardTicker.Instance) { }

        public LeaderBoardHub(LeaderBoardTicker leaderBoardTicker)
        {
            _leaderBoardTicker = leaderBoardTicker;
        }

        public IEnumerable<LeaderBoard> GetAllTeams()
        {
            return _leaderBoardTicker.GetAllTeams();
        }
    }
}