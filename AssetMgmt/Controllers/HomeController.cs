using AssetMgmt.DAL;
using AssetMgmt.Models;
using AssetMgmt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetMgmt.Controllers
{
    public class HomeController : Controller
    {
        private AssetContext db = new AssetContext();
        public ActionResult Index()
        {
            AssetService assetService = new AssetService();
            List<AssetSummary> result = assetService.CalcSummary();
            decimal totalAmountSGD = (from od in result select od.Amount).Sum();
            ViewBag.TotalAmountSGD = totalAmountSGD;

            List<AssetHistory> assetHistories = db.AssetHistories.Take(10).OrderByDescending(d => d.RecordDate).ToList();
            ViewBag.AssetHistories = assetHistories;

            return View(result);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowChart()
        {
            AssetService assetService = new AssetService();
            List<AssetSummary> result = assetService.CalcSummary();
            return View(result);
        }

        public ActionResult ShowAssetHistory()
        {
            List<AssetHistory> result = db.AssetHistories.ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult SaveSummary()
        {
            AssetService assetService = new AssetService();
            assetService.SaveCurrentSummary();
            return Content("success");
        }
    }
}