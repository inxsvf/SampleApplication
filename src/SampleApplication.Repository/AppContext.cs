using Microsoft.EntityFrameworkCore;
using SampleApplication.Repository.Models;

namespace SampleApplication.Repository
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        { }

        public static string BaseDirectory { get; set; }
        public DbSet<Person> People { get; set; }

    }
}