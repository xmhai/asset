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
    public class AssetHistoryController : Controller
    {
        private AssetContext db = new AssetContext();

        // GET: AssetHistory
        public ActionResult Index()
        {
            return View(db.AssetHistories.ToList());
        }

        // GET: AssetHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetHistory assetHistory = db.AssetHistories.Find(id);
            if (assetHistory == null)
            {
                return HttpNotFound();
            }
            return View(assetHistory);
        }

        // GET: AssetHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cash,Stock,Reits,ETF,Bond,Property,Loan,Debt,CPFOrdinary,CPFMedisave,CPFSpecial,Amount,RecordDate")] AssetHistory assetHistory)
        {
            if (ModelState.IsValid)
            {
                db.AssetHistories.Add(assetHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assetHistory);
        }

        // GET: AssetHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetHistory assetHistory = db.AssetHistories.Find(id);
            if (assetHistory == null)
            {
                return HttpNotFound();
            }
            return View(assetHistory);
        }

        // POST: AssetHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cash,Stock,Reits,ETF,Bond,Property,Loan,Debt,CPFOrdinary,CPFMedisave,CPFSpecial,Amount,RecordDate")] AssetHistory assetHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetHistory);
        }

        // GET: AssetHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetHistory assetHistory = db.AssetHistories.Find(id);
            if (assetHistory == null)
            {
                return HttpNotFound();
            }
            return View(assetHistory);
        }

        // POST: AssetHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetHistory assetHistory = db.AssetHistories.Find(id);
            db.AssetHistories.Remove(assetHistory);
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
