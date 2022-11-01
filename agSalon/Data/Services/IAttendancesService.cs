using agSalon.Data.ViewModels;
using agSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
    public interface IAttendancesService
    {
        
        Task AddNewAttendance(NewAttendanceVM newAttendance);
        Task<AttendanceDropdownsVM> GetAttendanceDropdownsValues(int groupId);

        Task<Attendance> GetAttendanceById(int id);
        Task<IEnumerable<Attendance>> GetIsRenderedAttendances();
        Task<IEnumerable<Attendance>> GetNotRenderedAttendances();
        Task<IEnumerable<Attendance>> GetNotRenderedIsPaidAttendances();
        Task<IEnumerable<Attendance>> GetNotRenderedNotPaidAttendances();

        double GetTotal();
    }
}
