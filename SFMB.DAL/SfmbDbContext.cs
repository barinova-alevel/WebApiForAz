using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SFMB.DAL.Entities;

namespace SFMB.DAL
{
    public class SfmbDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SfmbDbContext(DbContextOptions<SfmbDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("WebApiDefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OperationType>()
                .HasMany(ot => ot.Operations)
                .WithOne(o => o.OperationType)
                .HasForeignKey(o => o.OperationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            //uncomment for the init migration:
        //    var operationTypes = LoadSeedData<OperationType>("operationTypes.json");

        //    if (operationTypes != null)
        //    {
        //        modelBuilder.Entity<OperationType>().HasData(operationTypes);
        //    }

        //    var operations = LoadSeedData<Operation>("operations.json");

        //    if (operations != null)
        //    {
        //        modelBuilder.Entity<Operation>().HasData(operations);
        //    }
        //}

        //private List<T> LoadSeedData<T>(string filePath)
        //{
        //    try
        //    {
        //        var jsonData = File.ReadAllText(filePath);
        //        return JsonSerializer.Deserialize<List<T>>(jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Failed to load seed data: {ex.Message}");
        //        return null;
        //    }
        }
    }
}
