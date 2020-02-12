using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class ArrangementPrice
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel hotel { get; set; }
        public DateTime date { get; set; }
        public int price { get; set; }
    }
}