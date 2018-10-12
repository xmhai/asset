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

namespace AssetMgmt.Controllers
{
    public class PortfolioController : Controller
    {
        private AssetContext db = new AssetContext();

        // GET: Portfolio
        public ActionResult Index()
        {
            List<Portfolio> l = db.Portfolios.OrderBy(d => d.Stock.Name).Include(d => d.Stock).ToList();

            decimal totalAmountSGD = (from od in l select od.AmountSGD).Sum();
            ViewBag.TotalAmountSGD = totalAmountSGD;

            decimal totalPLSGD = (from od in l select od.PL).Sum();
            ViewBag.totalPLSGD = totalPLSGD;

            return View(l);
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name");
            return View();
        }

        // POST: Portfolio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StockID,Quantity,Cost")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                portfolio.Stock = (from o in db.Stocks where o.ID == portfolio.StockID select o).FirstOrDefault();
                db.Portfolios.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", portfolio.StockID);
            return View(portfolio);
        }

        // GET: Portfolio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", portfolio.StockID);
            return View(portfolio);
        }

        // POST: Portfolio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StockID,Quantity,Cost")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", portfolio.StockID);
            return View(portfolio);
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: Portfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolio portfolio = db.Portfolios.Find(id);
            db.Portfolios.Remove(portfolio);
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
