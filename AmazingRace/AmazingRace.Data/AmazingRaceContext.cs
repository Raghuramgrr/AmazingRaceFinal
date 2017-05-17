using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using AmazingRace.Model;
using System.Threading.Tasks;

namespace AmazingRace.Data
{
    public class AmazingRaceContext : DbContext
    {
        public AmazingRaceContext()
                : base("AmazingRaceNew")
        {
        }

        public DbSet<Login> Login { get; set; }

        public DbSet<PitStop> Pitstop { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Team> Team { get; set; }

    }
}
