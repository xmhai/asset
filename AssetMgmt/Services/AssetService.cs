using AssetMgmt.DAL;
using AssetMgmt.Models;
using AssetMgmt.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssetMgmt.Services
{
    public class AssetService
    {
        private AssetContext db = new AssetContext();

        public List<AssetSummary> CalcSummary()
        {
            List<AssetSummary> result = new List<AssetSummary>();

            // cash
            List<Cash> cashList = db.Cash.ToList();
            decimal cashAmount = (from cash in cashList select cash.AmountSGD).Sum();

            AssetSummary assetSummary = new AssetSummary();
            assetSummary.Category = "Cash";
            assetSummary.Amount = cashAmount;
            result.Add(assetSummary);

            // portofolio
            List<Portfolio> portfolioList = db.Portfolios.ToList();
            // stock, etf
            decimal stockAmount = (from p in portfolioList.Where(x=>x.Stock.Category==StockCategory.STOCK || x.Stock.Category == StockCategory.ETF) select p.AmountSGD).Sum();
            assetSummary = new AssetSummary();
            assetSummary.Category = "Stock";
            assetSummary.Amount = stockAmount;
            result.Add(assetSummary);

            // reits
            decimal reitsAmount = (from p in portfolioList.Where(x => x.Stock.Category == StockCategory.REITS) select p.AmountSGD).Sum();
            assetSummary = new AssetSummary();
            assetSummary.Category = "Reits";
            assetSummary.Amount = reitsAmount;
            result.Add(assetSummary);

            // bond
            decimal bondAmount = (from p in portfolioList.Where(x => x.Stock.Category == StockCategory.BOND) select p.AmountSGD).Sum();
            assetSummary = new AssetSummary();
            assetSummary.Category = "Bond";
            assetSummary.Amount = bondAmount;
            result.Add(assetSummary);

            // property

            // calculate percentage
            decimal amount = (from asset in result select asset.Amount).Sum();
            foreach (AssetSummary a in result)
            {
                a.Percentage = a.Amount / amount;
            }

            return result;
        }

        internal void SaveCurrentSummary()
        {
            List<AssetSummary> result = CalcSummary();

            AssetHistory assetHistory = db.AssetHistories.OrderByDescending(i => i.RecordDate).FirstOrDefault();
            if (assetHistory == null || assetHistory.RecordDate.Date != DateTime.Now.Date)
            {
                // if no record of current data
                assetHistory = new AssetHistory();
                db.AssetHistories.Add(assetHistory);
            }
            else
            {
                db.Entry(assetHistory).State = EntityState.Modified;
            }

            // set values
            assetHistory.RecordDate = DateTime.Now;
            foreach (AssetSummary a in result)
            {
                if (a.Category== "Cash")
                {
                    assetHistory.Cash = a.Amount;
                }
                else if (a.Category == "Stock")
                {
                    assetHistory.Stock = a.Amount;
                }
                else if (a.Category == "Reits")
                {
                    assetHistory.Reits = a.Amount;
                }
                else if (a.Category == "Bond")
                {
                    assetHistory.Bond = a.Amount;
                }
            }

            db.SaveChanges();
        }
    }
}