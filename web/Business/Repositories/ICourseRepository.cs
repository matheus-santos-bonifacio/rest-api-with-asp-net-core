using web.Business.Entities;

namespace web.Business.Repositories;

public interface ICourseRepository
{
    void Add(Course course);
    void Commit();
    List<Course> GetByUser(int userId);
}
