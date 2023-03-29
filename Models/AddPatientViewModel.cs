﻿using Hospital_Management.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models
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
    }
}
