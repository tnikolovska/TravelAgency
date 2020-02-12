using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class AddArrangementSpecialOffer
    {
        public HotelSpecialOffer hotel { get; set; }
        public int HotelSpecialOfferId { get; set; }
        public DateTime dateTime { get; set; }
        public int price { get; set; }
    }
}