using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models.Domain;

[Index("Username", Name = "UQ__Users__536C85E45C538ACC", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [StringLength(20)]
    public string UserType { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();

    [InverseProperty("User")]
    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}
