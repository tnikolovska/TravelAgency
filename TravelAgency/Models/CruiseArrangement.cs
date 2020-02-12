using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAgency.Models
{
    public class CruiseArrangement
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public Cruise cruise { get; set; }
        public int CruiseId { get; set; }
        public int InsideCabin { get; set; }
        public int BalconyCabin { get; set; }
        public int OceanViewCabin { get; set; }
    }
}