﻿using agSalon.Data.Base;
using agSalon.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Attendance:IEntityBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("client_id"), Required]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        [Column("group_id"), ]

        public int? GroupId { get; set; }
        public GroupOfServices Group { get; set; }

        [Column("service_id")]
        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        [Column("worker_id"), Required]
        public string WorkerId { get; set; }
        public Worker Worker { get; set; }


        [Column("date", TypeName = "date"), Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }

        [Column("time")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}", NullDisplayText = "")]
        public TimeSpan? Time { get; set; }


        [Column("price")]
        public double Price { get; set; }

        [Column("rendered")]
        public YesNoEnum IsRendered { get; set; }

        [Column("paid")]
        public YesNoEnum IsPaid { get; set; }

    }
}
