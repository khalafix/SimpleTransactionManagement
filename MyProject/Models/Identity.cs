using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class User : IdentityUser<int>
    {
        [Display(Name="نام")][StringLength(450)] public string? FirstName { get; set; }
        [Display(Name="نام خانوادگی")][StringLength(450)] public string? LastName { get; set; }
        [Display(Name="نام کامل")][NotMapped]public string? DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }
        [Display(Name = "آخرین ورود")] public DateTime? LastLoggedIn { get; set; }
               

        public virtual ICollection<UserToken>? UserTokens { get; set; }

        public virtual ICollection<UserRole>? Roles { get; set; }

        public virtual ICollection<UserLogin>? Logins { get; set; }

        public virtual ICollection<UserClaim>? Claims { get; set; }
    }

    public class Role : IdentityRole<int>
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
       [Display(Name="عنوان نقش")] public string? RoleTitle { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<UserRole>? Users { get; set; }

        public virtual ICollection<RoleClaim>? Claims { get; set; }

    }
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User? User { get; set; }

        public virtual Role? Role { get; set; }
    }

    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role? Role { get; set; }

    }

    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User? User { get; set; }
    }
    public class UserLogin : IdentityUserLogin<int>
    {
        public virtual User? User { get; set; }
    }
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User? User { get; set; }
    }
}
