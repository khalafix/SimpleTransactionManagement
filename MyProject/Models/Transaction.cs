using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Transaction
    {
       [Display(Name="شناسه")] public int Id { get; set; }
       [Display(Name="شناسه حساب")] public int BankAccountId { get; set; }
       [Display(Name="شناسه کاربر")] [ForeignKey("User")] public int UserId { get; set; }
       [Display(Name="شناسه مشتری")] [ForeignKey("Customer")] public int CustomerId { get; set; }
       [Display(Name="زمان واریزی")] public DateTime DepositDateTime { get; set; }
       [Display(Name="مبلغ واریزی")] public int DepositAmount { get; set; }
       [Display(Name="فیش واریزی")] public string? DepositReceipt { get; set; } 
       [Display(Name="کاربر")] public User? User { get; set; }
       [Display(Name="مشتری")] public User? Customer { get; set; }
       [Display(Name="شماره حساب")] public BankAccount? BankAccount { get; set; }
    }
}
