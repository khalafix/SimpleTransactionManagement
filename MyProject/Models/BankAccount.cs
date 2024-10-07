using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class BankAccount
    {
        [Display(Name="شناسه")]public int Id { get; set; }
        [Display(Name="شماره حساب")]public string? AccountNumber { get; set; }
        [Display(Name="تاریخ ایجاد")]public DateTime CreatedDateTime { get; set; }= DateTime.Now;
        [Display(Name="شناسه کاربر")]public int CreatedUserId { get; set; }
        [Display(Name="فعال")]public bool IsActive { get; set; }
        [Display(Name="اولویت")]public int Periority { get; set; }
        [Display(Name="کف واریز")]public long MinAmount { get; set; }
        [Display(Name="سقف واریز")]public long DepositLimit { get; set; }
        [Display(Name="سقف کل")]public long  TotalLimit { get; set; }
        [Display(Name = "ایجاد کننده")] public User? CreatedUser { get; set; }
    }
}
