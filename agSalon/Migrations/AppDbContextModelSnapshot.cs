﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using agSalon.Data;

namespace agSalon.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("agSalon.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_birth");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("initial");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("phone");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("agSalon.Models.GroupsOfServices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("groups_of_services");
                });

            modelBuilder.Entity("agSalon.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Services");
                });

            modelBuilder.Entity("agSalon.Models.Service_Group", b =>
                {
                    b.Property<int>("ServiceId")
                        .HasColumnType("int")
                        .HasColumnName("service_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.HasKey("ServiceId", "GroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ServiceId")
                        .IsUnique();

                    b.ToTable("Services_Groups");
                });

            modelBuilder.Entity("agSalon.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("address");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_birth");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("initial");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("phone");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("agSalon.Models.Worker_Group", b =>
                {
                    b.Property<int>("WorkerId")
                        .HasColumnType("int")
                        .HasColumnName("worker_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.HasKey("WorkerId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("Workers_Groups");
                });

            modelBuilder.Entity("agSalon.Models.Service_Group", b =>
                {
                    b.HasOne("agSalon.Models.GroupsOfServices", "Group")
                        .WithMany("Services_Groups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("agSalon.Models.Service", "Service")
                        .WithOne("Service_Group")
                        .HasForeignKey("agSalon.Models.Service_Group", "ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("agSalon.Models.Worker_Group", b =>
                {
                    b.HasOne("agSalon.Models.GroupsOfServices", "Group")
                        .WithMany("Workers_Groups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("agSalon.Models.Worker", "Worker")
                        .WithMany("Workers_Groups")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("agSalon.Models.GroupsOfServices", b =>
                {
                    b.Navigation("Services_Groups");

                    b.Navigation("Workers_Groups");
                });

            modelBuilder.Entity("agSalon.Models.Service", b =>
                {
                    b.Navigation("Service_Group");
                });

            modelBuilder.Entity("agSalon.Models.Worker", b =>
                {
                    b.Navigation("Workers_Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
