using agSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.ViewModels
{
    public class AttendanceDropdownsVM
    {
        public AttendanceDropdownsVM()
        {
            Groups = new List<GroupOfServices>();
            Services = new List<Service>();
            Workers = new List<Worker>();
        }

        public List<GroupOfServices> Groups { get; set; }
        public List<Service> Services { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
