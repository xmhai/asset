using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Profit
    {
        public int ID { get; set; }

        public int StockID { get; set; }

        public decimal Amount { get; set; }

        public virtual Stock Stock { get; set; }
    }
}