using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.User;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hospital_Management.Controllers
{
	public class UsersController : BaseController<User, AddUserViewModel, UpdateUserViewModel, UserDto, IUserRepository>
	{
		private readonly UserManager userManager;
		public UsersController(IUserRepository userRepository, IConfiguration configuration) : base(userRepository)
		{
			userManager = new UserManager(userRepository, configuration);
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
			Response.Cookies.Append("username", user.Username, cookieOptions);
			Response.Cookies.Append("userType", user.UserType, cookieOptions);

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		public async Task<IActionResult> Logout()
		{ 
			Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("userType");

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		public async Task<IActionResult> ValidateToken()
		{
            var token = Request.Cookies["jwt"];

            if (token == null)
			{
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

			if (decodedToken.ValidTo < DateTime.UtcNow)
			{
				return Unauthorized();
			}

            return Ok();
        }
	}
}
