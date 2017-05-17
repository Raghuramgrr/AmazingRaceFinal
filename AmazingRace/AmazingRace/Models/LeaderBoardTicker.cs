using AmazingRace.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AmazingRace.Models
{
    public class LeaderBoardTicker
    {
        // Singleton instance
        private readonly static Lazy<LeaderBoardTicker> _instance = new Lazy<LeaderBoardTicker>(() => new LeaderBoardTicker(GlobalHost.ConnectionManager.GetHubContext<LeaderBoardHub>().Clients));

        private readonly ConcurrentDictionary<string, LeaderBoard> _leaderBoard = new ConcurrentDictionary<string, LeaderBoard>();

        private readonly object _updateLeaderBoardLock = new object();

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000);
        private readonly Random _updateOrNotRandom = new Random();

        //private readonly Timer _timer;
        private volatile bool _updatingLeaderBoard = false;

        private LeaderBoardTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _leaderBoard.Clear();
            var leaderBoards = new List<LeaderBoard>
            {
                new LeaderBoard { teamName = "Team1", Position = "0", PitstopsCrossed="0",PitstopsRemaining="0" },
                new LeaderBoard { teamName = "Team2", Position = "0", PitstopsCrossed="0",PitstopsRemaining="0" },
                new LeaderBoard { teamName = "Team3", Position = "0", PitstopsCrossed="0",PitstopsRemaining="0" },
                new LeaderBoard { teamName = "Team4", Position = "0", PitstopsCrossed="0",PitstopsRemaining="0" },
                new LeaderBoard { teamName = "Team5", Position = "0", PitstopsCrossed="0",PitstopsRemaining="0" },
            };
            leaderBoards.ForEach(
            leaderBoard => _leaderBoard.TryAdd(leaderBoard.teamName, leaderBoard));

            //_timer = new Timer(UpdateLeaderBoard, null, _updateInterval, _updateInterval);

        }

        public static LeaderBoardTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public IEnumerable<LeaderBoard> GetAllTeams()
        {
            return _leaderBoard.Values;
        }

        private void UpdateLeaderBoard(object state)
        {
            lock (_updateLeaderBoardLock)
            {
                if (!_updatingLeaderBoard)
                {
                    _updatingLeaderBoard = true;

                    foreach (var leaderBoard in _leaderBoard.Values)
                    {
                        if (TryUpdateLeaderBoard(leaderBoard))
                        {
                            BroadcastLeaderBoard(leaderBoard);
                        }
                    }

                    _updatingLeaderBoard = false;
                }
            }
        }

        private bool TryUpdateLeaderBoard(LeaderBoard leaderBoard)
        {
            // Randomly choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                return false;
            }
            
            return true;
        }

        private void BroadcastLeaderBoard(LeaderBoard leaderBoard)
        {
            Clients.All.updateLeaderBoard(leaderBoard);
        }

    }
}