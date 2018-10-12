using AssetMgmt.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Portfolio
    {
        public int ID { get; set; }

        public int StockID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public virtual Stock Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Last
        {
            get
            {
                if (Stock==null)
                {
                    return 0.0M;
                }

                return StockPriceService.GetLast(Stock.Code);
            }
        }

        [Display(Name = "Profit/Lost")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal PL
        {
            get
            {
                if (Stock == null)
                {
                    return 0.0M;
                }

                return (Last - Cost) * Quantity * ExchangeRateService.GetRate(Stock.Currency);
            }
        }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Amount
        {
            get
            {
                return Last * Quantity;
            }
        }

        [Display(Name = "Amount (SGD)")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
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