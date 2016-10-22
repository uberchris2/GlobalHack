﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlobalHack.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Birth Year")]
        public int BirthYear { get; set; }
        public int Gender { get; set; }
        [Display(Name = "Number of Children")]
        public int NumChildren { get; set; }
        public bool Pregnant { get; set; }
        public bool Transgender { get; set; }
        [Display(Name = "Registered Sex Offender")]
        public bool SexOffender { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Referral> Referrals { get; set; }
    }
}