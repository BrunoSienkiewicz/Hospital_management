using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models.Domain;

[Index("Pesel", Name = "UQ__Patients__48A5F71740EF8282", IsUnique = true)]
[Index("PhoneNumber", Name = "UQ__Patients__85FB4E38E1BB983E", IsUnique = true)]
[Index("Email", Name = "UQ__Patients__A9D105340D346A68", IsUnique = true)]
public partial class Patient
{
    [Key]
    public int PatientId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [StringLength(11)]
    public string Pesel { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateofBirth { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    [InverseProperty("Patient")]
    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();

    [ForeignKey("UserId")]
    [InverseProperty("Patients")]
    public virtual User User { get; set; } = null!;
}
