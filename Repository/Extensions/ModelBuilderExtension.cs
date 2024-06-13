using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void AddRoles(this ModelBuilder modelBuilder)
        {
            var models = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                }
            };

            models.ForEach(i => modelBuilder.Entity<Role>().HasData(i));
        }
    }
}
