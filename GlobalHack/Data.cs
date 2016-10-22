using GlobalHack.Models;

namespace GlobalHack
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Data : DbContext
    {
        public Data()
            : base("name=Data")
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }
    }
}