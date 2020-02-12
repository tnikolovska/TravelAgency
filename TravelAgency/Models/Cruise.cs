using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class Cruise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string image1 { get; set; }

        public string image2 { get; set; }

        public string image3 { get; set; }

        public string image4 { get; set; }

        public string image5 { get; set; }

        public string image6 { get; set; }
        public List<CruisePlan> cruisePlans { get; set; }
        public List<CruiseArrangement> cruiseArrangements { get; set; }
        public Cruise()
        {
            cruisePlans = new List<CruisePlan>();
            cruiseArrangements = new List<CruiseArrangement>();

        }
    }
}