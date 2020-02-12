using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class AddHotelSpecialOffer
    {
        public SpecialOffer specialOffer { get; set; }
        public int SpecialOfferId { get; set; }
        public HotelSpecialOffer Hotel { get; set; }
        public int HotelId { get; set; }
        public List<HotelSpecialOffer> hotelNames { get; set; }
        public AddHotelSpecialOffer()
        {
            hotelNames = new List<HotelSpecialOffer>();
        }
    }
}