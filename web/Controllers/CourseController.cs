using web.Models.Courses;
using web.Business.Repositories;
using web.Business.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace web.Controllers;

[Route("api/v1/courses")]
[ApiController]
[Authorize]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [SwaggerResponse(statusCode: 201, description: "Course created with success")]
    [SwaggerResponse(statusCode: 401, description: "Unathorized")]
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
    {
        var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        Course course = new Course()
        {
            Id = userId,
            Name = courseViewModelInput.Name,
            Description = courseViewModelInput.Description,
            UserId = userId
        };

        _courseRepository.Add(course);
        _courseRepository.Commit();

        return Created("", courseViewModelInput);
    }

    [SwaggerResponse(statusCode: 200, description: "Got Courses with success")]
    [SwaggerResponse(statusCode: 401, description: "Unathorized")]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        var courses = _courseRepository.GetByUser(userId).Select(s => new CourseViewModelOutput()
                {
                    Name = s.Name,
                    Description = s.Description,
                    Login = s.User.Login
                });

        return Ok(courses);
    }
}
