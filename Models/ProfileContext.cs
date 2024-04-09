using Microsoft.EntityFrameworkCore;

namespace TamerProject.Models;

public class ProfileContext: DbContext
{
    public ProfileContext(DbContextOptions<ProfileContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; } = null!;
}
