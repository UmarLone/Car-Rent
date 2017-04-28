using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Code;
using Models;

namespace Repositories
{
    public class Repository : DbContext
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<RentalPlan> RentalPlan { get; set; }
        public DbSet<CarRentalPlan> CarRentalPlan { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<StaticContent> StaticContent { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<User> User { get; set; }
    }
}