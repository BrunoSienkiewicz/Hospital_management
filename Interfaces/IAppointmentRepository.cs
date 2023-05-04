using Hospital_Management.Models.Domain;

namespace Hospital_Management.Interfaces
{
	public interface IAppointmentRepository : IRepository<Appointment>
	{
		public Task<List<Appointment>> GetByUserId (int userId);
	}
}
