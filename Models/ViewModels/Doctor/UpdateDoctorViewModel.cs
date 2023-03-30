namespace Hospital_Management.Models.ViewModels.Doctor
{
	public class UpdateDoctorViewModel
	{
		public int DoctorId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Speciality { get; set; }
		public string PhoneNumber { get; set; }
	}
}
