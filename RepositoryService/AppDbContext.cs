using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryService.Extensions;

namespace RepositoryService
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string postgreConnectionString = configuration.GetConnectionString("PostgreConnectionString");

            optionsBuilder.UseNpgsql(postgreConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddRoles();
        }
    }
}
