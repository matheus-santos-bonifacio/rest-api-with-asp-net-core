using web.Business.Repositories;
using web.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace web.Infrastructure.Data.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _context;

    public CourseRepository(CourseDbContext context)
    {
        _context = context;
    }
    
    public void Add(Course course)
    {
        _context.Course.Add(course);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
    
    public List<Course> GetByUser(int userId)
    {
        return _context.Course.Include(i => i.User).Where(w => w.UserId == userId).ToList();
    }
}
