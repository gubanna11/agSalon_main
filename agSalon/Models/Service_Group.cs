using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Service_Group
    {
        [Column("service_id")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }
        public GroupsOfServices Group { get; set; }
    }
}
