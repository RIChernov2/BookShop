using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IBooksRepository : IGenericRepository<Book, long>
    {
    }
}