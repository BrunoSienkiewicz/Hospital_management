using Hospital_Management.Data;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
	public class DoctorsController : BaseController<Doctor, AddDoctorViewModel, UpdateDoctorViewModel>
	{
		public DoctorsController(HospitalDbContext hospitalDbContext) : base(hospitalDbContext)
		{
		}
	}
}
