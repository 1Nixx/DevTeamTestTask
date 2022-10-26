using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Order");
			return View();
		}

		public IActionResult Error()
		{ 
			return View(); 
		}
	}
}
