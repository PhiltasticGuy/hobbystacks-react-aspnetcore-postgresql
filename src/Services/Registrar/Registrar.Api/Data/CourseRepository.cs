using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registrar.Api.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly RegistrarContext _context;

        public CourseRepository(RegistrarContext context)
        {
            _context = context;
        }

        public async Task<IList<Course>> GetAllAsync()
        {
            return await
                _context.Courses
                    .Include(course => course.Instructor)
                    .Include(course => course.Subject)
                    .OrderBy(c => c.ReferenceCode)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await
                _context.Courses
                    .AnyAsync(e => e.CourseId == id);
        }
    }
}
