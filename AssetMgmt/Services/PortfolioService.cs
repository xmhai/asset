using AssetMgmt.DAL;
using AssetMgmt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssetMgmt.Services
{
    public class PortfolioService
    {
        private AssetContext db = new AssetContext();

        public void UpdatePortfolio(Transaction tran, bool isRevert)
        {
            // get current portfolio
            Portfolio portfolio = (from o in db.Portfolios where o.StockID == tran.StockID select o).FirstOrDefault();
            if (portfolio == null)
            {
                // first transaction
                portfolio = new Portfolio();
                portfolio.StockID = tran.StockID;
                portfolio.Cost = tran.Price;
                portfolio.Quantity = tran.Quantity;
                portfolio.Stock = (from o in db.Stocks where o.ID == portfolio.StockID select o).FirstOrDefault();
                db.Portfolios.Add(portfolio);
                db.SaveChanges();
                return;
            }

            decimal totalCost = portfolio.Cost * portfolio.Quantity;

            if ((tran.Action == Models.Enum.Action.BUY && !isRevert) || (tran.Action == Models.Enum.Action.SELL && isRevert))
            {
                portfolio.Quantity += tran.Quantity;
                totalCost += tran.Amount;
            }
            else
            {
                portfolio.Quantity -= tran.Quantity;
                totalCost -= tran.Amount;
            }

            // update to db
            if (portfolio.Quantity == 0)
            {
                db.Portfolios.Remove(portfolio);

                // update profit (realised)
                decimal d = (tran.Price - portfolio.Cost) * tran.Quantity;
                if (d!=0)
                {
                    Profit p = (from o in db.Profits where o.StockID == tran.StockID select o).FirstOrDefault();
                    if (p == null)
                    {
                        p = new Profit();
                        p.StockID = tran.StockID;
                        p.Amount = d;
                        db.Profits.Add(p);
                    }
                    else
                    {
                        p.Amount += d;
                        db.Entry(p).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                portfolio.Cost = totalCost / portfolio.Quantity;
                db.Entry(portfolio).State = EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}