using agSalon.Data.Base;
using agSalon.Data.Enums;
using agSalon.Data.Static;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
    public class AttendancesService : EntityBaseRepository<Attendance>, IAttendancesService
    {
        private readonly AppDbContext _context;
        public AttendancesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewAttendance(NewAttendanceVM newAttendance, string userId)
        {
            Attendance attendance = new Attendance()
            {
                GroupId = newAttendance.GroupId,
                ServiceId = newAttendance.ServiceId,
                WorkerId = newAttendance.WorkerId,
                Date = newAttendance.Date,
                Price = _context.Services.Where(s => s.Id == newAttendance.ServiceId).Select(s => s.Price).FirstOrDefault(),
                IsRendered = YesNoEnum.No,
                IsPaid = YesNoEnum.No,
                ClientId = userId
            };


            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }


        public async Task<AttendanceDropdownsVM> GetAttendanceDropdownsValues(int groupId)
        {
            var dropdowns = new AttendanceDropdownsVM()
            {
                Groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync(),
                Services = await _context.Services.Include(s => s.Service_Group).Where(s => s.Service_Group.GroupId == groupId).OrderBy(s => s.Name).ToListAsync(),
                //Workers = await _context.Workers_Groups.Include(wg => wg.Worker).Where(wg => wg.GroupId == groupId).Select(wg => wg.Worker).Include(w => w.Client).ToListAsync()
                Workers = await _context.Workers_Groups.Include(wg => wg.Worker).ThenInclude(w => w.Client).Where(wg => wg.GroupId == groupId).Select(wg => wg.Worker).ToListAsync()
            };

            return dropdowns;
        }

        //GET ATTENDANCES

        public async Task<Attendance> GetAttendanceById(int id)
            => await _context.Attendances.Where(a => a.Id == id)
            .Include(a => a.Client).Include(a => a.Group)
                .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Attendance>> GetNotRenderedAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker)
                        .ToListAsync();
            return attendances;
        }

        public async Task<IEnumerable<Attendance>> GetNotRenderedIsPaidAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.Yes)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker)
                        .ToListAsync();
            return attendances;
        }

        public async Task<IEnumerable<Attendance>> GetNotRenderedIsPaidAttendances(string userId)
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.Yes && a.ClientId == userId)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                        .ToListAsync();
            return attendances;
        }


        public async Task<IEnumerable<Attendance>> GetNotRenderedNotPaidAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.No)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                        .ToListAsync();
            return attendances;
        }

        public async Task<IEnumerable<Attendance>> GetNotRenderedNotPaidAttendances(string userId)
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.No && a.ClientId == userId)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                        .ToListAsync();
            return attendances;
        }



        public async Task<IEnumerable<Attendance>> GetIsRenderedAttendances()
        {
            var attendances = await _context.Attendances.Where(a => a.IsRendered == YesNoEnum.Yes)
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                        .ToListAsync();
            return attendances;
        }


        public double GetTotal(string userId)
        {
            var total = _context.Attendances.Where(a => a.IsRendered == YesNoEnum.No && a.IsPaid == YesNoEnum.No && a.ClientId == userId)
                    .Include(a => a.Service).Sum(p => p.Service.Price);
            return total;
        }




        public async Task<IEnumerable<Attendance>> GeAllAttendances(string userId, string role)
        {
            var attendances = await _context.Attendances
                .Include(a => a.Client).Include(a => a.Group)
                    .Include(a => a.Service).Include(a => a.Worker).ThenInclude(w => w.Client)
                        .ToListAsync();

            if (role == UserRoles.Worker)
                attendances = attendances.Where(n => n.WorkerId == userId).ToList();

            return attendances;
        }

        public async Task CompletePaymentAll(string userId)
        {
            var attendances = await GetNotRenderedNotPaidAttendances(userId);

            foreach (var item in attendances)
                item.IsPaid = YesNoEnum.Yes;

            await _context.SaveChangesAsync();
        }
    }
}
