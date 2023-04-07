using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.Appointment;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
	public class AppointmentsController : BaseController<Appointment, AddAppointmentViewModel, UpdateAppointmentViewModel, AppointmentDto, IAppointmentRepository>
	{
		public AppointmentsController(IAppointmentRepository appointmentRepository) : base(appointmentRepository)
		{
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		//[HttpPost]
		//public async Task<IActionResult> Login(LoginAppointmentViewModel loginAppointmentModel)
		//{
  //          var user = await hospitalDbContext.Users.Where(e => e.Username.Equals(loginAppointmentModel.Username)).FirstOrDefaultAsync();

		//	if (user == null)
		//	{
		//		return RedirectToAction("Index", "Home");
		//	}

		//	if (user.Password != loginAppointmentModel.Password)
		//	{
		//		return RedirectToAction("Index", "Home");
		//	}

		//	if (user.UserType == "doctor")
		//	{
		//		var doctor = hospitalDbContext.Doctors.Where(e => e.UserId.Equals(e.UserId)).FirstOrDefault();
		//		return await Task.Run(() => Index(doctor.DoctorId, user.UserType));
		//	}
		//	else if (user.UserType == "user")
		//	{
		//		var patient = hospitalDbContext.Patients.Where(e => e.UserId.Equals(e.UserId)).FirstOrDefault();
		//		return await Task.Run(() => Index(patient.PatientId, user.UserType));
		//	}
		//	return RedirectToAction("Index", "Home");
		//}

		//[HttpGet]
		//public async Task<IActionResult> Index (int? id, string? userType)
		//{
		//	List<Appointment>? appointments = null;
		//	if (userType == "user")
		//		appointments = await hospitalDbContext.Appointments.Where(e => e.PatientId == id).ToListAsync();
		//	else
		//		appointments = await hospitalDbContext.Appointments.Where(e => e.DoctorId == id).ToListAsync();

		//	return View(appointments);
		//}
	}
}
