using AssetMgmt.Models;
using AssetMgmt.Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Configuration;

namespace AssetMgmt.Services
{
    public class ExchangeRateService
    {
        private static Dictionary<Currency, decimal> Rates = new Dictionary<Currency, decimal>();

        static ExchangeRateService()
        {
            //Rates = YahooExchangeRateService.GetExchangeRates();

            // read from config file since YAHOO is not longer working
            Rates.Add(Currency.USD, Convert.ToDecimal(ConfigurationManager.AppSettings["USD"]));
            Rates.Add(Currency.CNY, Convert.ToDecimal(ConfigurationManager.AppSettings["CNY"]));
            Rates.Add(Currency.HKD, Convert.ToDecimal(ConfigurationManager.AppSettings["HKD"]));
        }

        public static decimal GetRate(Currency currency)
        {
            if (Rates.ContainsKey(currency))
                return Rates[currency];

            return 1.0M;
        }

        public static string Html
        {
            get
            {
                string outputHtml = "";
                foreach (KeyValuePair<Currency, decimal> entry in Rates)
                {
                    outputHtml = outputHtml + entry.Key + ": " + entry.Value + " SGD" + "<br/>";
                }
                return outputHtml;
            }
        }
    }
}