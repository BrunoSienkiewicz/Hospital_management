using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repositiory
{
    public class PatientRepository : IPatientRepository
	{
		private readonly HospitalDbContext hospitalDbContext;
		public PatientRepository(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}
		public async Task<Patient> AddEntity(Patient entity)
		{
			await hospitalDbContext.Patients.AddAsync(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<Patient> DeleteEntity(Patient entity)
		{
			hospitalDbContext.Patients.Remove(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<List<Patient>> GetAll()
		{
			return await hospitalDbContext.Patients.ToListAsync();
		}

		public async Task<Patient> GetEntityById(int? id)
		{
			return await hospitalDbContext.Patients.FirstOrDefaultAsync(e => e.PatientId == id);
		}

		public async Task<int> GetMaxId()
		{
			return await hospitalDbContext.Patients.MaxAsync(e => e.PatientId);
		}

		public async Task<Patient> UpdateEntity(Patient entity)
		{
			hospitalDbContext.Patients.Update(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}
	}
}
