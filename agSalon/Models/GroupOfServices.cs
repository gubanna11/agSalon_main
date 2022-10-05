using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Models
{
    [Table("groups_of_services")]
    public class GroupsOfServices
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        [Column("name")]
        public string Name { get; set; }

        public List<Service_Group> Services_Groups { get; set; }
    }
}
