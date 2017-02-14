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
    public class cfda_outhistoryController : Controller
    {
        private Icfda_outhistoryService ob_cfda_outhistoryservice = ServiceFactory.cfda_outhistoryservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_outhistory_index";
            Expression<Func<cfda_outhistory, bool>> where = PredicateExtensionses.True<cfda_outhistory>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "xh":
                            string xh = scld[1];
                            string xhequal = scld[2];
                            string xhand = scld[3];
                            if (!string.IsNullOrEmpty(xh))
                            {
                                if (xhequal.Equals("="))
                                {
                                    if (xhand.Equals("and"))
                                        where = where.And(cfda_outhistory => cfda_outhistory.XH == xh);
                                    else
                                        where = where.Or(cfda_outhistory => cfda_outhistory.XH == xh);
                                }
                                if (xhequal.Equals("like"))
                                {
                                    if (xhand.Equals("and"))
                                        where = where.And(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                                    else
                                        where = where.Or(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cfda_outhistory => cfda_outhistory.IsDelete == false);

            var tempData = ob_cfda_outhistoryservice.LoadSortEntities(where.Compile(), false, cfda_outhistory => cfda_outhistory.ID).ToPagedList<cfda_outhistory>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_outhistory = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_outhistory_index";
            string page = "1";
            string xh = Request["xh"] ?? "";
            string xhequal = Request["xhequal"] ?? "";
            string xhand = Request["xhand"] ?? "";
            Expression<Func<cfda_outhistory, bool>> where = PredicateExtensionses.True<cfda_outhistory>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(xh))
                {
                    if (xhequal.Equals("="))
                    {
                        if (xhand.Equals("and"))
                            where = where.And(cfda_outhistory => cfda_outhistory.XH == xh);
                        else
                            where = where.Or(cfda_outhistory => cfda_outhistory.XH == xh);
                    }
                    if (xhequal.Equals("like"))
                    {
                        if (xhand.Equals("and"))
                            where = where.And(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                        else
                            where = where.Or(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                    }
                }
                if (!string.IsNullOrEmpty(xh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xh", xh, xhequal, xhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xh", "", xhequal, xhand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(xh))
                {
                    if (xhequal.Equals("="))
                    {
                        if (xhand.Equals("and"))
                            where = where.And(cfda_outhistory => cfda_outhistory.XH == xh);
                        else
                            where = where.Or(cfda_outhistory => cfda_outhistory.XH == xh);
                    }
                    if (xhequal.Equals("like"))
                    {
                        if (xhand.Equals("and"))
                            where = where.And(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                        else
                            where = where.Or(cfda_outhistory => cfda_outhistory.XH.Contains(xh));
                    }
                }
                if (!string.IsNullOrEmpty(xh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xh", xh, xhequal, xhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xh", "", xhequal, xhand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cfda_outhistory => cfda_outhistory.IsDelete == false);

            var tempData = ob_cfda_outhistoryservice.LoadSortEntities(where.Compile(), false, cfda_outhistory => cfda_outhistory.ID).ToPagedList<cfda_outhistory>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_outhistory = tempData;
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
            string ckrq = Request["ckrq"] ?? "";
            string cklx = Request["cklx"] ?? "";
            string hpmc = Request["hpmc"] ?? "";
            string ggxh = Request["ggxh"] ?? "";
            string scqy = Request["scqy"] ?? "";
            string cpzcz = Request["cpzcz"] ?? "";
            string scph = Request["scph"] ?? "";
            string cytj = Request["cytj"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string dw = Request["dw"] ?? "";
            string sl = Request["sl"] ?? "";
            string shkhmc = Request["shkhmc"] ?? "";
            string shdz = Request["shdz"] ?? "";
            string lxr = Request["lxr"] ?? "";
            string lxfs = Request["lxfs"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                cfda_outhistory ob_cfda_outhistory = new cfda_outhistory();
                ob_cfda_outhistory.XH = xh.Trim();
                ob_cfda_outhistory.WTFQYMC = wtfqymc.Trim();
                ob_cfda_outhistory.CKRQ = ckrq.Trim();
                ob_cfda_outhistory.CKLX = cklx.Trim();
                ob_cfda_outhistory.HPMC = hpmc.Trim();
                ob_cfda_outhistory.GGXH = ggxh.Trim();
                ob_cfda_outhistory.SCQY = scqy.Trim();
                ob_cfda_outhistory.CPZCZ = cpzcz.Trim();
                ob_cfda_outhistory.SCPH = scph.Trim();
                ob_cfda_outhistory.CYTJ = cytj.Trim();
                ob_cfda_outhistory.YXQ = yxq.Trim();
                ob_cfda_outhistory.DW = dw.Trim();
                ob_cfda_outhistory.SL = sl.Trim();
                ob_cfda_outhistory.SHKHMC = shkhmc.Trim();
                ob_cfda_outhistory.SHDZ = shdz.Trim();
                ob_cfda_outhistory.LXR = lxr.Trim();
                ob_cfda_outhistory.LXFS = lxfs.Trim();
                ob_cfda_outhistory.BZ = bz.Trim();
                ob_cfda_outhistory.Col1 = col1.Trim();
                ob_cfda_outhistory.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cfda_outhistory.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cfda_outhistory = ob_cfda_outhistoryservice.AddEntity(ob_cfda_outhistory);
                ViewBag.cfda_outhistory = ob_cfda_outhistory;
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
            cfda_outhistory tempData = ob_cfda_outhistoryservice.GetEntityById(cfda_outhistory => cfda_outhistory.ID == id && cfda_outhistory.IsDelete == false);
            ViewBag.cfda_outhistory = tempData;
            if (tempData == null)
                return View();
            else
            {
                cfda_outhistoryViewModel cfda_outhistoryviewmodel = new cfda_outhistoryViewModel();
                cfda_outhistoryviewmodel.ID = tempData.ID;
                cfda_outhistoryviewmodel.XH = tempData.XH;
                cfda_outhistoryviewmodel.WTFQYMC = tempData.WTFQYMC;
                cfda_outhistoryviewmodel.CKRQ = tempData.CKRQ;
                cfda_outhistoryviewmodel.CKLX = tempData.CKLX;
                cfda_outhistoryviewmodel.HPMC = tempData.HPMC;
                cfda_outhistoryviewmodel.GGXH = tempData.GGXH;
                cfda_outhistoryviewmodel.SCQY = tempData.SCQY;
                cfda_outhistoryviewmodel.CPZCZ = tempData.CPZCZ;
                cfda_outhistoryviewmodel.SCPH = tempData.SCPH;
                cfda_outhistoryviewmodel.CYTJ = tempData.CYTJ;
                cfda_outhistoryviewmodel.YXQ = tempData.YXQ;
                cfda_outhistoryviewmodel.DW = tempData.DW;
                cfda_outhistoryviewmodel.SL = tempData.SL;
                cfda_outhistoryviewmodel.SHKHMC = tempData.SHKHMC;
                cfda_outhistoryviewmodel.SHDZ = tempData.SHDZ;
                cfda_outhistoryviewmodel.LXR = tempData.LXR;
                cfda_outhistoryviewmodel.LXFS = tempData.LXFS;
                cfda_outhistoryviewmodel.BZ = tempData.BZ;
                cfda_outhistoryviewmodel.Col1 = tempData.Col1;
                cfda_outhistoryviewmodel.MakeDate = tempData.MakeDate;
                cfda_outhistoryviewmodel.MakeMan = tempData.MakeMan;
                return View(cfda_outhistoryviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xh = Request["xh"] ?? "";
            string wtfqymc = Request["wtfqymc"] ?? "";
            string ckrq = Request["ckrq"] ?? "";
            string cklx = Request["cklx"] ?? "";
            string hpmc = Request["hpmc"] ?? "";
            string ggxh = Request["ggxh"] ?? "";
            string scqy = Request["scqy"] ?? "";
            string cpzcz = Request["cpzcz"] ?? "";
            string scph = Request["scph"] ?? "";
            string cytj = Request["cytj"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string dw = Request["dw"] ?? "";
            string sl = Request["sl"] ?? "";
            string shkhmc = Request["shkhmc"] ?? "";
            string shdz = Request["shdz"] ?? "";
            string lxr = Request["lxr"] ?? "";
            string lxfs = Request["lxfs"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                cfda_outhistory p = ob_cfda_outhistoryservice.GetEntityById(cfda_outhistory => cfda_outhistory.ID == uid);
                p.XH = xh.Trim();
                p.WTFQYMC = wtfqymc.Trim();
                p.CKRQ = ckrq.Trim();
                p.CKLX = cklx.Trim();
                p.HPMC = hpmc.Trim();
                p.GGXH = ggxh.Trim();
                p.SCQY = scqy.Trim();
                p.CPZCZ = cpzcz.Trim();
                p.SCPH = scph.Trim();
                p.CYTJ = cytj.Trim();
                p.YXQ = yxq.Trim();
                p.DW = dw.Trim();
                p.SL = sl.Trim();
                p.SHKHMC = shkhmc.Trim();
                p.SHDZ = shdz.Trim();
                p.LXR = lxr.Trim();
                p.LXFS = lxfs.Trim();
                p.BZ = bz.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cfda_outhistoryservice.UpdateEntity(p);
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
            cfda_outhistory ob_cfda_outhistory;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cfda_outhistory = ob_cfda_outhistoryservice.GetEntityById(cfda_outhistory => cfda_outhistory.ID == id && cfda_outhistory.IsDelete == false);
                    ob_cfda_outhistory.IsDelete = true;
                    ob_cfda_outhistoryservice.UpdateEntity(ob_cfda_outhistory);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

