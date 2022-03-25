using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpisconApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EpisconApi.DBContexts
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasOne(user => user.Address);
            builder.Entity<User>().HasMany(user => user.PhoneNumbers);
            builder.Entity<Purchase>().HasOne(purch => purch.User);
            builder.Entity<Purchase>().HasOne(purch => purch.Product);
            builder.Entity<Rating>().HasOne(rating => rating.Product);

            builder.Seed();
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
