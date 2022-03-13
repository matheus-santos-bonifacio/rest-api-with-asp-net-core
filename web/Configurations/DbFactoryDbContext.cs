using web.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace web.Configurations;

public class DbFactoryDbContext : IDesignTimeDbContextFactory<CourseDbContext>
{
        public CourseDbContext CreateDbContext(string[] args)
        {
                
                var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
                optionsBuilder.UseNpgsql("Host=localhost;Database=net_backend_architecture;Username=aspnet;Password=aspnet");

                CourseDbContext context = new CourseDbContext(optionsBuilder.Options);
                
                return context;
        }
    
}
