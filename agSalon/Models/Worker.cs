﻿using agSalon.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Worker
    {
        //[Column("id"), Required, Key]
        //public int Id { get; set; }

        [Column("id")]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public Client Client { get; set; }


        [Column("address"), Required]
        [StringLength(45)]
        public string Address { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        public List<Worker_Group> Workers_Groups { get; set; }
    }
}
