using web.Models.Users;

namespace web.Configurations;

public interface IAuthenticationService
{
        string GenerateToken(UserViewModelOutput userViewModelOutput);
}
