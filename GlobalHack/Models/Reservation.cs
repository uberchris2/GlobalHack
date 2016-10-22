using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalHack.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ShelterId { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }
    }
}