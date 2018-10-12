using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class AssetSummary
    {
        public string Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal Percentage { get; set; }

        public string PieCategory
        {
            get { return Category + "\n" + String.Format("{0:P2}", Percentage); }
        }
    }
}