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
using System.Text;

namespace CKWMS.Controllers
{
    public class wms_rukudanController : Controller
    {
        private Iwms_rukudanService ob_wms_rukudanservice = ServiceFactory.wms_rukudanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_index";
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
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
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_index";
            string page = "1";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
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
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
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
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }
        public ActionResult OperateList()
        {
            int userid = (int)Session["user_id"];

            var tempData = ob_wms_rukudanservice.LoadSortEntities(p => p.IsDelete == false && p.RukuZT < 5, false, s => s.MakeDate);
            ViewBag.wms_rukudan = tempData;
            return View();
        }
        public JsonResult EntryFinish()
        {
            var _ids = Request["rk"] ?? "";
            if (string.IsNullOrEmpty(_ids))
                return Json(-1);
            string[] _efs = _ids.Split(',');
            foreach (var _rkid in _efs)
            {
                wms_rukudan _rkd = ob_wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkid));
                if (_rkd != null)
                {
                    _rkd.RukuZT = 5;
                    ob_wms_rukudanservice.UpdateEntity(_rkd);
                }
            }
            return Json(1);
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
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string yanshousf = Request["yanshousf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string rukutm = Request["rukutm"] ?? "";
            string rukuzt = Request["rukuzt"] ?? "";
            string duifangqy = Request["duifangqy"] ?? "";
            string shouhuoren = Request["shouhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string rukudanbh = Request["rukudanbh"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            string zhijiesh = Request["zhijiesh"] ?? "";
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            if (yanshousf.IndexOf("true") >= 0)
                yanshousf = "true";
            if (zhijiesh.IndexOf("true") >= 0)
                zhijiesh = "true";
            try
            {
                wms_rukudan ob_wms_rukudan = new wms_rukudan();
                ob_wms_rukudan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_wms_rukudan.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_wms_rukudan.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                ob_wms_rukudan.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                ob_wms_rukudan.Fahuodizhi = fahuodizhi.Trim();
                ob_wms_rukudan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_wms_rukudan.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                ob_wms_rukudan.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_wms_rukudan.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_wms_rukudan.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_wms_rukudan.YanshouSF = yanshousf == "" ? false : Boolean.Parse(yanshousf);
                ob_wms_rukudan.ChunyunYQ = chunyunyq.Trim();
                ob_wms_rukudan.KehuDH = kehudh.Trim();
                ob_wms_rukudan.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_wms_rukudan.Lianxiren = lianxiren.Trim();
                ob_wms_rukudan.LianxiDH = lianxidh.Trim();
                ob_wms_rukudan.RukuTM = rukutm.Trim();
                ob_wms_rukudan.RukuZT = rukuzt == "" ? 0 : int.Parse(rukuzt);
                ob_wms_rukudan.DuifangQY = duifangqy.Trim();
                ob_wms_rukudan.Shouhuoren = shouhuoren == "" ? 0 : int.Parse(shouhuoren);
                ob_wms_rukudan.Beizhu = beizhu.Trim();
                ob_wms_rukudan.Col1 = col1.Trim();
                ob_wms_rukudan.Col2 = col2.Trim();
                ob_wms_rukudan.Col3 = col3.Trim();
                ob_wms_rukudan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_rukudan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_rukudan.RukudanBH = rukudanbh.Trim();
                ob_wms_rukudan.ZhijieSH = zhijiesh == "" ? false : Boolean.Parse(zhijiesh);
                ob_wms_rukudan.CangkuID=cangkuid ==""?0:int.Parse(cangkuid);
                ob_wms_rukudan = ob_wms_rukudanservice.AddEntity(ob_wms_rukudan);
                id = ob_wms_rukudan.ID.ToString();
                ViewBag.wms_rukudan = ob_wms_rukudan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OperateList");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_rukudan tempData = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == id && wms_rukudan.IsDelete == false);
            ViewBag.wms_rukudan = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_rukudanViewModel wms_rukudanviewmodel = new wms_rukudanViewModel();
                wms_rukudanviewmodel.ID = tempData.ID;
                wms_rukudanviewmodel.HuozhuID = tempData.HuozhuID;
                wms_rukudanviewmodel.JihuaID = tempData.JihuaID;
                wms_rukudanviewmodel.XinxiLY = tempData.XinxiLY;
                wms_rukudanviewmodel.GongyingshangID = tempData.GongyingshangID;
                wms_rukudanviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                wms_rukudanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                wms_rukudanviewmodel.RukuRQ = tempData.RukuRQ;
                wms_rukudanviewmodel.YewuLX = tempData.YewuLX;
                wms_rukudanviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                wms_rukudanviewmodel.JianguanSF = tempData.JianguanSF;
                wms_rukudanviewmodel.YanshouSF = tempData.YanshouSF;
                wms_rukudanviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                wms_rukudanviewmodel.KehuDH = tempData.KehuDH;
                wms_rukudanviewmodel.KefuID = tempData.KefuID;
                wms_rukudanviewmodel.Lianxiren = tempData.Lianxiren;
                wms_rukudanviewmodel.LianxiDH = tempData.LianxiDH;
                wms_rukudanviewmodel.RukuTM = tempData.RukuTM;
                wms_rukudanviewmodel.RukuZT = tempData.RukuZT;
                wms_rukudanviewmodel.DuifangQY = tempData.DuifangQY;
                wms_rukudanviewmodel.Shouhuoren = tempData.Shouhuoren;
                wms_rukudanviewmodel.Beizhu = tempData.Beizhu;
                wms_rukudanviewmodel.Col1 = tempData.Col1;
                wms_rukudanviewmodel.Col2 = tempData.Col2;
                wms_rukudanviewmodel.Col3 = tempData.Col3;
                wms_rukudanviewmodel.MakeDate = tempData.MakeDate;
                wms_rukudanviewmodel.MakeMan = tempData.MakeMan;
                wms_rukudanviewmodel.RukudanBH = tempData.RukudanBH;
                wms_rukudanviewmodel.ZhijieSH = tempData.ZhijieSH;
                wms_rukudanviewmodel.CangkuID = tempData.CangkuID;
                return View(wms_rukudanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string yanshousf = Request["yanshousf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string rukutm = Request["rukutm"] ?? "";
            string rukuzt = Request["rukuzt"] ?? "";
            string duifangqy = Request["duifangqy"] ?? "";
            string shouhuoren = Request["shouhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string rukudanbh = Request["rukudanbh"] ?? "";
            string zhijiesh = Request["zhijiesh"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            int uid = int.Parse(id);
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            if (yanshousf.IndexOf("true") >= 0)
                yanshousf = "true";
            if (zhijiesh.IndexOf("true") >= 0)
                zhijiesh = "true";
            try
            {
                wms_rukudan p = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                p.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                p.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.YanshouSF = yanshousf == "" ? false : Boolean.Parse(yanshousf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.RukuTM = rukutm.Trim();
                p.RukuZT = rukuzt == "" ? 0 : int.Parse(rukuzt);
                p.DuifangQY = duifangqy.Trim();
                p.Shouhuoren = shouhuoren == "" ? 0 : int.Parse(shouhuoren);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.RukudanBH = rukudanbh.Trim();
                p.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                p.ZhijieSH= zhijiesh == "" ? false : Boolean.Parse(zhijiesh);
                ob_wms_rukudanservice.UpdateEntity(p);
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
            wms_rukudan ob_wms_rukudan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_rukudan = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == id && wms_rukudan.IsDelete == false);
                    ob_wms_rukudan.IsDelete = true;
                    ob_wms_rukudanservice.UpdateEntity(ob_wms_rukudan);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Export()
        {
            var resultList = ob_wms_rukudanservice.LoadEntities(p => p.IsDelete == false);
            ViewBag.NoPaging = true;
            ViewBag.wms_rukudan = resultList;
            ViewData.Model = resultList;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "Export");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("rdk_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult PrintRuKuXiangDan(){
            var rkxdid = Request["rkxdid"] ?? "";
            ViewBag.rkxdid = rkxdid;
            return View();
        }
    }
}
