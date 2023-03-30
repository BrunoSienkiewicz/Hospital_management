namespace Hospital_Management.Models.ViewModels.Perscription
{
	public class UpdatePerscriptionViewModel
	{
		public int PerscriptionId { get; set; }
		public int AppointmentId { get; set; }
		public string Medication { get; set; } = null!;
		public string Dosage { get; set; } = null!;
		public string Frequency { get; set; } = null!;
	}
}
