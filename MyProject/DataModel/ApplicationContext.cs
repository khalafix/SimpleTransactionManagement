using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Reflection.Emit;
using MyProject.ViewModels;

namespace MyProject.DataModel
{
    public class ApplicationContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuring foreign key relationship for UserId with no cascading delete
            builder.Entity<Transaction>()
                .HasOne(dh => dh.User)
                .WithMany()
                .HasForeignKey(dh => dh.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete

            // Configuring foreign key relationship for CustomerId with no cascading delete
            builder.Entity<Transaction>()
                .HasOne(dh => dh.Customer)
                .WithMany()
                .HasForeignKey(dh => dh.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete

            builder.Entity<User>(b =>
            {

                b.Property(p => p.UserName).IsRequired();
                b.ToTable("Users");
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("Roles");
            });
            builder.Entity<UserRole>(b =>
            {
                b.HasOne(userRole => userRole.Role)
                 .WithMany(role => role.Users)
                 .HasForeignKey(userRole => userRole.RoleId);

                b.HasOne(userRole => userRole.User)
                 .WithMany(user => user.Roles)
                 .HasForeignKey(userRole => userRole.UserId);

                b.ToTable("UserRoles");
            });
            builder.Entity<RoleClaim>(b =>
            {
                b.HasOne(r => r.Role)
                 .WithMany(r => r.Claims)
                 .HasForeignKey(r => r.RoleId);
                b.ToTable("RoleClaims");
            });
            builder.Entity<UserLogin>(b =>
            {
                b.HasOne(x => x.User)
                 .WithMany(u => u.Logins)
                 .HasForeignKey(u => u.UserId);
                b.ToTable("UserLogins");
            });
            builder.Entity<UserClaim>(b =>
            {
                b.HasOne(x => x.User)
                 .WithMany(u => u.Claims)
                 .HasForeignKey(u => u.UserId);
                b.ToTable("UserClaims");
            });
            builder.Entity<UserToken>(b =>
            {
                b.HasOne(x => x.User)
                 .WithMany(u => u.UserTokens)
                 .HasForeignKey(u => u.UserId);
                b.ToTable("UserTokens");
            });

            
        }


        
    }
}
