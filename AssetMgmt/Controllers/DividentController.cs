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
    public class DividentController : Controller
    {
        private AssetContext db = new AssetContext();

        // GET: Divident
        public ActionResult Index()
        {
            //var dividents = db.Dividents.Include(d => d.Stock);
            List<Divident> l = db.Dividents.ToList();

            decimal totalAmountSGD = (from od in l select od.AmountSGD).Sum();
            ViewBag.TotalAmountSGD = totalAmountSGD;

            //return View(dividents.OrderByDescending(o => o.PayDate).ToList());
            return View(l.OrderByDescending(o => o.PayDate).ToList());
        }

        // GET: Divident/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Divident divident = db.Dividents.Find(id);
            if (divident == null)
            {
                return HttpNotFound();
            }
            return View(divident);
        }

        // GET: Divident/Create
        public ActionResult Create()
        {
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name");
            return View();
        }

        // POST: Divident/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StockID,Amount,PayDate")] Divident divident)
        {
            if (ModelState.IsValid)
            {
                db.Dividents.Add(divident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", divident.StockID);
            return View(divident);
        }

        // GET: Divident/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Divident divident = db.Dividents.Find(id);
            if (divident == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", divident.StockID);
            return View(divident);
        }

        // POST: Divident/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StockID,Amount,PayDate")] Divident divident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(divident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockID = new SelectList(db.Stocks.OrderBy(o => o.Name).ToList(), "ID", "Name", divident.StockID);
            return View(divident);
        }

        // GET: Divident/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Divident divident = db.Dividents.Find(id);
            if (divident == null)
            {
                return HttpNotFound();
            }
            return View(divident);
        }

        // POST: Divident/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Divident divident = db.Dividents.Find(id);
            db.Dividents.Remove(divident);
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
