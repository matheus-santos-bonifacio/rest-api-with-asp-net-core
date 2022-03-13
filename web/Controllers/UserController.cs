using web.Models.Users;
using web.Models;
using web.Filters;
using web.Business.Entities;
using web.Business.Repositories;
using web.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace web.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public UserController(
            IUserRepository userRepository,
            IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    [SwaggerResponse(statusCode: 200, description: "Success in authentication", Type = typeof(SignInViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "The fields is required", Type = typeof(ValidateFieldViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(GenericErrorViewModel))]
    [HttpPost]
    [Route("signIn")]
    [CustomizedModelStateValidation]
    public IActionResult SignIn(SignInViewModelInput signInViewModelInput)
    {
        User user = _userRepository.GetUser(signInViewModelInput.Login);

        if (user == null)
            return BadRequest("We had a problem when tried to access the content");

        var userViewModelOutput = new UserViewModelOutput()
        {
            Id = user.Id,
            Login = signInViewModelInput.Login,
            Email = user.Email
        };

        var token = _authenticationService.GenerateToken(userViewModelOutput);

        return Ok(new
                {
                    Token = token,
                    User = userViewModelOutput
                });
    }

    [SwaggerResponse(statusCode: 200, description: "Success in authentication", Type = typeof(SignInViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "The fields is required", Type = typeof(ValidateFieldViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(GenericErrorViewModel))]
    [HttpPost]
    [Route("register")]
    [CustomizedModelStateValidation]
    public IActionResult SignUp(SignUpViewModelInput signUpViewModelInput)
    {
        var user = new User();
        user.Login = signUpViewModelInput.Login;
        user.Password = signUpViewModelInput.Password;
        user.Email = signUpViewModelInput.Email;

        _userRepository.Add(user);
        _userRepository.Commit();

        return Created("", signUpViewModelInput);
    }
}
