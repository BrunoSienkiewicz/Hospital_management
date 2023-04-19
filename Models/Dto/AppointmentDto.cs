namespace Hospital_Management.Models.Dto
{
	public class AppointmentDto
	{
		public int AppointmentId { get; set; }
		public int DoctorId { get; set; }
		public int PatientId { get; set; }
		public string DoctorName { get; set; }
		public string PatientName { get; set; }
		public string Date { get; set; }
		public string Notes { get; set; }
	}
}
