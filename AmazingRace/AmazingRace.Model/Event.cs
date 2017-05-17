using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingRace.Model
{
    [MetadataType(typeof(EventMetadata))]
    public class Event
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventId { get; set; }

        public String EventName { get; set; }

        public String EventLocation { get; set; }

        public DateTime EventTime { get; set; }

        public virtual List<PitStop> pitstop { get; set; }
    }

    public class EventMetadata
    {
        public int EventId { get; set; }

        [DisplayName("Event Name")]
        public String EventName { get; set; }

        [DisplayName("Event Location")]
        [Required(ErrorMessage = " Set the Location")]
        public String EventLocation { get; set; }

        [DisplayName("Event Time")]
        public DateTime EventTime { get; set; }

    }
}
