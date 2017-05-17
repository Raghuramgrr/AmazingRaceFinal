using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AmazingRace.Model
{
    public class PitStop
    {

        public int PitStopId { get; set; }

        public String PitStopName { get; set; }

        public String PitStopLocation { get; set; }

        public int PitStopOrder { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

    }

    public class PitstopMetadata
    {
        
        public int PitStopId { get; set; }

        public String PitStopName { get; set; }

        public String PitStopLocation { get; set; }

        public int PitStopOrder { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
