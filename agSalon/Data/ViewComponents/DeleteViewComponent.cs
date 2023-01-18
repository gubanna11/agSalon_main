using Microsoft.AspNetCore.Mvc;

namespace agSalon.Data.ViewComponents
{
	public class DeleteViewComponent : ViewComponent
	{
		public DeleteViewComponent()
		{

		}

		public IViewComponentResult Invoke(int id)
		{
			return View(id);
		}
	}
}
