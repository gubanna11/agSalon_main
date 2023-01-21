using agSalon.Data.Base;
using agSalon.Data.ViewModels;
using agSalon.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
	public interface IServicesService : IEntityBaseRepository<Service>
	{
		Task<Service> GetServiceByIdWithGroupAsync(int id);
		List<Service> GetServicesByGroupId(int groupId);
		Task AddNewServiceAsync(ServiceVM newService);
        Task UpdateServiceAsync(ServiceVM serviceVM);
    }
}
