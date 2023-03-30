namespace Hospital_Management.Models.ViewModels.Perscription
{
    public class AddPerscriptionViewModel
    {
        public int AppointmentId { get; set; }
        public string Medication { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public string Frequency { get; set; } = null!;
    }
}
