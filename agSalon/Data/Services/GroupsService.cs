using agSalon.Data.Base;
using agSalon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

namespace agSalon.Data.Services
{
	public class GroupsService : EntityBaseRepository<GroupOfServices>, IGroupsService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		public GroupsService(AppDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task AddNewGroupAsync(GroupOfServices newGroup)
		{
			newGroup.ImgUrl = UploadFIle(newGroup.Img);
			await AddAsync(newGroup);
		}

		public async Task UpdateGroupAsync(GroupOfServices group)
		{
			if (group.Img != null)
				group.ImgUrl = UploadFIle(group.Img);

			await UpdateAsync(group);
		}

		private string UploadFIle(IFormFile file)
		{
			string uniqueFileName = null;

			if (file != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/groups");
				if (File.Exists(uploadsFolder + "/" + file.FileName))
					return file.FileName;

				uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
			}

			return uniqueFileName;
		}
	}
}
