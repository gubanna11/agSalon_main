using agSalon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.ViewModels
{
    public class ServiceVM
    {
        
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [Column("price")]
        public double Price { get; set; }

        public int GroupId { get; set; }
    }
}
