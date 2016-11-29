using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.EFModels;
using CKWMS.BSL;
using CKWMS.Models;
using CKWMS.Common;
namespace CKWMS.Controllers
{
    public class wms_scanoutController : Controller
    {
        // GET: wms_scanout
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            var tempData = ServiceFactory.wms_chukudanservice.LoadSortEntities(p => p.IsDelete == false && p.JihuaZT < 5, false, s => s.ChukuRQ);
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public JsonResult PickFinish()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckid))
                return Json(-2);
            bool pickok = true;
            var _jhmx = ServiceFactory.wms_jianhuoservice.GetPickDetail(int.Parse(_ckid)).ToList<wms_pick_v>();
            if (_jhmx != null)
            {
                foreach(var jh in _jhmx)
                {
                    if(jh.DaijianSL!=jh.ShijianSL)
                    {
                        pickok = false;
                        break;
                    }
                }
                if (!pickok)
                    return Json(-1);
            }
            else
                return Json(-2);
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            if (_ckd == null)
                return Json(-2);
            _ckd.JihuaZT = 2;
            if (!ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd))
                return Json(-2);
            return Json(1);
        }
        public JsonResult PickDown()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ck"] ?? "";
            var _loc = Request["loc"] ?? "";
            var _ph = Request["ph"] ?? "";
            var _xl = Request["xl"] ?? "";
            var _sl = Request["sl"] ?? "";
            if (string.IsNullOrEmpty(_ckid) || string.IsNullOrEmpty(_loc))
                return Json(-1);
            if (string.IsNullOrEmpty(_ph) && string.IsNullOrEmpty(_xl))
                return Json(-1);
            if ((!string.IsNullOrEmpty(_ph)) && (!string.IsNullOrEmpty(_xl)))
                return Json(-1);
            if (string.IsNullOrEmpty(_sl))
                _sl = "1";
            else
            {
                if (_sl == "0")
                    return Json(-1);
            }
            var _cargoloc = ServiceFactory.wms_jianhuoservice.GetPickDetail(int.Parse(_ckid), p => p.Kuwei == _loc.Trim()).ToList<wms_pick_v>();
            if (_cargoloc == null)
                return Json(-2);
            List<wms_pick_v> _cargo = null;
            if (!string.IsNullOrEmpty(_ph))
            {
                _ph =BarcodeRead.BatchCode(_ph.Trim());
                _cargo = _cargoloc.Where(p => p.Pihao == _ph.Trim()).ToList<wms_pick_v>();
            }
            if (!string.IsNullOrEmpty(_xl))
            {
                _xl = BarcodeRead.SerialNumber(_xl.Trim());
                _cargo = _cargoloc.Where(p => p.Xuliema == _xl.Trim()).ToList<wms_pick_v>();
            }
            if (_cargo == null)
                return Json(-2);
            if (_cargo.Count == 1)
            {
                wms_jianhuo _jh = ServiceFactory.wms_jianhuoservice.GetEntityById(p => p.ID == _cargo[0].pickid);
                if (_jh != null)
                {
                    if (_jh.ShijianSL == null)
                        _jh.ShijianSL = 0;
                    if (_cargo[0].DaijianSL >= _jh.ShijianSL + float.Parse(_sl))
                    {
                        _jh.ShijianSL = _jh.ShijianSL + float.Parse(_sl);
                        ServiceFactory.wms_jianhuoservice.UpdateEntity(_jh);
                    }
                }
            }
            else
            {
                float _sjsl = float.Parse(_sl);
                foreach (var cargo in _cargo)
                {
                    if (cargo != null)
                    {
                        wms_jianhuo _jh = ServiceFactory.wms_jianhuoservice.GetEntityById(p => p.ID == cargo.pickid);
                        if (_jh != null)
                        {
                            if (_jh.ShijianSL == null)
                                _jh.ShijianSL = 0;
                            if (_jh.DaijianSL > _jh.ShijianSL && _sjsl>0)
                            {
                                if (_jh.DaijianSL >= _jh.ShijianSL + _sjsl)
                                {
                                    _jh.ShijianSL = _jh.ShijianSL + _sjsl;
                                    _sjsl = 0;
                                }
                                else
                                {
                                    _sjsl = _sjsl - ((float)_jh.DaijianSL - (float)_jh.ShijianSL);
                                    _jh.ShijianSL = _jh.DaijianSL;
                                }
                                ServiceFactory.wms_jianhuoservice.UpdateEntity(_jh);
                            }
                        }
                    }
                }
            }
            return Json(1);
        }
        public JsonResult GetLocationCargo()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ck"] ?? "";
            var _loc = Request["loc"] ?? "";
            if (string.IsNullOrEmpty(_ckid) || string.IsNullOrEmpty(_loc))
                return Json(-1);
            var _pickdetail = ServiceFactory.wms_jianhuoservice.GetPickDetail(int.Parse(_ckid), p => p.Kuwei == _loc.Trim() && p.DaijianSL > 0).ToList<wms_pick_v>();
            if (_pickdetail == null)
                return Json(-1);
            return Json(_pickdetail);
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
            if (_ckd.JihuaZT > 3)
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
            List<wms_scanpickViewModel> _pickloc = new List<wms_scanpickViewModel>();
            foreach (var loc in _loclist)
            {
                wms_scanpickViewModel _sp = new wms_scanpickViewModel();
                _sp.KuWei = loc.kw;
                _sp.DaijianSL = (float)loc.sl;
                _sp.JianhuoSL = (float)loc.jh;
                _pickloc.Add(_sp);
            }
            ViewBag.pickloc = _pickloc;
            ViewBag.ckid = _ckid;
            ViewBag.ckbh = _ckd.ChukudanBH;
            return View();
        }
    }
}