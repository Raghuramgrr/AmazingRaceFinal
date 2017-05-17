using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingRace.Model;

namespace AmazingRace.Data
{
    public class AmazingRaceRepository
    {
        AmazingRaceContext db = new AmazingRaceContext();


        public Login GetUser(Login login)
        {
            return db.Login.SingleOrDefault(l => l.UserId == login.UserId && l.Password == login.Password);
        }

        public Event GetEvent(int id)
        {
            return db.Event.Single(m => m.EventId == id);
        }

        public PitStop GetPitStop(int id)
        {
            return db.Pitstop.Single(m => m.PitStopId == id);
        }
            
        public void AddPitStops(String[] pitstops, String eventId)
        {

            for (int pitIndex = 0; pitIndex < pitstops.Length; pitIndex++)
            {
                System.Diagnostics.Debug.WriteLine(pitstops[pitIndex]);
                PitStop pitstop = new PitStop();
                pitstop.PitStopLocation = pitstops[pitIndex];
                pitstop.PitStopName = pitstops[pitIndex];
                pitstop.EventId = Int32.Parse(eventId);
                pitstop.PitStopOrder = pitIndex + 1;
                db.Pitstop.Add(pitstop);
                db.SaveChanges();
            }


        }

        public void AddEvent(Event eventrep)
        {
            db.Event.Add(eventrep);
    
        }

        public void AddPitstop(PitStop pitstoprep)
        {
            db.Pitstop.Add(pitstoprep);
        }   

        public void DeleteEvent(int id)
        {
            Event event1 = db.Event.Single(m => m.EventId == id);
            db.Event.Remove(event1);
        }

        public void DeletePitStop(int id)
        {
            PitStop pitstop1 = db.Pitstop.Single(m => m.PitStopId == id);
            db.Pitstop.Remove(pitstop1);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return db.Event;
        }

        public IEnumerable<PitStop> GetAllPitStops()
        {
            return db.Pitstop;
        }

        public Team GetTeam(int id)
        {
            return db.Team.Single(m => m.TeamId == id);
        }

        public List<Team> GetTeamForEvent(String eventName)
        {
            var teams = (from c in db.Team
                                   where c.EventName == eventName
                         select c).ToList();
            return teams;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return db.Team;
        }

        public void Add(Team teamrep)
        {
            db.Team.Add(teamrep);

        }

        public void Delete(int id)
        {
            Team team = db.Team.Single(m => m.TeamId == id);
            db.Team.Remove(team);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public List<PitStop> GetPitsForEvent(int id)
        {
            var pits = (from c in db.Pitstop
                                   where c.EventId == id
                                   select c).ToList();
            return pits;
        }
        public void DeletePitForEvent(int id)
        {
            List<PitStop> pitstop1 = (from c in db.Pitstop
                                where c.EventId == id
                                select c).ToList();
            for (int ind=0;ind< pitstop1.Count;ind++) {
                db.Pitstop.Remove(pitstop1[ind]);
            }
        }
    }

}
