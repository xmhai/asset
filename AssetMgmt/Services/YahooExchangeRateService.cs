using AssetMgmt.Models;
using AssetMgmt.Models.Enum;
using AssetMgmt.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AssetMgmt.Services
{
    public class YahooExchangeRateService
    {
        public static Dictionary<Currency, decimal> GetExchangeRates()
        {
            String yahooResult = GetExchangeRateFromYahoo1();
            return ParseYahooExchangeRateResult1(yahooResult);
        }

        private static string GetExchangeRateFromYahoo()
        {
            // invoke Yahoo web service to exchange rates
            String url = "http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.xchange where pair in (\"USDSGD\", \"CNYSGD\", \"HKDSGD\")&format=json&env=store://datatables.org/alltableswithkeys";
            return HttpUtil.GetResponse(url);
        }

        private static Dictionary<Currency, decimal> ParseYahooExchangeRateResult(string yahooResult)
        {
            Dictionary<Currency, decimal> resultDict = new Dictionary<Currency, decimal>();
            YahooExchangeRatesJson resultJson = JsonConvert.DeserializeObject<YahooExchangeRatesJson>(yahooResult);
            int count = resultJson.Query.Count;
            for (int i = 0; i < count; i++)
            {
                string id = resultJson.Query.Results.Rates[i].Id;
                decimal rate = resultJson.Query.Results.Rates[i].Rate;
                if (id == "USDSGD")
                {
                    resultDict.Add(Currency.USD, rate);
                }
                else if (id == "CNYSGD")
                {
                    resultDict.Add(Currency.CNY, rate);
                }
                else if (id == "HKDSGD")
                {
                    resultDict.Add(Currency.HKD, rate);
                }
            }
            return resultDict;
        }

        private static string GetExchangeRateFromYahoo1()
        {
            // invoke Yahoo web service to exchange rates
            String url = "https://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote";
            return HttpUtil.GetResponse(url);
        }

        private static Dictionary<Currency, decimal> ParseYahooExchangeRateResult1(string yahooResult)
        {
            Dictionary<Currency, decimal> resultDict = new Dictionary<Currency, decimal>();
            XDocument xDoc = XDocument.Parse(yahooResult);

            XElement xElem = xDoc.XPathSelectElement(String.Format("list/resources/resource[field='{0}']", "USD/SGD"));
            String price = xElem.XPathSelectElement("descendant::field[@name='price']").Value;
            decimal rateUSD = Convert.ToDecimal(price);
            resultDict.Add(Currency.USD, Math.Round(rateUSD, 2, MidpointRounding.ToEven));

            xElem = xDoc.XPathSelectElement(String.Format("list/resources/resource[field='{0}']", "USD/CNY"));
            price = xElem.XPathSelectElement("descendant::field[@name='price']").Value;
            decimal rateUSDtoCNY = Convert.ToDecimal(price);
            decimal rateCNY = rateUSD / rateUSDtoCNY;
            resultDict.Add(Currency.CNY, Math.Round(rateCNY, 2, MidpointRounding.ToEven));

            xElem = xDoc.XPathSelectElement(String.Format("list/resources/resource[field='{0}']", "USD/HKD"));
            price = xElem.XPathSelectElement("descendant::field[@name='price']").Value;
            decimal rateUSDtoHKD = Convert.ToDecimal(price);
            decimal rateHKD = rateUSD / rateUSDtoHKD;
            resultDict.Add(Currency.HKD, Math.Round(rateHKD, 2, MidpointRounding.ToEven));

            return resultDict;
        }
    }
}