using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models.Dto;
using Hospital_Management.Models.ViewModels.Appointment;
using Hospital_Management.Models.ViewModels.Patient;
using Hospital_Management.Repositiory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
	public class AppointmentsController : BaseController<Appointment, AddAppointmentViewModel, UpdateAppointmentViewModel, AppointmentDto, IAppointmentRepository>
	{
		private readonly IDoctorRepository doctorRepository;
		private readonly IPatientRepository patientRepository;
		private readonly IUserRepository userRepository;
		public AppointmentsController(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IUserRepository userRepository) : base(appointmentRepository)
		{
			this.doctorRepository = doctorRepository;
			this.patientRepository = patientRepository;
			this.userRepository = userRepository;
		}

		protected async override Task<AppointmentDto> mapToEntityDto(Appointment entity)
		{
			var doctor = await doctorRepository.GetEntityById(entity.DoctorId);
			var patient = await patientRepository.GetEntityById(entity.PatientId);

			var dto = new AppointmentDto
			{
				AppointmentId = entity.AppointmentId,
				DoctorId = entity.DoctorId,
				PatientId = entity.PatientId,
				DoctorName = doctor.FirstName + " " + doctor.LastName,
				PatientName = patient.FirstName + " " + patient.LastName,
				Date = entity.AppointmentDate.ToString(),
				Notes = entity.Notes
			};

			return dto;
		}

		[HttpGet]
		public async Task<IActionResult> GetByUser()
		{
			var username = Request.Cookies["username"];
			var user = await userRepository.GetByUsername(username);
			var appointments = await entityRepository.GetByUserId(user.UserId);
            var appointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointments)
			{
                var appointmentDto = await mapToEntityDto(appointment);
                appointmentDtos.Add(appointmentDto);
            }
			return View("Index", appointmentDtos);
		}
	}
}
