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
		public AppointmentsController(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository) : base(appointmentRepository)
		{
			this.doctorRepository = doctorRepository;
			this.patientRepository = patientRepository;
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
	}
}
