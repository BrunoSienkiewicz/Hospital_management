using Hospital_Management.Data;
using Hospital_Management.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class LoginController : Controller
	{
		private readonly HospitalDbContext hospitalDbContext;

		public LoginController(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Logout()
		{
			return View();
		}

		public async Task<IActionResult> LoginAsync(LoginUserViewModel loginUserRequest)
		{
			var userId = await hospitalDbContext.Users.Where(u => u.Username == loginUserRequest.Username && u.Password == loginUserRequest.Password).FirstOrDefaultAsync();
			var user = await hospitalDbContext.Users.FindAsync(userId);

			if (user != null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}
	}
}
