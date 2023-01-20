using AdminPresentationLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPresentationLayer.Data
{
    public class ApplicationDbContext : DbContext 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
