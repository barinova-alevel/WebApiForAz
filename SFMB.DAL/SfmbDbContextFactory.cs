using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SFMB.DAL
{
    public class SfmbDbContextFactory : IDesignTimeDbContextFactory<SfmbDbContext>
    {
        public SfmbDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SfmbDbContext>();
            var connectionString = configuration.GetConnectionString("WebApiDefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new SfmbDbContext(optionsBuilder.Options, configuration);
        }
    }
}
