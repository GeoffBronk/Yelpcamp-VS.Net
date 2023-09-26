using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yelpcamp.Areas.Identity.Data;
using Yelpcamp.Models;

namespace Yelpcamp.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Campground> Campgrounds { get; set; } = null!;
    public DbSet<CampgroundReview> CampgroundReviews { get; set; } = null!;
    public DbSet<CampgroundImage> CampgroundImages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
    }

    private class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(256);
            builder.Property(u => u.FirstName).IsRequired(false);

            builder.Property(u => u.LastName).HasMaxLength(256);
            builder.Property(u => u.LastName).IsRequired(false);
        }
    }
}
