using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class AddHotelToDestination
    {
        public DistantDestination destination { get; set; }
        public int DistantDestinationId { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public List<Hotel> hotelNames { get; set; }
        public AddHotelToDestination()
        {
            hotelNames = new List<Hotel>();
        }
    }
}