using web.Models.Users;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace web.Configurations;

public class JwtService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserViewModelOutput userViewModelOutput)
    {

        var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
        var symmetricSecurityKey = new SymmetricSecurityKey(secret);
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Id.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString())
                    }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

        return token;
    }
}
