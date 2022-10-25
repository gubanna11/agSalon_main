using agSalon.Data.Enums;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
    public class AttendancesService : IAttendancesService
    {
        private readonly AppDbContext _context;
        public AttendancesService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Attendance>> GetAllAttendances()
        {
            var attendances = await _context.Attendances.Include(a => a.Client).Include(a => a.Group)
                .Include(a => a.Service).Include(a => a.Worker).ToListAsync();
            return attendances;
        }

        public async Task AddNewAttendance(NewAttendanceVM newAttendance)
        {
            Attendance attendance = new Attendance()
            {
                GroupId = newAttendance.GroupId,
                ServiceId = newAttendance.ServiceId,
                WorkerId = newAttendance.WorkerId,
                Date = newAttendance.Date,
                Price = _context.Services.Where(s => s.Id == newAttendance.ServiceId).Select(s => s.Price).FirstOrDefault(),
                IsRendered = IsRendered.No
            };

            //!!!!!!!!!!!!!
            attendance.ClientId = 1; //!!!!!!
            //!!!!!!!!!!!!!

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }


        public async Task<AttendanceDropdownsVM> GetAttendanceDropdownsValues(int groupId)
        {
            var dropdowns = new AttendanceDropdownsVM()
            {
                Groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync(),
                Services = await _context.Services.Include(s => s.Service_Group).Where(s => s.Service_Group.GroupId == groupId).OrderBy(s => s.Name).ToListAsync(),
                Workers = await _context.Workers_Groups.Include(wg => wg.Worker).Where(wg => wg.GroupId == groupId).Select(wg => wg.Worker).ToListAsync()
            };

            return dropdowns;
        }

        
    }
}
