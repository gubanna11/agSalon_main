using agSalon.Data.Base;
using agSalon.Models;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
	public interface IGroupsService : IEntityBaseRepository<GroupOfServices>
	{
		Task AddNewGroupAsync(GroupOfServices newGroup);
		Task UpdateGroupAsync(GroupOfServices group);
	}
}
