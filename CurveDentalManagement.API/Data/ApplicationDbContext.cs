
using CurveDentalManagement.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CurveDentalManagement.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Staff> Staffs { get; set; }
    }
}
