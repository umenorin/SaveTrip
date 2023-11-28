using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using SaveTrip.Models;

namespace SaveTrip
{
    public class IDesignTimeDbContextFactory
    {
        public class BloggingContextFactory : IDesignTimeDbContextFactory<STDbContext>
        {
            public STDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<STDbContext>();
                optionsBuilder.UseSqlite("Data Source=blog.db");

                return new STDbContext(optionsBuilder.Options);
            }
        }

    }
}
