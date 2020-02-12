using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class HotelSpecialOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public SpecialOffer specialOffer { get; set; }
        public int SpecialOfferId { get; set; }
        [Required]
        public int stars { get; set; }
        [Required]
        public string mainImage { get; set; }

        public string image1 { get; set; }

        public string image2 { get; set; }

        public string image3 { get; set; }

        public string image4 { get; set; }

        public string image5 { get; set; }

        public string image6 { get; set; }
        public List<ArrangementPriceSpecial> arrangementPrices { get; set; }
        public HotelSpecialOffer()
        {
            arrangementPrices = new List<ArrangementPriceSpecial>();
        }
    }
}