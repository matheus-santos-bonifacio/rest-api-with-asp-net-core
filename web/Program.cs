using web.Business.Repositories;
using web.Infrastructure.Data.Repositories;
using web.Infrastructure.Data;
using web.Configurations;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
                        {
                        options.SuppressModelStateInvalidFilter = true;
                        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                {
                                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                                Name = "Authorization",
                                In = ParameterLocation.Header,
                                Type = SecuritySchemeType.ApiKey,
                                Scheme = "Bearer"
                                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                {
                                        {
                                                new OpenApiSecurityScheme
                                                {
                                                        Reference = new OpenApiReference
                                                        {
                                                                Type = ReferenceType.SecurityScheme,
                                                                Id = "Bearer"
                                                        }
                                                },
                                                Array.Empty<string>()
                                        }
                                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                });

var secret = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfigurations:Secret").Value);
builder.Services.AddAuthentication(x =>
                {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                        {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken= true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                               ValidateIssuerSigningKey = true,
                               IssuerSigningKey = new SymmetricSecurityKey(secret),
                               ValidateIssuer = false,
                               ValidateAudience = false
                        };
                        });

builder.Services.AddDbContext<CourseDbContext>(options =>
                {
                        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAuthenticationService, JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
            {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api course backend");
            c.RoutePrefix = string.Empty;
            });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
