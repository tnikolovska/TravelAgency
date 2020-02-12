using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class AddArangement
    {
        public Hotel hotel { get; set; }
        public int HotelId { get; set; }
        public DateTime dateTime { get; set; }
        public int price { get; set; }
    }
}