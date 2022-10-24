using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
			
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}