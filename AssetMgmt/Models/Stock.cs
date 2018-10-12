using AssetMgmt.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Stock
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Exchange Exchange { get; set; }

        public Currency Currency { get; set; }

        public StockCategory Category { get; set; }

        public decimal Price { get; set; }
    }
}