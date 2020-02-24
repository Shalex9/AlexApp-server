using AlexApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Data
{
    public class AlexAppDbContext : DbContext
    {
        //public IConfiguration _config { get; }

        public AlexAppDbContext()
        {
        }

        public AlexAppDbContext(DbContextOptions<AlexAppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEF> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-QG8NAEF\\SQLEXPRESS;Database=alex_app;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Data Source=SQL5052.site4now.net;Initial Catalog=DB_A50B30_alexapp;User Id=DB_A50B30_alexapp_admin;Password=alexapp399779639");
                //optionsBuilder.UseSqlServer("Data Source=SQL5052.site4now.net;Initial Catalog=DB_A50B30_coserv;User Id=DB_A50B30_coserv_admin;Password=coserv399779639");
            }
        }
    }
}
