using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User, long>
    {
        Task<User> GetByLoginModelAsync(LoginModel model);
    }
}