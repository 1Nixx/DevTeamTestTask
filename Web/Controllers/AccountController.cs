using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.ViewModels.Account;

namespace Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[InvalidModelFilter]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			var user = new IdentityUser()
			{
				Email = model.Email,
				UserName = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, model.UserRole.ToString());
				await _signInManager.SignInAsync(user, false);
				return RedirectToAction("Index", "Order");
			}
			else
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[InvalidModelFilter]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

			if (result.Succeeded)
				return RedirectToAction("Index", "Order");
			else
				ModelState.AddModelError("", "Неправильный логин и (или) пароль ");

			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		[HttpGet("iAdmin")]
		public ActionResult<bool> IsAdmin()
		{
			return Json(User.IsInRole("Admin"));
		}
	}
}
