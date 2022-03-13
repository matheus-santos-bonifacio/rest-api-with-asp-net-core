using web.Business.Entities;
using web.Business.Repositories;

namespace web.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
        private readonly CourseDbContext _context;

        public UserRepository(CourseDbContext context)
        {
                _context = context;
        }

        public void Add(User user)
        {
                _context.User.Add(user);
        }

        public User GetUser(string login)
        {
                return _context.User.FirstOrDefault(u => u.Login == login);
        }

        public void Commit()
        {
                _context.SaveChanges();
        }
}
