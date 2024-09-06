using MyProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MyProject.ViewModels
{
    public class TransactionInqueryViewModel
    {


        [Display(Name = "نام مشتری")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][StringLength(100, ErrorMessage = "لطفا {0} را بین {2} تا {1} حرف وارد نمایید!", MinimumLength = 2)] public string? CustomerName { get; set; }
        [Display(Name = "مبلغ واریزی")][Required(ErrorMessage = "لطفا {0} را وارد کنید!")][Range(1, int.MaxValue, ErrorMessage = "لطفا {0} را بین {1} الی {2} وارد نمایید!")] public int DepositAmount { get; set; }

        public DateTime? DateTime { get; set; }
        
        public string? AccountNumber { get; set; }


    }
}

