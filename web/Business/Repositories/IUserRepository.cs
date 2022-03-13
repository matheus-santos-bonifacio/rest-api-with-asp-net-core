using web.Business.Entities;

namespace web.Business.Repositories;

public interface IUserRepository
{

        void Add(User user);
        void Commit();
        User GetUser(string login);
}
