using Microsoft.EntityFrameworkCore;
using Rajmohol.Models;

namespace Rajmohol.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet <VillaNumber> VillaNumbers{ get; set; }
    }
}
