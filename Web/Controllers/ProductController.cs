using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[Authorize("Admin")]
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
