namespace Hospital_Management.Models.Dto
{
	public class PrescriptionDto
	{
		public int PerscriptionId { get; set; }
		public string DoctorId { get; set; }
		public string PatientId { get; set; }
		public string DoctorName { get; set; }
		public string PatientName { get; set; }
		public string Medication { get; set; }
		public string Dosage { get; set; }
		public string Frequency { get; set; }
	}
}
