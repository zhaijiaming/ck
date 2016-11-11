using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.EFModels;
using CKWMS.BSL;

namespace CKWMS.Controllers
{
    public class wms_scanoutController : Controller
    {
        // GET: wms_scanout
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            var tempData = ServiceFactory.wms_chukudanservice.LoadSortEntities(p => p.IsDelete == false && p.JihuaZT < 5, false, s=>s.ChukuRQ);
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public ActionResult Picking()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ckd"] ?? "";
            if (string.IsNullOrEmpty(_ckid))
                _ckid = "0";
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            if (_ckd == null)
                return RedirectToAction("Index");
            if (_ckd.JihuaZT >3)
                return RedirectToAction("Index");
            var _pickdetail = ServiceFactory.wms_jianhuoservice.GetPickDetail(int.Parse(_ckid), p => p.DaijianSL > 0).ToList<wms_pick_v>();
            if (_pickdetail == null)
                return RedirectToAction("Index");
            var _loclist = from u in _pickdetail
                           group u by u.Kuwei into tb
                           select new
                           {
                               kw = tb.Key,
                               sl = tb.Sum(p => p.DaijianSL),
                               jh = tb.Sum(p => p.ShijianSL)
                           };
            ViewBag.pickloc = _loclist.ToList();
            ViewBag.ckid = _ckid;
            ViewBag.ckbh = _ckd.ChukudanBH;
            return View();
        }
    }
}