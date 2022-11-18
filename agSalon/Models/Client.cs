﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace agSalon.Models
{
    public class Client: IdentityUser
    {
        //[Key]
        //[Column("id"), Required]
        //public int Id { get; set; }

        [Column("name"), Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Column("surname"), Required]
        [StringLength(45)]
        public string Surname { get; set; }

        [Column("phone"), Required]
        [StringLength(13), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Column("date_birth", TypeName = "date"), Required]
        public DateTime DateBirth { get; set; }
    }
}
