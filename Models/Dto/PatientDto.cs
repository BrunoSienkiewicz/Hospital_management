using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models.Dto
{
	public class PatientDto
	{
		public int PatientId { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string Address { get; set; } = null!;
		public string Pesel { get; set; } = null!;
		public DateTime DateofBirth { get; set; }

	}
}
