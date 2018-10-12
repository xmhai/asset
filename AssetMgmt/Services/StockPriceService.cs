using AssetMgmt.DAL;
using AssetMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt.Services
{
    public class StockPriceService
    {
        private static AssetContext db = new AssetContext();

        private static Dictionary<string, decimal> StockPrice;

        static StockPriceService()
        {
            StockPrice = GetStockPrices();
        }

        public static void UpdateStockPrice(string stockCode)
        {
            StockPrice.Remove(stockCode);
            GetLast(stockCode);
        }

        public static Dictionary<string, decimal> GetStockPrices()
        {
            AssetContext db = new AssetContext();
            List<Stock> l = db.Stocks.ToList();

            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            if (l.Count == 0)
            {
                return null;
            }

            //YahooFinanceApiService
            foreach (Stock s in l)
            {
                result.Add(s.Code, GetStockPrice(s));
            }
            return result;

        }

        private static decimal GetStockPrice(Stock s)
        {
            if (s.Code.EndsWith(".UN"))
            {
                return s.Price;
            }

            HtmlStockPriceService stockPriceService = new ShareInvestorHtmlStockPriceService();
            decimal price = stockPriceService.GetStockPrice(s);
            if (price == 0)
            {
                return s.Price;
            }

            return price;
        }


        public static decimal GetLast(string stockCode)
        {
            if (StockPrice.ContainsKey(stockCode))
            {
                return StockPrice[stockCode];
            }
            else
            {
                Stock s = (from o in db.Stocks where o.Code == stockCode select o).FirstOrDefault();
                decimal price = GetStockPrice(s);
                StockPrice.Add(stockCode, price);
                return price;
            }
        }
    }
}