using Hospital_Management.Models.Domain;

namespace Hospital_Management.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		public Task<User> GetByUsername(string username);
	}
}
