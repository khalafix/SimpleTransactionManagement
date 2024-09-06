using MyProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MyProject.ViewModels
{
    public class RegisterViewModel
    {
   
        public int Id { get; set; }
        [Display(Name = "نام")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][StringLength(100,ErrorMessage ="لطفا {0} را بین {2} تا {1} حرف وارد نمایید!",MinimumLength =2)] public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][StringLength(100,ErrorMessage ="لطفا {0} را بین {2} تا {1} حرف وارد نمایید!",MinimumLength =2)] public string? LastName { get; set; }
        [Display(Name = "نام کاربری")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][StringLength(20,ErrorMessage ="لطفا {0} را بین {2} تا {1} حرف وارد نمایید!",MinimumLength =4)] public string? UserName { get; set; }
       [Display(Name="کلمه عبور")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][DataType(DataType.Password)] public string? Password { get; set; }
       [Display(Name="شناسه نقش")] public int RoleId { get; set; } 
       [Display(Name="نقش")] public Role? Role { get; set; }
       [Display(Name="آخرین ورود")] public DateTime? LastLoggedIn { get; set; }


    }
}
