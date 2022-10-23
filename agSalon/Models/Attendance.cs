using agSalon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Attendance
    {
        [Column("client_id"), Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Column("group_id"), ]

        public int? GroupId { get; set; }
        public GroupsOfServices Group { get; set; }

        [Column("service_id")]
        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        [Column("worker_id"), Required]
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }


        [Column("date", TypeName = "date"), Required]
        public DateTime Date { get; set; }

        [Column("time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public DateTime? Time { get; set; }


        [Column("price")]
        public double Price { get; set; }

        [Column("rendered")]
        public IsRendered IsRendered { get; set; }

    }
}
