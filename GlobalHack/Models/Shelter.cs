using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlobalHack.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        [Display(Name = "Shelter")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public int Beds { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int GenderRestriction { get; set; }
        public bool PregnantOnly { get; set; }
        public bool SexOffenderRestriction { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Referral> Referrals { get; set; }
    }
}