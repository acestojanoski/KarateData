using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateData.Models
{
    public class Competitor
    {
        public int CompetitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}