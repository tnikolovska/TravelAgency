using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class DistantDestination
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<string> images { get; set; }
        [Required]
        public string MainImage { get; set; }

        public DistantDestination()
        {
            Hotels = new List<Hotel>();
            images = new List<string>();
        }
    }
}