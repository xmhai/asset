using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        public int StockID { get; set; }

        public AssetMgmt.Models.Enum.Action Action { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public virtual Stock Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}