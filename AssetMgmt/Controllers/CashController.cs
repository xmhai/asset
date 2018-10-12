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
    public class CashController : Controller
    {
        private AssetContext db = new AssetContext();

        // GET: Cash
        public ActionResult Index()
        {
            List<Cash> l = db.Cash.ToList();
            decimal totalAmountSGD = (from od in l select od.AmountSGD).Sum();
            ViewBag.TotalAmountSGD = totalAmountSGD;
            ViewBag.ExchangeRates = ExchangeRateService.Html;
            return View(l);
        }

        // GET: Cash/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                return HttpNotFound();
            }
            return View(cash);
        }

        // GET: Cash/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cash/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashType,BankName,BankAccountName,BankAccountNo,Currency,Amount,MaturityDate")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                cash.UpdatedDate = DateTime.Now;
                db.Cash.Add(cash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cash);
        }

        // GET: Cash/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                return HttpNotFound();
            }
            return View(cash);
        }

        // POST: Cash/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashID,CashType,BankName,BankAccountName,BankAccountNo,Currency,Amount,MaturityDate")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                cash.UpdatedDate = DateTime.Now;
                db.Entry(cash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cash);
        }

        // GET: Cash/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                return HttpNotFound();
            }
            return View(cash);
        }

        // POST: Cash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cash cash = db.Cash.Find(id);
            db.Cash.Remove(cash);
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
