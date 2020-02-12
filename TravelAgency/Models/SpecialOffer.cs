using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class SpecialOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<HotelSpecialOffer> hotels { get; set; }
        [Required]
        public string MainImage { get; set; }
        public SpecialOffer()
        {
            hotels = new List<HotelSpecialOffer>();
        }
    }
}