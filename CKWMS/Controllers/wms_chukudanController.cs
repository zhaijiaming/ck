using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Generic;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using CKWMS.Filters;

namespace CKWMS.Controllers
{
    public class wms_chukudanController : Controller
    {
        private Iwms_chukudanService ob_wms_chukudanservice = ServiceFactory.wms_chukudanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";
            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";
            string page = "1";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public ActionResult PrintTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult OutOperate()
        {
            int userid = (int)Session["user_id"];
            string page = Request["page"] ?? "1";
            var tempData = ob_wms_chukudanservice.LoadSortEntities(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT<5, false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string fuhesf = Request["fuhesf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukutm = Request["chukutm"] ?? "";
            string baifangqy = Request["baifangqy"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string ckid = Request["ckid"] ?? "";
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (fuhesf.IndexOf("true") >= 0)
                fuhesf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            try
            {
                wms_chukudan ob_wms_chukudan = new wms_chukudan();
                ob_wms_chukudan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_wms_chukudan.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_wms_chukudan.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                ob_wms_chukudan.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                ob_wms_chukudan.KehuMC = kehumc.Trim();
                ob_wms_chukudan.Fahuodizhi = fahuodizhi.Trim();
                ob_wms_chukudan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_wms_chukudan.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                ob_wms_chukudan.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_wms_chukudan.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_wms_chukudan.FuheSF = fuhesf == "" ? false : Boolean.Parse(fuhesf);
                ob_wms_chukudan.ChunyunYQ = chunyunyq.Trim();
                ob_wms_chukudan.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_wms_chukudan.KehuDH = kehudh.Trim();
                ob_wms_chukudan.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_wms_chukudan.Lianxiren = lianxiren.Trim();
                ob_wms_chukudan.LianxiDH = lianxidh.Trim();
                ob_wms_chukudan.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                ob_wms_chukudan.ChukuTM = chukutm.Trim();
                ob_wms_chukudan.BaifangQY = baifangqy.Trim();
                ob_wms_chukudan.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                ob_wms_chukudan.Beizhu = beizhu.Trim();
                ob_wms_chukudan.Col1 = col1.Trim();
                ob_wms_chukudan.Col2 = col2.Trim();
                ob_wms_chukudan.Col3 = col3.Trim();
                ob_wms_chukudan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukudan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukudan.ChukudanBH = chukudanbh.Trim();
                ob_wms_chukudan.CKID= ckid == "" ? 0 : int.Parse(ckid);
                ob_wms_chukudan = ob_wms_chukudanservice.AddEntity(ob_wms_chukudan);
                ViewBag.wms_chukudan = ob_wms_chukudan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OutOperate");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_chukudan tempData = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == id && wms_chukudan.IsDelete == false);
            ViewBag.wms_chukudan = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukudanViewModel wms_chukudanviewmodel = new wms_chukudanViewModel();
                wms_chukudanviewmodel.ID = tempData.ID;
                wms_chukudanviewmodel.HuozhuID = tempData.HuozhuID;
                wms_chukudanviewmodel.JihuaID = tempData.JihuaID;
                wms_chukudanviewmodel.XinxiLY = tempData.XinxiLY;
                wms_chukudanviewmodel.KehuID = tempData.KehuID;
                wms_chukudanviewmodel.KehuMC = tempData.KehuMC;
                wms_chukudanviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                wms_chukudanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                wms_chukudanviewmodel.ChukuRQ = tempData.ChukuRQ;
                wms_chukudanviewmodel.YewuLX = tempData.YewuLX;
                wms_chukudanviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                wms_chukudanviewmodel.FuheSF = tempData.FuheSF;
                wms_chukudanviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                wms_chukudanviewmodel.JianguanSF = tempData.JianguanSF;
                wms_chukudanviewmodel.KehuDH = tempData.KehuDH;
                wms_chukudanviewmodel.KefuID = tempData.KefuID;
                wms_chukudanviewmodel.Lianxiren = tempData.Lianxiren;
                wms_chukudanviewmodel.LianxiDH = tempData.LianxiDH;
                wms_chukudanviewmodel.JihuaZT = tempData.JihuaZT;
                wms_chukudanviewmodel.ChukuTM = tempData.ChukuTM;
                wms_chukudanviewmodel.BaifangQY = tempData.BaifangQY;
                wms_chukudanviewmodel.Jianhuoren = tempData.Jianhuoren;
                wms_chukudanviewmodel.Beizhu = tempData.Beizhu;
                wms_chukudanviewmodel.Col1 = tempData.Col1;
                wms_chukudanviewmodel.Col2 = tempData.Col2;
                wms_chukudanviewmodel.Col3 = tempData.Col3;
                wms_chukudanviewmodel.MakeDate = tempData.MakeDate;
                wms_chukudanviewmodel.MakeMan = tempData.MakeMan;
                wms_chukudanviewmodel.ChukudanBH = tempData.ChukudanBH;
                wms_chukudanviewmodel.CKID = tempData.CKID;
                return View(wms_chukudanviewmodel);
            }
        }
        public JsonResult OutFinish()
        {
            var _ids = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ids))
                return Json(-1);
            string[] _efs = _ids.Split(',');
            foreach (var _ckid in _efs)
            {
                wms_chukudan _ckd = ob_wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
                if (_ckd != null)
                {
                    var _ckmx = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == _ckd.ID).ToList<wms_chukumx>();
                    var _ng = false;
                    if (_ckmx.Count == 0)
                        _ng = true;
                    foreach (wms_chukumx mx in _ckmx)
                    {
                        if (mx.ChukuSL != mx.JianhuoSL)
                        {
                            _ng = true;
                            break;
                        }
                    }
                    if (!_ng)
                    {
                        _ckd.JihuaZT = 5;
                        ob_wms_chukudanservice.UpdateEntity(_ckd);
                    }
                }
            }
            return Json(1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string fuhesf = Request["fuhesf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukutm = Request["chukutm"] ?? "";
            string baifangqy = Request["baifangqy"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string ckid = Request["ckid"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_chukudan p = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                p.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                p.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                p.KehuMC = kehumc.Trim();
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.FuheSF = fuhesf == "" ? false : Boolean.Parse(fuhesf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                p.ChukuTM = chukutm.Trim();
                p.BaifangQY = baifangqy.Trim();
                p.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ChukudanBH = chukudanbh.Trim();
                p.CKID = ckid == "" ? 0 : int.Parse(ckid);
                ob_wms_chukudanservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            wms_chukudan ob_wms_chukudan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_chukudan = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == id && wms_chukudan.IsDelete == false);
                    ob_wms_chukudan.IsDelete = true;
                    ob_wms_chukudanservice.UpdateEntity(ob_wms_chukudan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

