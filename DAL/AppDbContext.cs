using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using DAL.Configurations;
using System.Reflection.Emit;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        public AppDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MyEntityConfiguration());

            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            var adminId = Guid.NewGuid();

            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var adminUser =
                new AppUser
                {
                    Id = adminId,
                    Email = "SuperUser@mail.ru",
                    UserName = "SuperUser",
                    NormalizedUserName = "SUPERUSER",  // Устанавливаем нормализованное имя
                    NormalizedEmail = "SUPERUSER@MAIL.RU" // Устанавливаем нормализованный email
                };
            
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin");

            builder.Entity<AppUser>().HasData(adminUser);

            // Связь пользователя с ролью
            var adminUserRole = new IdentityUserRole<Guid>
            {
                UserId = adminId,
                RoleId = adminRoleId
            };
            builder.Entity<IdentityUserRole<Guid>>().HasData(adminUserRole);
        }
    }
}
