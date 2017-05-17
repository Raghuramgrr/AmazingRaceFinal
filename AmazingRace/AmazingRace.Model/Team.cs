using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingRace.Model
{
    public class Team
    {
        public int TeamId { get; set; }

        public String TeamName { get; set; }

        public String TeamDescription { get; set; }

        public String EventName { get; set; }

        public virtual List<Team> teams { get; set; }


    }
}
