using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repositiory
{
    public class UserRepository : IUserRepository
	{
		private readonly HospitalDbContext hospitalDbContext;
		public UserRepository(HospitalDbContext hospitalDbContext)
		{
			this.hospitalDbContext = hospitalDbContext;
		}
		public async Task<User> AddEntity(User entity)
		{
			await hospitalDbContext.Users.AddAsync(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<User> DeleteEntity(User entity)
		{
			hospitalDbContext.Users.Remove(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<List<User>> GetAll()
		{
			return await hospitalDbContext.Users.ToListAsync();
		}

		public async Task<User> GetEntityById(int? id)
		{
			return await hospitalDbContext.Users.FirstOrDefaultAsync(e => e.UserId == id);
		}

		public async Task<int> GetMaxId()
		{
			return await hospitalDbContext.Users.MaxAsync(e => e.UserId);
		}

		public async Task<User> UpdateEntity(User entity)
		{
			hospitalDbContext.Users.Update(entity);
			await hospitalDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<User> GetByUsername(string username)
		{
            return await hospitalDbContext.Users.FirstOrDefaultAsync(e => e.Username == username);
        }
	}
}
