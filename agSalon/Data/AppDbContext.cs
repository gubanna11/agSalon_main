﻿using agSalon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data
{
    public class AppDbContext: IdentityDbContext<Client>
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<GroupOfServices> Groups { get; set; }
        public DbSet<Service_Group> Services_Groups { get; set; }

        //public DbSet<Client> Clients { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public DbSet<Worker_Group> Workers_Groups { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /**/
            modelBuilder.Entity<Service_Group>().HasKey(sg => new { sg.ServiceId, sg.GroupId });

            modelBuilder.Entity<Service_Group>().HasOne(g => g.Group).WithMany(sg => sg.Services_Groups);

            modelBuilder.Entity<Service_Group>().HasOne(s => s.Service).WithOne(sg => sg.Service_Group);

            /**/
            modelBuilder.Entity<Worker_Group>().HasKey(wg => new { wg.WorkerId, wg.GroupId });

            modelBuilder.Entity<Worker_Group>().HasOne(w => w.Group).WithMany(wg => wg.Workers_Groups);


            modelBuilder.Entity<Attendance>().HasIndex(att => new { att.ClientId, att.Date, att.ServiceId }).IsUnique();
            modelBuilder.Entity<Attendance>().HasIndex(att => new { att.WorkerId, att.Date, att.ServiceId }).IsUnique();
            modelBuilder.Entity<Attendance>().Property(a => a.Time).HasColumnType("time");

            modelBuilder.Entity<GroupOfServices>().HasIndex(g => g.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
