using AssetMgmt.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Divident
    {
        public int ID { get; set; }

        public int StockID { get; set; }

        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PayDate { get; set; }

        public virtual Stock Stock { get; set; }

        [Display(Name = "Amount (SGD)")]
        public decimal AmountSGD
        {
            get
            {
                if (Stock == null)
                {
                    return 0.0M;
                }

                return ExchangeRateService.GetRate(Stock.Currency) * Amount;
            }
        }
    }
}