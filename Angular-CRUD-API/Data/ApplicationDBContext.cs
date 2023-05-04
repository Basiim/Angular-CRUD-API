using Angular_CRUD_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Angular_CRUD_API.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
