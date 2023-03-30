namespace Hospital_Management.Models.ViewModels.Appointment
{
	public class UpdateAppointmentViewModel
	{
		public int AppointmentId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string Notes { get; set; }
		public string DoctorFirstName { get; set; }
		public string DoctorLastName { get; set; }
		public string PatientFirstName { get; set; }
		public string PatientLastName { get; set; }
		public string PatientPesel { get; set;}
	}
}
