using agSalon.Data.Base;
using agSalon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using agSalon.Data.ViewModels;

namespace agSalon.Data.Services
{
	public class ServicesService : EntityBaseRepository<Service>, IServicesService
	{
		private readonly AppDbContext _context;
		public ServicesService(AppDbContext context) : base(context)
		{
			_context = context;
		}

        public List<Service> GetServicesByGroupId(int groupId)
			=> _context.Services.Include(s => s.Service_Group).Where(s => s.Service_Group.GroupId == groupId).ToList();


        public async Task AddNewServiceAsync(ServiceVM newService)
        {
            Service service = new Service
            {
                Name = newService.Name,
                Price = newService.Price
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            Service_Group serviceGroup = new Service_Group
            {
                ServiceId = service.Id,
                GroupId = newService.GroupId
            };

            await _context.Services_Groups.AddAsync(serviceGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServiceAsync(ServiceVM serviceVM)
        {
            var service = await GetByIdAsync(serviceVM.Id);

            if (service != null)
            {
                service.Name = serviceVM.Name;
                service.Price = serviceVM.Price;
            }

            var service_group = _context.Services_Groups.Where(sg => sg.ServiceId == service.Id).FirstOrDefault();
            _context.Services_Groups.Remove(service_group);

            Service_Group newService_Group = new Service_Group()
            {
                ServiceId = service.Id,
                GroupId = serviceVM.GroupId
            };

            await _context.Services_Groups.AddAsync(newService_Group);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> GetServiceByIdWithGroupAsync(int id) => await _context.Services
            .Where(s => s.Id == id)
            .Include(s => s.Service_Group).ThenInclude(sg => sg.Group).FirstOrDefaultAsync();
    }
}
