using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repositiory
{
    public class PrescriptionRepository : IPrescriptionRepository
	{
		private readonly HospitalDbContext hospitalDbContext;
		public PrescriptionRepository(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}
		public async Task<Prescription> AddEntity(Prescription entity)
		{
			await hospitalDbContext.Prescriptions.AddAsync(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<Prescription> DeleteEntity(Prescription entity)
		{
			hospitalDbContext.Prescriptions.Remove(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<List<Prescription>> GetAll()
		{
			return await hospitalDbContext.Prescriptions.ToListAsync();
		}

		public async Task<Prescription> GetEntityById(int? id)
		{
			return await hospitalDbContext.Prescriptions.FirstOrDefaultAsync(e => e.PrescriptionId == id);
		}

		public async Task<int> GetMaxId()
		{
			return await hospitalDbContext.Prescriptions.MaxAsync(e => e.PrescriptionId);
		}

		public async Task<Prescription> UpdateEntity(Prescription entity)
		{
			hospitalDbContext.Prescriptions.Update(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}
	}
}
