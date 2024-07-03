using Microsoft.EntityFrameworkCore;
using Rajmohol.Models;

namespace Rajmohol.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
    }
}
