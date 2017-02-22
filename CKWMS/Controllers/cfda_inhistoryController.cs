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
    public class cfda_inhistoryController : Controller
    {
        private Icfda_inhistoryService ob_cfda_inhistoryservice = ServiceFactory.cfda_inhistoryservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_inhistory_index";
            Expression<Func<cfda_inhistory, bool>> where = PredicateExtensionses.True<cfda_inhistory>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "CPMC":
                            string CPMC = scld[1];
                            string CPMCequal = scld[2];
                            string CPMCand = scld[3];
                            if (!string.IsNullOrEmpty(CPMC))
                            {
                                if (CPMCequal.Equals("="))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.CPMC == CPMC.Trim());
                                    else
                                        where = where.Or(p => p.CPMC == CPMC.Trim());
                                }
                                if (CPMCequal.Equals("like"))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.CPMC.Contains(CPMC.Trim()));
                                    else
                                        where = where.Or(p => p.CPMC.Contains(CPMC.Trim()));
                                }
                            }
                            break;
                        case "GGXH":
                            string GGXH = scld[1];
                            string GGXHequal = scld[2];
                            string GGXHand = scld[3];
                            if (!string.IsNullOrEmpty(GGXH))
                            {
                                if (GGXHequal.Equals("="))
                                {
                                    if (GGXHand.Equals("and"))
                                        where = where.And(p => p.GGXH == GGXH.Trim());
                                    else
                                        where = where.Or(p => p.GGXH == GGXH.Trim());
                                }
                                if (GGXHequal.Equals("like"))
                                {
                                    if (GGXHand.Equals("and"))
                                        where = where.And(p => p.GGXH.Contains(GGXH.Trim()));
                                    else
                                        where = where.Or(p => p.GGXH.Contains(GGXH.Trim()));
                                }
                            }
                            break;
                        case "SCPH":
                            string SCPH = scld[1];
                            string SCPHequal = scld[2];
                            string SCPHand = scld[3];
                            if (!string.IsNullOrEmpty(SCPH))
                            {
                                if (SCPHequal.Equals("="))
                                {
                                    if (SCPHand.Equals("and"))
                                        where = where.And(p => p.SCPH == SCPH.Trim());
                                    else
                                        where = where.Or(p => p.SCPH == SCPH.Trim());
                                }
                                if (SCPHequal.Equals("like"))
                                {
                                    if (SCPHand.Equals("and"))
                                        where = where.And(p => p.SCPH.Contains(SCPH.Trim()));
                                    else
                                        where = where.Or(p => p.SCPH.Contains(SCPH.Trim()));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cfda_inhistory => cfda_inhistory.IsDelete == false);

            var tempData = ob_cfda_inhistoryservice.LoadSortEntities(where.Compile(), false, cfda_inhistory => cfda_inhistory.ID).ToPagedList<cfda_inhistory>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_inhistory = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_inhistory_index";
            string page = "1";
            //CPMC
            string CPMC = Request["CPMC"] ?? "";
            string CPMCequal = Request["CPMCequal"] ?? "";
            string CPMCand = Request["CPMCand"] ?? "";
            //GGXH
            string GGXH = Request["GGXH"] ?? "";
            string GGXHequal = Request["GGXHequal"] ?? "";
            string GGXHand = Request["GGXHand"] ?? "";
            //SCPH
            string SCPH = Request["SCPH"] ?? "";
            string SCPHequal = Request["SCPHequal"] ?? "";
            string SCPHand = Request["SCPHand"] ?? "";
            Expression<Func<cfda_inhistory, bool>> where = PredicateExtensionses.True<cfda_inhistory>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //CPMC
                if (!string.IsNullOrEmpty(CPMC))
                {
                    if (CPMCequal.Equals("="))
                    {
                        if (CPMCand.Equals("and"))
                            where = where.And(p => p.CPMC == CPMC.Trim());
                        else
                            where = where.Or(p => p.CPMC == CPMC.Trim());
                    }
                    if (CPMCequal.Equals("like"))
                    {
                        if (CPMCand.Equals("and"))
                            where = where.And(p => p.CPMC.Contains(CPMC.Trim()));
                        else
                            where = where.Or(p => p.CPMC.Contains(CPMC.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(CPMC))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CPMC", CPMC, CPMCequal, CPMCand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CPMC", "", CPMCequal, CPMCand);
                //GGXH
                if (!string.IsNullOrEmpty(GGXH))
                {
                    if (GGXHequal.Equals("="))
                    {
                        if (GGXHand.Equals("and"))
                            where = where.And(p => p.GGXH == GGXH.Trim());
                        else
                            where = where.Or(p => p.GGXH == GGXH.Trim());
                    }
                    if (GGXHequal.Equals("like"))
                    {
                        if (GGXHand.Equals("and"))
                            where = where.And(p => p.GGXH.Contains(GGXH.Trim()));
                        else
                            where = where.Or(p => p.GGXH.Contains(GGXH.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(GGXH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "GGXH", GGXH, GGXHequal, GGXHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "GGXH", "", GGXHequal, GGXHand);
                //SCPH
                if (!string.IsNullOrEmpty(SCPH))
                {
                    if (SCPHequal.Equals("="))
                    {
                        if (SCPHand.Equals("and"))
                            where = where.And(p => p.SCPH == SCPH.Trim());
                        else
                            where = where.Or(p => p.SCPH == SCPH.Trim());
                    }
                    if (SCPHequal.Equals("like"))
                    {
                        if (SCPHand.Equals("and"))
                            where = where.And(p => p.SCPH.Contains(SCPH.Trim()));
                        else
                            where = where.Or(p => p.SCPH.Contains(SCPH.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(SCPH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "SCPH", SCPH, SCPHequal, SCPHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "SCPH", "", SCPHequal, SCPHand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(CPMC))
                {
                    if (CPMCequal.Equals("="))
                    {
                        if (CPMCand.Equals("and"))
                            where = where.And(p => p.CPMC == CPMC.Trim());
                        else
                            where = where.Or(p => p.CPMC == CPMC.Trim());
                    }
                    if (CPMCequal.Equals("like"))
                    {
                        if (CPMCand.Equals("and"))
                            where = where.And(p => p.CPMC.Contains(CPMC.Trim()));
                        else
                            where = where.Or(p => p.CPMC.Contains(CPMC.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(CPMC))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CPMC", CPMC, CPMCequal, CPMCand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CPMC", "", CPMCequal, CPMCand);
                //GGXH
                if (!string.IsNullOrEmpty(GGXH))
                {
                    if (GGXHequal.Equals("="))
                    {
                        if (GGXHand.Equals("and"))
                            where = where.And(p => p.GGXH == GGXH.Trim());
                        else
                            where = where.Or(p => p.GGXH == GGXH.Trim());
                    }
                    if (GGXHequal.Equals("like"))
                    {
                        if (GGXHand.Equals("and"))
                            where = where.And(p => p.GGXH.Contains(GGXH.Trim()));
                        else
                            where = where.Or(p => p.GGXH.Contains(GGXH.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(GGXH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "GGXH", GGXH, GGXHequal, GGXHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "GGXH", "", GGXHequal, GGXHand);
                //SCPH
                if (!string.IsNullOrEmpty(SCPH))
                {
                    if (SCPHequal.Equals("="))
                    {
                        if (SCPHand.Equals("and"))
                            where = where.And(p => p.SCPH == SCPH.Trim());
                        else
                            where = where.Or(p => p.SCPH == SCPH.Trim());
                    }
                    if (SCPHequal.Equals("like"))
                    {
                        if (SCPHand.Equals("and"))
                            where = where.And(p => p.SCPH.Contains(SCPH.Trim()));
                        else
                            where = where.Or(p => p.SCPH.Contains(SCPH.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(SCPH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "SCPH", SCPH, SCPHequal, SCPHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "SCPH", "", SCPHequal, SCPHand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cfda_inhistory => cfda_inhistory.IsDelete == false);

            var tempData = ob_cfda_inhistoryservice.LoadSortEntities(where.Compile(), false, cfda_inhistory => cfda_inhistory.ID).ToPagedList<cfda_inhistory>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_inhistory = tempData;
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
            string xh = Request["xh"] ?? "";
            string wtfqymc = Request["wtfqymc"] ?? "";
            string rkrq = Request["rkrq"] ?? "";
            string rklx = Request["rklx"] ?? "";
            string cpmc = Request["cpmc"] ?? "";
            string ggxh = Request["ggxh"] ?? "";
            string scqy = Request["scqy"] ?? "";
            string scxkz = Request["scxkz"] ?? "";
            string scph = Request["scph"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string sl = Request["sl"] ?? "";
            string dw = Request["dw"] ?? "";
            string cytj = Request["cytj"] ?? "";
            string hw = Request["hw"] ?? "";
            string zlzt = Request["zlzt"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                cfda_inhistory ob_cfda_inhistory = new cfda_inhistory();
                ob_cfda_inhistory.XH = xh.Trim();
                ob_cfda_inhistory.WTFQYMC = wtfqymc.Trim();
                ob_cfda_inhistory.RKRQ = rkrq.Trim();
                ob_cfda_inhistory.RKLX = rklx.Trim();
                ob_cfda_inhistory.CPMC = cpmc.Trim();
                ob_cfda_inhistory.GGXH = ggxh.Trim();
                ob_cfda_inhistory.SCQY = scqy.Trim();
                ob_cfda_inhistory.SCXKZ = scxkz.Trim();
                ob_cfda_inhistory.SCPH = scph.Trim();
                ob_cfda_inhistory.YXQ = yxq.Trim();
                ob_cfda_inhistory.SL = sl.Trim();
                ob_cfda_inhistory.DW = dw.Trim();
                ob_cfda_inhistory.CYTJ = cytj.Trim();
                ob_cfda_inhistory.HW = hw.Trim();
                ob_cfda_inhistory.ZLZT = zlzt.Trim();
                ob_cfda_inhistory.BZ = bz.Trim();
                ob_cfda_inhistory.Col1 = col1.Trim();
                ob_cfda_inhistory.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cfda_inhistory.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cfda_inhistory = ob_cfda_inhistoryservice.AddEntity(ob_cfda_inhistory);
                ViewBag.cfda_inhistory = ob_cfda_inhistory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            cfda_inhistory tempData = ob_cfda_inhistoryservice.GetEntityById(cfda_inhistory => cfda_inhistory.ID == id && cfda_inhistory.IsDelete == false);
            ViewBag.cfda_inhistory = tempData;
            if (tempData == null)
                return View();
            else
            {
                cfda_inhistoryViewModel cfda_inhistoryviewmodel = new cfda_inhistoryViewModel();
                cfda_inhistoryviewmodel.ID = tempData.ID;
                cfda_inhistoryviewmodel.XH = tempData.XH;
                cfda_inhistoryviewmodel.WTFQYMC = tempData.WTFQYMC;
                cfda_inhistoryviewmodel.RKRQ = tempData.RKRQ;
                cfda_inhistoryviewmodel.RKLX = tempData.RKLX;
                cfda_inhistoryviewmodel.CPMC = tempData.CPMC;
                cfda_inhistoryviewmodel.GGXH = tempData.GGXH;
                cfda_inhistoryviewmodel.SCQY = tempData.SCQY;
                cfda_inhistoryviewmodel.SCXKZ = tempData.SCXKZ;
                cfda_inhistoryviewmodel.SCPH = tempData.SCPH;
                cfda_inhistoryviewmodel.YXQ = tempData.YXQ;
                cfda_inhistoryviewmodel.SL = tempData.SL;
                cfda_inhistoryviewmodel.DW = tempData.DW;
                cfda_inhistoryviewmodel.CYTJ = tempData.CYTJ;
                cfda_inhistoryviewmodel.HW = tempData.HW;
                cfda_inhistoryviewmodel.ZLZT = tempData.ZLZT;
                cfda_inhistoryviewmodel.BZ = tempData.BZ;
                cfda_inhistoryviewmodel.Col1 = tempData.Col1;
                cfda_inhistoryviewmodel.MakeDate = tempData.MakeDate;
                cfda_inhistoryviewmodel.MakeMan = tempData.MakeMan;
                return View(cfda_inhistoryviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xh = Request["xh"] ?? "";
            string wtfqymc = Request["wtfqymc"] ?? "";
            string rkrq = Request["rkrq"] ?? "";
            string rklx = Request["rklx"] ?? "";
            string cpmc = Request["cpmc"] ?? "";
            string ggxh = Request["ggxh"] ?? "";
            string scqy = Request["scqy"] ?? "";
            string scxkz = Request["scxkz"] ?? "";
            string scph = Request["scph"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string sl = Request["sl"] ?? "";
            string dw = Request["dw"] ?? "";
            string cytj = Request["cytj"] ?? "";
            string hw = Request["hw"] ?? "";
            string zlzt = Request["zlzt"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                cfda_inhistory p = ob_cfda_inhistoryservice.GetEntityById(cfda_inhistory => cfda_inhistory.ID == uid);
                p.XH = xh.Trim();
                p.WTFQYMC = wtfqymc.Trim();
                p.RKRQ = rkrq.Trim();
                p.RKLX = rklx.Trim();
                p.CPMC = cpmc.Trim();
                p.GGXH = ggxh.Trim();
                p.SCQY = scqy.Trim();
                p.SCXKZ = scxkz.Trim();
                p.SCPH = scph.Trim();
                p.YXQ = yxq.Trim();
                p.SL = sl.Trim();
                p.DW = dw.Trim();
                p.CYTJ = cytj.Trim();
                p.HW = hw.Trim();
                p.ZLZT = zlzt.Trim();
                p.BZ = bz.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cfda_inhistoryservice.UpdateEntity(p);
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
            cfda_inhistory ob_cfda_inhistory;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cfda_inhistory = ob_cfda_inhistoryservice.GetEntityById(cfda_inhistory => cfda_inhistory.ID == id && cfda_inhistory.IsDelete == false);
                    ob_cfda_inhistory.IsDelete = true;
                    ob_cfda_inhistoryservice.UpdateEntity(ob_cfda_inhistory);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult inHistoryExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_inhistory_index";
            Expression<Func<cfda_inhistory, bool>> where = PredicateExtensionses.True<cfda_inhistory>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "CPMC":
                            string CPMC = scld[1];
                            string CPMCequal = scld[2];
                            string CPMCand = scld[3];
                            if (!string.IsNullOrEmpty(CPMC))
                            {
                                if (CPMCequal.Equals("="))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.CPMC == CPMC.Trim());
                                    else
                                        where = where.Or(p => p.CPMC == CPMC.Trim());
                                }
                                if (CPMCequal.Equals("like"))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.CPMC.Contains(CPMC.Trim()));
                                    else
                                        where = where.Or(p => p.CPMC.Contains(CPMC.Trim()));
                                }
                            }
                            break;
                        case "GGXH":
                            string GGXH = scld[1];
                            string GGXHequal = scld[2];
                            string GGXHand = scld[3];
                            if (!string.IsNullOrEmpty(GGXH))
                            {
                                if (GGXHequal.Equals("="))
                                {
                                    if (GGXHand.Equals("and"))
                                        where = where.And(p => p.GGXH == GGXH.Trim());
                                    else
                                        where = where.Or(p => p.GGXH == GGXH.Trim());
                                }
                                if (GGXHequal.Equals("like"))
                                {
                                    if (GGXHand.Equals("and"))
                                        where = where.And(p => p.GGXH.Contains(GGXH.Trim()));
                                    else
                                        where = where.Or(p => p.GGXH.Contains(GGXH.Trim()));
                                }
                            }
                            break;
                        case "SCPH":
                            string SCPH = scld[1];
                            string SCPHequal = scld[2];
                            string SCPHand = scld[3];
                            if (!string.IsNullOrEmpty(SCPH))
                            {
                                if (SCPHequal.Equals("="))
                                {
                                    if (SCPHand.Equals("and"))
                                        where = where.And(p => p.SCPH == SCPH.Trim());
                                    else
                                        where = where.Or(p => p.SCPH == SCPH.Trim());
                                }
                                if (SCPHequal.Equals("like"))
                                {
                                    if (SCPHand.Equals("and"))
                                        where = where.And(p => p.SCPH.Contains(SCPH.Trim()));
                                    else
                                        where = where.Or(p => p.SCPH.Contains(SCPH.Trim()));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cfda_inhistory => cfda_inhistory.IsDelete == false);

            var tempData = ob_cfda_inhistoryservice.LoadSortEntities(where.Compile(), false, cfda_inhistory => cfda_inhistory.ID);
            ViewBag.cfda_inhistory = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "inHistoryExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("inHistoryExport_{0}.xls", DateTime.Now.ToShortDateString()));
        }
    }
}

