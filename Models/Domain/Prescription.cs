using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Models.Domain;

public partial class Prescription
{
    [Key]
    public int PrescriptionId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public int AppointmentId { get; set; }

    [StringLength(100)]
    public string Medication { get; set; } = null!;

    [StringLength(20)]
    public string Dosage { get; set; } = null!;

    [StringLength(20)]
    public string Frequency { get; set; } = null!;

    [ForeignKey("AppointmentId")]
    [InverseProperty("Prescriptions")]
    public virtual Appointment Appointment { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("Prescriptions")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Prescriptions")]
    public virtual Patient Patient { get; set; } = null!;
}
