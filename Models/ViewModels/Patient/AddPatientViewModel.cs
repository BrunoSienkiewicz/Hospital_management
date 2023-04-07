using Hospital_Management.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models.ViewModels.Patient
{
    public class AddPatientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Pesel { get; set; }
        public DateTime DateofBirth { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserType { get; set; }
    }
}
