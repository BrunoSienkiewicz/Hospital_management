using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models.Domain;

[Index("Email", Name = "UQ__Doctors__A9D105349C68F8BC", IsUnique = true)]
public partial class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    public string Specialty { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    public int UserId { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    [InverseProperty("Doctor")]
    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();

    [ForeignKey("UserId")]
    [InverseProperty("Doctors")]
    public virtual User User { get; set; } = null!;
}
