using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class PriceList
    {
        public Cruise cruise { get; set; }
        public int CruiseId { get; set; }
        public DateTime date { get; set; }
        public int InsideCabins { get; set; }
        public int OcenViewCabins { get; set; }
        public int BalconyCabins { get; set; }
    }
}