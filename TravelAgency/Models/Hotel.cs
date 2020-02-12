using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DistantDestinationId { get; set; }
        public DistantDestination destination { get; set; }
        [Required]
        public string Description { get; set; }
        public List<ArrangementPrice> arrangements { get; set; }
        public List<decimal> prices { get; set; }
        public string URLAddress { get; set; }
        [Required]
        public string MainImage { get; set; }

        public string image1 { get; set; }

        public string image6 { get; set; }

        public string image2 { get; set; }

        public string image3 { get; set; }

        public string image4 { get; set; }

        public string image5 { get; set; }
        [Required]
        public int stars { get; set; }
        public Hotel()
        {
            arrangements = new List<ArrangementPrice>();

        }
    }
}