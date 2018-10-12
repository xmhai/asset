using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt.Services
{
    public class YahooExchangeRatesJson
    {
        [JsonProperty("query")]
        public YahooExchangeRatesJsonQuery Query { get; set; }

        public class YahooExchangeRatesJsonQuery
        {
            [JsonProperty("count")]
            public int Count { get; set; }
            [JsonProperty("created")]
            public string Created { get; set; }
            [JsonProperty("lang")]
            public string Lang { get; set; }
            [JsonProperty("results")]
            public YahooExchangeRatesJsonRates Results { get; set; }
        }

        public class YahooExchangeRatesJsonRates
        {
            [JsonProperty("rate")]
            public List<YahooExchangeRatesJsonRate> Rates { get; set; }
        }

        public class YahooExchangeRatesJsonRate
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("Name")]
            public string Name { get; set; }
            [JsonProperty("Rate")]
            public decimal Rate { get; set; }
            [JsonProperty("Date")]
            public string Date { get; set; }
            [JsonProperty("Time")]
            public string Time { get; set; }
            [JsonProperty("Ask")]
            public decimal Ask { get; set; }
            [JsonProperty("Bid")]
            public decimal Bid { get; set; }
        }
    }
}