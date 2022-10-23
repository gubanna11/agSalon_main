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
        Task<List<Attendance>> GetAllAttendances();
        Task AddNewAttendance(NewAttendanceVM newAttendance);
        Task<AttendanceDropdownsVM> GetAttendanceDropdownsValues(int groupId);
    }
}
