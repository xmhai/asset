using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class AssetHistory
    {
        public int ID { get; set; }

        public decimal Cash { get; set; }

        public decimal Stock { get; set; }

        public decimal Reits { get; set; }

        public decimal ETF { get; set; }

        public decimal Bond { get; set; }

        public decimal Property { get; set; }

        public decimal Loan { get; set; }

        public decimal Debt { get; set; }

        public decimal CPFOrdinary { get; set; }

        public decimal CPFMedisave { get; set; }

        public decimal CPFSpecial { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Amount
        {
            get { return Cash + Stock + Reits + ETF + Bond + Property + Loan + CPFOrdinary + CPFSpecial - Debt; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime RecordDate { get; set; }
    }
}