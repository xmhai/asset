using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class PL
    {
        public Stock Stock { get; set; }

        [DefaultValue(0)]
        public decimal RealisedAmount { get; set; }

        [DefaultValue(0)]
        public decimal UnrealisedAmount { get; set; }

        [DefaultValue(0)]
        public decimal Amount { get; set; }
    }
}