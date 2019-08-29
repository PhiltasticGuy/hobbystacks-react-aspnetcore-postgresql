using System.Collections.Generic;
using System.Threading.Tasks;

namespace Registrar.Api.Data
{
    public interface ICourseRepository
    {
        Task<IList<Course>> GetAllAsync();

        Task<bool> ExistsAsync(int id);
    }
}
