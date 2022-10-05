using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Models
{
    public class Service_Group
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int GroupId { get; set; }
        public GroupOfServices Group { get; set; }
    }
}
