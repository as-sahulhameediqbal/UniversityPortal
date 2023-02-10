using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityPortal.Entity;

namespace UniversityPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<University> University { get; set; }
        public DbSet<UniversityStaff> UniversityStaff { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}