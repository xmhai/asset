using AssetMgmt.Models.Enum;
using AssetMgmt.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Cash
    {
        public int CashID { get; set; }

        [Display(Name = "Type")]
        public CashType CashType { get; set; }

        [Display(Name = "Bank")]
        public string BankName { get; set; }

        [Display(Name = "Account Name")]
        public string BankAccountName { get; set; }

        [Display(Name = "Account No.")]
        public string BankAccountNo { get; set; }

        public Currency Currency { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Amount { get; set; }


        [Display(Name = "Amount (SGD)")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal AmountSGD
        {
            get
            {
                return ExchangeRateService.GetRate(Currency) * Amount;
            }
        }

        [DataType(DataType.Date)]
        [Display(Name = "Maturity Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? MaturityDate { get; set; }

        [Display(Name = "Updated Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime UpdatedDate { get; set; }
    }
}
