using MyProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject.ViewModels
{
    public class TransactionViewModel
    {

        [Display(Name = "شناسه")] public int Id { get; set; }
        [Display(Name = "شناسه حساب")] public int BankAccountId { get; set; }
        //[Display(Name = "شناسه کاربر")][ForeignKey("User")] public int UserId { get; set; }
        [Display(Name = "نام")] public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی ")]public string? LastName { get; set; }
        public string? DisplayName
        {
            get
            {
                return  $"{FirstName} {LastName}";
            }
        }


        [Display(Name = "زمان واریزی")] public DateTime DepositDateTime { get; set; }
        [Display(Name = "مبلغ واریزی")] public int DepositAmount { get; set; }
        [Display(Name = "فیش واریزی")] public string? DepositReceipt { get; set; }
        [Display(Name = "فیش واریزی")] public IFormFile? DepositReceiptFile { get; set; }
        [Display(Name = "شماره حساب")] public BankAccount? BankAccount { get; set; }
    }
}
