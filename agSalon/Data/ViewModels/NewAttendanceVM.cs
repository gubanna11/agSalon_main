using agSalon.Data.Enums;
using agSalon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.ViewModels
{
    public class NewAttendanceVM
    {
        [Column("client_id"), Required]
        public int ClientId { get; set; }

        [Column("group_id"),]

        public int GroupId { get; set; }

        [Column("service_id")]

        public int ServiceId { get; set; }

        [Column("worker_id"), Required]

        public int WorkerId { get; set; }

        [Column("date", TypeName = "date"), Required]

        public DateTime Date { get; set; }

        [Column("price")]

        public double Price { get; set; }

        [Column("rendered")]

        public YesNoEnum IsRendered { get; set; }

        [Column("paid")]

        public YesNoEnum IsPaid { get; set; }
    }
}
