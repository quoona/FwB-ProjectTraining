using Microsoft.EntityFrameworkCore;
using System;
using FwB.Models;


namespace FwB.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Item> Item { get; set; }



    }
}