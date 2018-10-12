using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetMgmt.DAL;
using AssetMgmt.Models;
using AssetMgmt.Services;

namespace AssetMgmt.Controllers
{
    public class ProfitController : Controller
    {
        private AssetContext db = new AssetContext();

        // GET: Profit
        public ActionResult Index()
        {
            List<PL> pls = new List<PL>();

            // get realised profit
            var profits = db.Profits.Include(p => p.Stock);
            foreach (Profit p in profits)
            {
                PL pl = new PL();
                pl.Stock = p.Stock;
                pl.RealisedAmount = p.Amount * ExchangeRateService.GetRate(p.Stock.Currency);
                pls.Add(pl);
            }

            // get unrealised profit
            var portfolios = db.Portfolios.Include(p => p.Stock);
            foreach (Portfolio p in portfolios)
            {
                PL pl = pls.Where(item => item.Stock.ID == p.StockID).FirstOrDefault();
                if (pl == null)
                {
                    pl = new PL();
                    pl.Stock = p.Stock;
                    pls.Add(pl);
                }
                pl.UnrealisedAmount = p.PL;
            }

            // add divident
            var dividents = db.Dividents.Include(p => p.Stock);
            foreach (Divident d in dividents)
            {
                PL pl = pls.Where(item => item.Stock.ID == d.StockID).FirstOrDefault();
                if (pl == null)
                {
                    pl = new PL();
                    pl.Stock = d.Stock;
                    pls.Add(pl);
                }
                pl.RealisedAmount += d.AmountSGD;
            }

            // update total profit/loss
            foreach (PL pl in pls)
            {
                pl.Amount = pl.RealisedAmount + pl.UnrealisedAmount;
            }

            decimal totalAmountSGD = (from od in pls select od.Amount).Sum();
            ViewBag.TotalAmountSGD = totalAmountSGD;

            return View(pls);
        }

        // GET: Profit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profit profit = db.Profits.Find(id);
            if (profit == null)
            {
                return HttpNotFound();
            }
            return View(profit);
        }

        // GET: Profit/Create
        public ActionResult Create()
        {
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "Code");
            return View();
        }

        // POST: Profit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StockID,Amount")] Profit profit)
        {
            if (ModelState.IsValid)
            {
                db.Profits.Add(profit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stocks, "ID", "Code", profit.StockID);
            return View(profit);
        }

        // GET: Profit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profit profit = db.Profits.Find(id);
            if (profit == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "Code", profit.StockID);
            return View(profit);
        }

        // POST: Profit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StockID,Amount")] Profit profit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "Code", profit.StockID);
            return View(profit);
        }

        // GET: Profit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profit profit = db.Profits.Find(id);
            if (profit == null)
            {
                return HttpNotFound();
            }
            return View(profit);
        }

        // POST: Profit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profit profit = db.Profits.Find(id);
            db.Profits.Remove(profit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
