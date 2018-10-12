using AssetMgmt.Models;
using AssetMgmt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AssetMgmt.Services
{
    public class HtmlStockPriceService
    {
        public virtual string Url { get; }
        public virtual string Pattern { get; }

        public virtual string getStockCode(Stock stock)
        {
            return stock.Code;
        }

        public decimal GetStockPrice(Stock stock)
        {
            string searchCode = getStockCode(stock);
            string url = string.Format(Url, searchCode);
            string result = HttpUtil.GetResponse(url);

            Regex regex = new Regex(Pattern);
            Match match = regex.Match(result);

            string value = "0.00";
            if (match.Success)
            {
                value = match.Groups["price"].Value;
            }

            return Convert.ToDecimal(value);
        }
    }
}