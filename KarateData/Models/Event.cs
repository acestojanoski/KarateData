using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateData.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<Competitor> Competitors { get; set; }
    }
}