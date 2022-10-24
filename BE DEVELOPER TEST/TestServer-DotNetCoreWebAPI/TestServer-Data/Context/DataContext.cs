using TestServer_Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestServer_Data.Context
{
    public class DataContext:DbContext
    {
        public DbSet<UserInput> UserInput { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
