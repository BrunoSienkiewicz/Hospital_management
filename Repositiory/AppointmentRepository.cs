using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repositiory
{
    public class AppointmentRepository : IAppointmentRepository
	{ 
		private readonly HospitalDbContext hospitalDbContext;
		public AppointmentRepository(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}
		public async Task<Appointment> AddEntity(Appointment entity)
		{
			await hospitalDbContext.Appointments.AddAsync(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<Appointment> DeleteEntity(Appointment entity)
		{
			hospitalDbContext.Appointments.Remove(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<List<Appointment>> GetAll()
		{
			return await hospitalDbContext.Appointments.ToListAsync();
		}

		public async Task<Appointment> GetEntityById(int? id)
		{
			return await hospitalDbContext.Appointments.FirstOrDefaultAsync(e => e.AppointmentId == id);
		}

		public async Task<int> GetMaxId()
		{
			return await hospitalDbContext.Appointments.MaxAsync(e => e.AppointmentId);
		}

		public async Task<Appointment> UpdateEntity(Appointment entity)
		{
			hospitalDbContext.Appointments.Update(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}
	}
}
