using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlobalHack.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }
        public bool NoShow { get; set; }
    }
}