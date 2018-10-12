using AssetMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt.Services
{
    public class ShareInvestorHtmlStockPriceService : HtmlStockPriceService
    {
        string _url = "http://www.shareinvestor.com/fundamental/factsheet.html?counter={0}";
        string _pattern = "<td.*sic_lastdone.*<strong>(?<price>.*)<\\/strong><\\/td>";

        public override string Url { get { return _url; } }
        public override string Pattern { get { return _pattern; } }

        public override string getStockCode(Stock stock)
        {
            if (stock.Code.IndexOf(".")>=0)
            {
                return stock.Code;
            }
            else
            {
                if (stock.Exchange == Models.Enum.Exchange.SG)
                {
                    return stock.Code + ".SI";
                }
                else if (stock.Exchange == Models.Enum.Exchange.HK)
                {
                    return stock.Code + ".HK";
                }
                else if (stock.Exchange == Models.Enum.Exchange.NQ)
                {
                    return stock.Code + ".NQ";
                }
                else if (stock.Exchange == Models.Enum.Exchange.US)
                {
                    return stock.Code + ".NY";
                }
                else if (stock.Exchange == Models.Enum.Exchange.CN)
                {
                    return stock.Code + ".CN";
                }
                else
                {
                    return stock.Code + ".NM";
                }
            }
        }
    }
}