using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class Agenda
    {
        public Cruise cruise { get; set; }
        public int CruiseId { get; set; }
        public int Day { get; set; }
        public string Port { get; set; }
        public TimeSpan Departure { get; set; }
        public TimeSpan Arrival { get; set; }
    }
}