using agSalon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<GroupsOfServices> Groups { get; set; }
        public DbSet<Service_Group> Services_Groups { get; set; }
        
        public DbSet<Client>Clients { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.Entity<Service_Group>().HasKey(sg => new { sg.ServiceId, sg.GroupId });

            modelBuilder.Entity<Service_Group>().HasOne(g => g.Group).WithMany(sg => sg.Services_Groups);

            modelBuilder.Entity<Service_Group>().HasOne(s => s.Service).WithOne(sg => sg.Service_Group);

            base.OnModelCreating(modelBuilder);
        }

    }
}
