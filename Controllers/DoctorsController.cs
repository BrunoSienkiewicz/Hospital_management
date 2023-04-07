using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.Doctor;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
	public class DoctorsController : BaseController<Doctor, AddDoctorViewModel, UpdateDoctorViewModel, DoctorDto, IDoctorRepository>
	{
		public DoctorsController(IDoctorRepository doctorRepository) : base(doctorRepository)
		{
		}
	}
}
