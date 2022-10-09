using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Worker_Group
    {
        [Column("worker_id")]
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }
        public GroupsOfServices Group { get; set; }
    }
}
