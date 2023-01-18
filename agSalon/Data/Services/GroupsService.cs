using agSalon.Data.Base;
using agSalon.Models;

namespace agSalon.Data.Services
{
	public class GroupsService : EntityBaseRepository<GroupOfServices>, IGroupsService
	{
		public GroupsService(AppDbContext context) : base(context)
		{
		}
	}
}
