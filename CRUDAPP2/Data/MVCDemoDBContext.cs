using CRUDAPP2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPP2.Data
{
    public class MVCDemoDBContext : DbContext
    {
        public MVCDemoDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
