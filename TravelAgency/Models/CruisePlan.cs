using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class CruisePlan
    {
        [Key]
        public int Id { get; set; }
        public int Day { get; set; }
        public string Port { get; set; }
        public TimeSpan Departure { get; set; }
        public TimeSpan Arrival { get; set; }
        public Cruise cruise { get; set; }
        public int CruiseId { get; set; }
    }
}