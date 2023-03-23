namespace Hospital_Management.Models
{
    public class AddPatientViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string PESEL { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int userId { get; set; }
    }
}
