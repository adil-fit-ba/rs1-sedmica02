using Microsoft.EntityFrameworkCore;
using MojaApp.API.Models;

namespace MojaApp.API.Data
{
    public class MyDbContext: DbContext
    {
        public DbSet<Student> Student => Set<Student>();
        public DbSet<Opstina> Opstina => Set<Opstina>();

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
