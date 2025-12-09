using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SFMB.DAL.Entities;

namespace SFMB.DAL
{
    public class SfmbDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public SfmbDbContext(DbContextOptions<SfmbDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseNpgsql(connectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<OperationType>()
                .HasMany(ot => ot.Operations)
                .WithOne(o => o.OperationType)
                .HasForeignKey(o => o.OperationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User relationships
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Operations)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OperationTypes)
                .WithOne(ot => ot.User)
                .HasForeignKey(ot => ot.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //uncomment for the init migration:
            //var operationTypes = LoadSeedData<OperationType>("operationTypes.json");

            //if (operationTypes != null)
            //{
            //    modelBuilder.Entity<OperationType>().HasData(operationTypes);
            //}

            //var operations = LoadSeedData<Operation>("operations.json");

            //if (operations != null)
            //{
            //    modelBuilder.Entity<Operation>().HasData(operations);
            //}
        }

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
        //}
    }
}
