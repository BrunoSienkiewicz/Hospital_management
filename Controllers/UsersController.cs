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
		public UsersController(IUserRepository userRepository) : base(userRepository)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
