using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalHack.Models
{
    public class Referral
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int? ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}