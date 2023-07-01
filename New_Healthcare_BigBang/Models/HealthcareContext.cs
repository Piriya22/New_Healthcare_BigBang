using Microsoft.EntityFrameworkCore;

namespace New_Healthcare_BigBang.Models
{
    public class HealthcareContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Patients> Patients { get; set; }

        public HealthcareContext(DbContextOptions<HealthcareContext> options) : base(options) { }
    }
}
