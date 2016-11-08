using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.IBSL;
using CKWMS.EFModels;
using CKWMS.BSL;

namespace CKWMS.Controllers
{
    public class wms_scaninController : Controller
    {
        // GET: wms_scanin
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            var tempData = ServiceFactory.wms_rukudanservice.LoadSortEntities(p => p.IsDelete == false && p.RukuZT < 5, false, s => s.RukuRQ);
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }
        public ActionResult Recieving()
        {
            int _userid = (int)Session["user_id"];
            var _rkdid = Request["rkd"] ?? "";
            if (string.IsNullOrEmpty(_rkdid))
                _rkdid = "0";
            var _rkmx = ServiceFactory.wms_rukumxservice.LoadEntities(p => p.RukuID == int.Parse(_rkdid) && p.IsDelete == false).ToList<wms_rukumx>();
            if (_rkmx == null)
            {
                ViewBag.waitnum = 0;
                ViewBag.cargonum = 0;
                ViewBag.rkd = _rkdid;
                ViewBag.rkbh = "";
            }
            else
            {
                var data = from u in _rkmx
                           group u by u.RukuID into b
                           select new
                           {
                               rkd = b.Key,
                               spnum = b.Sum(p => p.DaohuoSL),
                               waitnum = b.Sum(p => p.YishouSL)
                           };
                foreach (var da in data)
                {
                    ViewBag.cargonum = da.spnum;
                    ViewBag.waitnum = da.waitnum;
                    ViewBag.rkd = da.rkd;
                }
                var _rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkdid));
                if (_rkd == null)
                    ViewBag.rkbh = "";
                else
                    ViewBag.rkbh = _rkd.RukudanBH;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRecieve()
        {
            int _userid = (int)Session["user_id"];
            var _rkd = Request["rkid"] ?? "";
            var _sp = Request["shangpin"] ?? "";
            var _spnum = Request["spnum"] ?? "";
            var _ph = Request["pihao"] ?? "";
            var _phnum = Request["phnum"] ?? "";
            var _xl = Request["xlm"] ?? "";
            var _xlnum = Request["xlnum"] ?? "";

            if (string.IsNullOrEmpty(_rkd))
                _rkd = "0";
            var _rkmx = ServiceFactory.wms_rukumxservice.LoadEntities(p => p.RukuID == int.Parse(_rkd) && p.IsDelete == false).ToList<wms_rukumx>();
            if (_rkmx == null)
                return RedirectToAction("Index");

            if (string.IsNullOrEmpty(_sp))
                _spnum = "0";
            else
            {
                //var _mxsp = from u in _rkmx
                //            group u by u.ShangpinTM into tb
                //            select new
                //            {
                //                sptm = tb.Key,
                //                spcount = tb.Count()
                //            };
                var _mxsp = _rkmx.Where(p => p.ShangpinTM == _sp).ToList<wms_rukumx>();
                if (_mxsp != null)
                {
                    if (_mxsp.Count() == 1 && int.Parse(_spnum) > 0)
                    {
                        wms_shouhuomx _sh = AddRecieveRec(_mxsp[0], int.Parse(_spnum), _userid);
                        if (_sh == null)
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Recieving", new { rkd = _rkd });
                    }
                }
            }
            if (string.IsNullOrEmpty(_ph))
                _phnum = "0";
            else
            {
                var _mxph = _rkmx.Where(p => p.Pihao == _ph).ToList<wms_rukumx>();
                if (_mxph != null)
                {
                    if (_mxph.Count() == 1 && int.Parse(_phnum) > 0)
                    {
                        wms_shouhuomx _sh = AddRecieveRec(_mxph[0], int.Parse(_phnum), _userid);
                        if (_sh == null)
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Recieving", new { rkd = _rkd });
                    }
                }
            }
            if (string.IsNullOrEmpty(_xl))
                _xlnum = "0";
            else
            {
                var _mxxl = _rkmx.Where(p => p.Xuliema == _xl).ToList<wms_rukumx>();
                if (_mxxl != null)
                {
                    if (_mxxl.Count() == 1 && int.Parse(_xlnum) > 0)
                    {
                        wms_shouhuomx _sh = AddRecieveRec(_mxxl[0], int.Parse(_xlnum), _userid);
                        if (_sh == null)
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Recieving", new { rkd = _rkd });
                    }
                }
            }
            return RedirectToAction("Index");
        }
        private wms_shouhuomx AddRecieveRec(wms_rukumx rkmx, int recnum, int op)
        {
            if (rkmx == null)
                return null;
            wms_shouhuomx _sh = new wms_shouhuomx();
            _sh.RukuID = rkmx.RukuID;
            _sh.RKMXID = rkmx.ID;
            _sh.ShangpinID = rkmx.ShangpinID;
            _sh.ShangpinDM = rkmx.ShangpinDM;
            _sh.ShangpinMC = rkmx.ShangpinMC;
            _sh.ShangpinTM = rkmx.ShangpinTM;
            _sh.ShengchanRQ = rkmx.ShengchanRQ;
            _sh.ShixiaoRQ = rkmx.ShixiaoRQ;
            _sh.Zhucezheng = rkmx.Zhucezheng;
            _sh.Guige = rkmx.Guige;
            _sh.Xuliema = rkmx.Xuliema;
            _sh.Pihao = rkmx.Pihao;
            _sh.Pihao1 = rkmx.Pihao1;
            _sh.JibenDW = rkmx.JibenDW;
            _sh.BaozhuangDW = rkmx.BaozhuangDW;
            _sh.Chandi = rkmx.Chandi;
            _sh.Changjia = rkmx.Changjia;
            _sh.Huansuanlv = rkmx.Huansuanlv;
            _sh.MakeMan = op;
            _sh.MakeDate = DateTime.Now;
            _sh.Shuliang = recnum;
            _sh.Zhongliang = (recnum / rkmx.DaohuoSL) * rkmx.Zhongliang;
            _sh.Jingzhong = (recnum / rkmx.DaohuoSL) * rkmx.Jingzhong;
            _sh.Tiji = (recnum / rkmx.DaohuoSL) * rkmx.Tiji;
            _sh.Jifeidun = (recnum / rkmx.DaohuoSL) * rkmx.Jifeidun;
            _sh = ServiceFactory.wms_shouhuomxservice.AddEntity(_sh);
            return _sh;
        }
        public ActionResult EntryCheck()
        {
            return View();
        }
        public ActionResult UploadCargo()
        {
            int _userid = (int)Session["user_id"];
            var _rkdid = Request["rkd"] ?? "";
            if (string.IsNullOrEmpty(_rkdid))
                _rkdid = "0";
            var _rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkdid));
            if (_rkd == null)
                ViewBag.rkbh = "";
            else
                ViewBag.rkbh = _rkd.RukudanBH;
            ViewBag.rkd = _rkdid;
            return View();
        }
        public JsonResult SaveUpload()
        {
            int _userid = (int)Session["user_id"];
            var _rkid = Request["rkd"] ?? "";
            var _kuwei = Request["kw"] ?? "";
            var _kwid = Request["kwid"] ?? "";
            var _ph1 = Request["pihao1"] ?? "";
            var _phnum1 = Request["phnum1"] ?? "";
            var _ph2 = Request["pihao2"] ?? "";
            var _phnum2 = Request["phnum2"] ?? "";
            var _ph3 = Request["pihao3"] ?? "";
            var _phnum3 = Request["phnum3"] ?? "";
            var _xlm1 = Request["xlm1"] ?? "";
            var _xlnum1 = Request["xlnum1"] ?? "";
            var _xlm2 = Request["xlm2"] ?? "";
            var _xlnum2 = Request["xlnum2"] ?? "";
            var _xlm3 = Request["xlm3"] ?? "";
            var _xlnum3 = Request["xlnum3"] ?? "";
            if (string.IsNullOrEmpty(_rkid) || string.IsNullOrEmpty(_kuwei))
                return Json(-1);
            if (!string.IsNullOrEmpty(_ph1))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Pihao == _ph1 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_phnum1)>0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_phnum1), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            if (!string.IsNullOrEmpty(_ph2))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Pihao == _ph2 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_phnum2) > 0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_phnum2), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            if (!string.IsNullOrEmpty(_ph3))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Pihao == _ph3 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_phnum3) > 0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_phnum3), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            if (!string.IsNullOrEmpty(_xlm1))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Xuliema==_xlm1 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_xlnum1) > 0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_xlnum1), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            if (!string.IsNullOrEmpty(_xlm2))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Xuliema == _xlm2 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_xlnum2) > 0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_xlnum2), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            if (!string.IsNullOrEmpty(_xlm3))
            {
                var _sh = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.Xuliema == _xlm3 && p.IsDelete == false).ToList<wms_shouhuomx>();
                if (_sh.Count == 1 && int.Parse(_xlnum3) > 0)
                {
                    wms_cunhuo _ch = AddUpload(_sh[0], int.Parse(_xlnum3), _kuwei, int.Parse(_kwid), _userid);
                    if (_ch == null)
                        return Json(-2);
                }
            }
            return Json(1);
        }
        private wms_cunhuo AddUpload(wms_shouhuomx shmx,int upnum,string kuwei,int kwid,int userid)
        {
            wms_cunhuo _ch = new wms_cunhuo();
            _ch.RKMXID = shmx.ID;
            _ch.Kuwei = kuwei;
            _ch.KuweiID = kwid;
            _ch.Shuliang = upnum;
            _ch.HegeSF = shmx.Yanshou ;
            _ch = ServiceFactory.wms_cunhuoservice.AddEntity(_ch);
            return _ch;
        }
    }
}