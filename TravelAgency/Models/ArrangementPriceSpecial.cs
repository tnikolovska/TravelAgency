using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class ArrangementPriceSpecial
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int price { get; set; }
        public HotelSpecialOffer hotel { get; set; }
        public int HotelSpecialOfferId { get; set; }
    }
}