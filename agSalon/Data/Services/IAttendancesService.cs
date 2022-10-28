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
        
        Task<List<Attendance>> GetIsRenderedAttendances();
        Task<List<Attendance>> GetNotRenderedAttendances();
        Task<List<Attendance>> GetNotRenderedIsPaidAttendances();
        Task<List<Attendance>> GetNotRenderedNotPaidAttendances();

        double GetTotal();
    }
}
