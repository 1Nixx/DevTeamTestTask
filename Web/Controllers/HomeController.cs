using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGenericRepository<Product> _genericRepository;
		public HomeController(IGenericRepository<Product> genericRepository)
		{
			_genericRepository = genericRepository;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}