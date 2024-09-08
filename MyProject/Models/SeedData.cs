using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.DataModel;

namespace MyProject.Models
{
    public static class SeedData
    {
        /// <summary>
        /// در این بخش یک سری داده اولیه برای راه اندازی سیستم در دیتابیس درج میشود.
        /// نقشهای کاربری
        /// کاربر مدیر سامانه 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider , IWebHostEnvironment webHostEnvironment)
        {
            
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, "uploads"));
                }
                // Look for any movies.
                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }
                var role = new Role() { RoleTitle = "مدیریت", Name = "Admin", NormalizedName = "ADMIN", Description = "مدیریت سامانه با دسترسی کامل" };
                context.Roles.Add(role);    
                context.Roles.AddRange(
                       new Role() {  RoleTitle = "کارشناس فروش", Name = "Sales", NormalizedName = "SALES", Description = "کاربر با نقش کارشناس فروش" },
                       new Role() {  RoleTitle = "مشتری", Name = "Customer", NormalizedName = "CUSTOMER", Description = "کاربر با نقش مشتری " }
                );

                var hasher = new PasswordHasher<User>();
                var user = new User()
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FirstName = "مدیر",
                    LastName = "سامانه",
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = hasher.HashPassword(null, "admin"),
                };
                
                context.Users.Add(user);
                context.SaveChanges();

                //var adminUser = context.Users.FirstOrDefault(x=>x.UserName = admin)
                context.UserRoles.Add(new UserRole() { UserId = user.Id , RoleId = role.Id });
                context.SaveChanges();

               

            }


        }
    }
}
