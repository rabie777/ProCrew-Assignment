using Microsoft.EntityFrameworkCore;
using ProCrew_Assignment.Models;
using System.Security.Cryptography.X509Certificates;

namespace ProCrew_Assignment.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<AuditLog> AuditLogs { get; set; } 
        public DbSet<Product> Products { get; set; }
         
        
    }
}
