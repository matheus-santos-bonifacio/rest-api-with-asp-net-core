using web.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace web.Infrastructure.Data.Mappings;

public class CourseMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name);
        builder.Property(p => p.Description);
        builder.HasOne(p => p.User)
            .WithMany().HasForeignKey(fk => fk.UserId);
    }
}
