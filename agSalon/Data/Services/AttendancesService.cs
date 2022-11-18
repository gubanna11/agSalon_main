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


        public async Task AddNewAttendance(NewAttendanceVM newAttendance)
        {
            Attendance attendance = new Attendance()
            {
                GroupId = newAttendance.GroupId,
                ServiceId = newAttendance.ServiceId,
                WorkerId = newAttendance.WorkerId,
                Date = newAttendance.Date,
                Price = _context.Services.Where(s => s.Id == newAttendance.ServiceId).Select(s => s.Price).FirstOrDefault(),
                IsRendered = YesNoEnum.No,
                IsPaid = YesNoEnum.No
            };

            //!!!!!!!!!!!!!
            attendance.ClientId = ""; //!!!!!!
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
                //Workers = await _context.Workers_Groups.Include(wg => wg.Worker).Where(wg => wg.GroupId == groupId).Select(wg => wg.Worker).ToListAsync()
            };

            return dropdowns;
        }

        //GET ATTENDANCES

        public async Task<Attendance> GetAttendanceById(int id)
            => await _context.Attendances.Where(a => a.Id == id)
            //.Include(a => a.Client)
            .Include(a => a.Group)
                //.Include(a => a.Service).Include(a => a.Worker)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Attendance>> GetNotRenderedAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No)
                //.Include(a => a.Client)
                .Include(a => a.Group)
                //.Include(a => a.Service).Include(a => a.Worker)
                .ToListAsync();
            return attendances;
        }

        public async Task<IEnumerable<Attendance>> GetNotRenderedIsPaidAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.Yes).
                //.Include(a => a.Client).
                Include(a => a.Group)
                //.Include(a => a.Service).Include(a => a.Worker)
                .ToListAsync();
            return attendances;
        }


        public async Task<IEnumerable<Attendance>> GetNotRenderedNotPaidAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.No)
                //.Include(a => a.Client)
                .Include(a => a.Group)
//                .Include(a => a.Service).Include(a => a.Worker)
                .ToListAsync();
            return attendances;
        }

     

        public async Task<IEnumerable<Attendance>> GetIsRenderedAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.Yes)
                //.Include(a => a.Client)
                .Include(a => a.Group)
                //.Include(a => a.Service).Include(a => a.Worker)
                .ToListAsync();
            return attendances;
        }


        public double GetTotal()
        {
            var total =  _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.No)
                .Include(a => a.Service).Sum(p => p.Service.Price);
            return total;
        }

    }
}
