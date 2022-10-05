using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Client
    {
        [Key]
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("surname"), Required]
        [StringLength(45)]
        public string Surname { get; set; }

        [Column("initial"), Required]
        [StringLength(5)]
        public string Initials { get; set; }

        [Column("phone"), Required]
        [StringLength(13)]
        public string Phone { get; set; }

        [Column("date_birth", TypeName = "date"), Required]
        public DateTime DateBirth { get; set; }
    }
}
