using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.User;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
	public class UsersController : BaseController<User, AddUserViewModel, UpdateUserViewModel, UserDto, IUserRepository>
	{
		private readonly UserManager userManager;
		public UsersController(IUserRepository userRepository, IConfiguration configuration) : base(userRepository)
		{
			userManager = new UserManager(userRepository, configuration);
        }

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult LoginPage()
		{
			return View();
		}

		public IActionResult RegisterPage()
		{
			return View();
		}

		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var user = await userManager.FindByName(model.Username);

			if (user == null || !await userManager.CheckPassword(user, model.Password))
			{
				ModelState.AddModelError("", "Invalid username or password.");
				return View(model);
			}

			var token = await userManager.CreateToken(user);

			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict
			};

			Response.Cookies.Append("jwt", token, cookieOptions);

			var userType = user.UserType;

			TempData["UserType"] = userType;

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		public async Task<IActionResult> Logout()
		{ 
			Response.Cookies.Delete("jwt");

            TempData["UserType"] = null;

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}
	}
}
