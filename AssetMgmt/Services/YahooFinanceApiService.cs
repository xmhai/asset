using AssetMgmt.DAL;
using AssetMgmt.Models;
using AssetMgmt.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AssetMgmt.Services
{
    internal class YahooFinanceApiService
    {
        public static Dictionary<string, decimal> GetStockPrices()
        {
            AssetContext db = new AssetContext();
            List<Stock> l = db.Stocks.ToList();

            //string yahooResult = GetStockPriceXML(l);
            //return Parse(yahooResult);
            return GetStockPriceXML1(l);

        }

        static Dictionary<string, decimal> GetStockPriceXML1(List<Stock> l)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            if (l.Count == 0)
            {
                return null;
            }

            foreach (Stock s in l)
            {
                result.Add(s.Code, GetStockPriceGoogleFinance(s.Code));
            }
            return result;
        }

        static decimal getStockPrice(String code)
        {
            string url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20csv%20where%20url%3D'http%3A%2F%2Fdownload.finance.yahoo.com%2Fd%2Fquotes.csv%3Fs%3D{0}%26f%3Dsl1d1t1c1ohgv%26e%3D.csv'%20and%20columns%3D'symbol%2Cprice%2Cdate%2Ctime%2Cchange%2Ccol1%2Chigh%2Clow%2Ccol2'&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            url = string.Format(url, code);
            string resultXml = HttpUtil.GetResponse(url);
            XDocument xDoc = XDocument.Parse(resultXml);
            string value = xDoc.Root.Element("results").Element("row").Element("price").Value;
            return Convert.ToDecimal(value);
        }

        static string GetStockPriceXML(List<Stock> l)
        {
            //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22D05.SI%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22)%0A%09%09&env=http%3A%2F%2Fdatatables.org%2Falltables.env

            string stockStr = BuildStockCodeString(l);
            //string url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20("
            //    + stockStr + ")%0A%09%09&env=http%3A%2F%2Fdatatables.org%2Falltables.env";
            string url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20("
                + stockStr + ")&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            int tryTimes = 3;
            string result = "";
            while (tryTimes > 0)
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    result = HttpUtil.GetResponse(url);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    if (tryTimes==0)
                    {
                        throw e;
                    }
                }
                tryTimes--;
            }
            return result;
        }

        static decimal GetStockPriceGoogleFinance(String code)
        {
            string googleCode = "";
            if (code.EndsWith(".SI"))
            {
                googleCode = "SGX+" + code.Substring(0, code.IndexOf("."));
            }
            string url = "https://finance.google.com/finance?q={0}";
            url = string.Format(url, googleCode);
            string result = HttpUtil.GetResponse(url);

            Regex regex = new Regex("itemprop=\"price\"\\s+content=\"(?<price>.*)\"");
            Match match = regex.Match(result);

            string value = "0.00";
            if (match.Success)
            {
                value = match.Groups["price"].Value;
            }

            return Convert.ToDecimal(value);
        }

        static string BuildStockCodeString(List<Stock> l)
        {
            if (l.Count==0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (Stock s in l)
            {
                sb.Append("%22"+s.Code+"%22%2C");
            }
            sb.Length = sb.Length - "%2C".Length;
            return sb.ToString();
        }

        static Dictionary<string, decimal> Parse(string xml)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                while (reader.ReadToFollowing("quote"))
                {
                    reader.MoveToFirstAttribute();
                    string stockCode = reader.Value;

                    reader.ReadToFollowing("LastTradePriceOnly");
                    string last = reader.ReadElementContentAsString();
                    result.Add(stockCode, Convert.ToDecimal(last));
                }
            }
            return result;
        }
    }
}