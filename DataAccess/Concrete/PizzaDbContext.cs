using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class PizzaDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Buraya mssql bağlantı dizesi yazılacak") // MSSQL Bağlantı dizesi
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHelper> OrderHelpers { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CampaignSlider> CampaignSliders { get; set; }
    }
}

