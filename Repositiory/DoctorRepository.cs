using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repositiory
{
    public class DoctorRepository : IDoctorRepository
	{
		private readonly HospitalDbContext hospitalDbContext;
		public DoctorRepository(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}
		public async Task<Doctor> AddEntity(Doctor entity)
		{
			await hospitalDbContext.Doctors.AddAsync(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<Doctor> DeleteEntity(Doctor entity)
		{
			hospitalDbContext.Doctors.Remove(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<List<Doctor>> GetAll()
		{
			return await hospitalDbContext.Doctors.ToListAsync();
		}

		public async Task<Doctor> GetEntityById(int? id)
		{
			return await hospitalDbContext.Doctors.FirstOrDefaultAsync(e => e.DoctorId == id);
		}

		public async Task<int> GetMaxId()
		{
			return await hospitalDbContext.Doctors.MaxAsync(e => e.DoctorId);
		}

		public async Task<Doctor> UpdateEntity(Doctor entity)
		{
			hospitalDbContext.Doctors.Update(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}
	}
}
