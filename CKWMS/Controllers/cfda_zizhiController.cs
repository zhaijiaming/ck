using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using System.Linq.Expressions;

namespace CKWMS.Controllers
{
    public class cfda_zizhiController : Controller
    {
        // GET: cfda_zizhi
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(Duration = 30)]
        public ActionResult CustomerCheckList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_zizhi_customer";
            Expression<Func<base_weituokehu, bool>> where = PredicateExtensionses.True<base_weituokehu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "kehumingcheng":
                            string kehumingcheng = scld[1];
                            string kehumingchengequal = scld[2];
                            string kehumingchengand = scld[3];
                            if (!string.IsNullOrEmpty(kehumingcheng))
                            {
                                if (kehumingchengequal.Equals("="))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng == kehumingcheng);
                                    else
                                        where = where.Or(p => p.Kehumingcheng == kehumingcheng);
                                }
                                if (kehumingchengequal.Equals("like"))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng.Contains(kehumingcheng));
                                    else
                                        where = where.Or(p => p.Kehumingcheng.Contains(kehumingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_weituokehu => base_weituokehu.IsDelete == false && base_weituokehu.Shouying > 4 && base_weituokehu.ShenchaSF == true);
            var tempData =ServiceFactory.base_weituokehuservice.LoadSortEntities(where.Compile(), false, base_weituokehu => base_weituokehu.ID).ToPagedList<base_weituokehu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerCheckList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_zizhi_customer";
            string page = "1";
            //kehumingcheng
            string kehumingcheng = Request["kehumingcheng"] ?? "";
            string kehumingchengequal = Request["kehumingchengequal"] ?? "";
            string kehumingchengand = Request["kehumingchengand"] ?? "";
            Expression<Func<base_weituokehu, bool>> where = PredicateExtensionses.True<base_weituokehu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //kehumingcheng
                if (!string.IsNullOrEmpty(kehumingcheng))
                {
                    if (kehumingchengequal.Equals("="))
                    {
                        if (kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng == kehumingcheng);
                        else
                            where = where.Or(p => p.Kehumingcheng == kehumingcheng);
                    }
                    if (kehumingchengequal.Equals("like"))
                    {
                        if (kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng.Contains(kehumingcheng));
                        else
                            where = where.Or(p => p.Kehumingcheng.Contains(kehumingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(kehumingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumingcheng", kehumingcheng, kehumingchengequal, kehumingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumingcheng", "", kehumingchengequal, kehumingchengand);
            }
            else
            {
                sc.ConditionInfo = "";
                //kehumingcheng
                if (!string.IsNullOrEmpty(kehumingcheng))
                {
                    if (kehumingchengequal.Equals("="))
                    {
                        if (kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng == kehumingcheng);
                        else
                            where = where.Or(p => p.Kehumingcheng == kehumingcheng);
                    }
                    if (kehumingchengequal.Equals("like"))
                    {
                        if (kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng.Contains(kehumingcheng));
                        else
                            where = where.Or(p => p.Kehumingcheng.Contains(kehumingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(kehumingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumingcheng", kehumingcheng, kehumingchengequal, kehumingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumingcheng", "", kehumingchengequal, kehumingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_weituokehu => base_weituokehu.IsDelete == false && base_weituokehu.Shouying>4 && base_weituokehu.ShenchaSF==true);
            var tempData = ServiceFactory.base_weituokehuservice.LoadSortEntities(where.Compile(), false, base_weituokehu => base_weituokehu.ID).ToPagedList<base_weituokehu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CargoCheckList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_zizhi_cargochecklist";
            string page = "1";
            var custid = Request["customer_id"] ?? "";
            if (custid.Length == 0)
                custid = "0";
            Expression<Func<cfda_cargos_v, bool>> where = PredicateExtensionses.True<cfda_cargos_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            //Kehumingcheng
            string Kehumingcheng = Request["Kehumingcheng"] ?? "";
            string Kehumingchengequal = Request["Kehumingchengequal"] ?? "";
            string Kehumingchengand = Request["Kehumingchengand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            //guige
            string guige = Request["guige"] ?? "";
            string guigeequal = Request["guigeequal"] ?? "";
            string guigeand = Request["guigeand"] ?? "";
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //Kehumingcheng
                if (!string.IsNullOrEmpty(Kehumingcheng))
                {
                    if (Kehumingchengequal.Equals("="))
                    {
                        if (Kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng == Kehumingcheng);
                        else
                            where = where.Or(p => p.Kehumingcheng == Kehumingcheng);
                    }
                    if (Kehumingchengequal.Equals("like"))
                    {
                        if (Kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng.Contains(Kehumingcheng));
                        else
                            where = where.Or(p => p.Kehumingcheng.Contains(Kehumingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(Kehumingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kehumingcheng", Kehumingcheng, Kehumingchengequal, Kehumingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kehumingcheng", "", Kehumingchengequal, Kehumingchengand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng == mingcheng);
                        else
                            where = where.Or(p => p.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.guige == guige);
                        else
                            where = where.Or(p => p.guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.guige.Contains(guige));
                        else
                            where = where.Or(p => p.guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //Kehumingcheng
                if (!string.IsNullOrEmpty(Kehumingcheng))
                {
                    if (Kehumingchengequal.Equals("="))
                    {
                        if (Kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng == Kehumingcheng);
                        else
                            where = where.Or(p => p.Kehumingcheng == Kehumingcheng);
                    }
                    if (Kehumingchengequal.Equals("like"))
                    {
                        if (Kehumingchengand.Equals("and"))
                            where = where.And(p => p.Kehumingcheng.Contains(Kehumingcheng));
                        else
                            where = where.Or(p => p.Kehumingcheng.Contains(Kehumingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(Kehumingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kehumingcheng", Kehumingcheng, Kehumingchengequal, Kehumingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kehumingcheng", "", Kehumingchengequal, Kehumingchengand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng == mingcheng);
                        else
                            where = where.Or(p => p.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.guige == guige);
                        else
                            where = where.Or(p => p.guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.guige.Contains(guige));
                        else
                            where = where.Or(p => p.guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.base_shangpinxxservice.CFDALoadCargos(int.Parse(custid)).Where(where.Compile()).ToPagedList<cfda_cargos_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_cargo = tempData;
            return View(tempData);
        }
        public ActionResult CargoExport()
        {
            int userid = (int)Session["user_id"];
            var custid = Request["customer_id"] ?? "";
            if (custid.Length == 0)
                custid = "0";
            string pagetag = "cfda_zizhi_cargochecklist";
            Expression<Func<cfda_cargos_v, bool>> where = PredicateExtensionses.True<cfda_cargos_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "kehumingcheng":
                            string kehumingcheng = scld[1];
                            string kehumingchengequal = scld[2];
                            string kehumingchengand = scld[3];
                            if (!string.IsNullOrEmpty(kehumingcheng))
                            {
                                if (kehumingchengequal.Equals("="))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng == kehumingcheng);
                                    else
                                        where = where.Or(p => p.Kehumingcheng == kehumingcheng);
                                }
                                if (kehumingchengequal.Equals("like"))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng.Contains(kehumingcheng));
                                    else
                                        where = where.Or(p => p.Kehumingcheng.Contains(kehumingcheng));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(p => p.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.guige == guige);
                                    else
                                        where = where.Or(p => p.guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.guige.Contains(guige));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ServiceFactory.base_shangpinxxservice.CFDALoadCargos(int.Parse(custid)).Where(where.Compile());
            ViewBag.cfda_cargo = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "CargoExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("cfda_cargos_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        [OutputCache(Duration = 30)]
        public ActionResult CargoCheckList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var custid = Request["customer_id"] ?? "";
            if (custid.Length == 0)
                custid = "0";
            string pagetag = "cfda_zizhi_cargochecklist";
            Expression<Func<cfda_cargos_v, bool>> where = PredicateExtensionses.True<cfda_cargos_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "kehumingcheng":
                            string kehumingcheng = scld[1];
                            string kehumingchengequal = scld[2];
                            string kehumingchengand = scld[3];
                            if (!string.IsNullOrEmpty(kehumingcheng))
                            {
                                if (kehumingchengequal.Equals("="))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng== kehumingcheng);
                                    else
                                        where = where.Or(p => p.Kehumingcheng == kehumingcheng);
                                }
                                if (kehumingchengequal.Equals("like"))
                                {
                                    if (kehumingchengand.Equals("and"))
                                        where = where.And(p => p.Kehumingcheng.Contains(kehumingcheng));
                                    else
                                        where = where.Or(p => p.Kehumingcheng.Contains(kehumingcheng));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(p => p.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.guige == guige);
                                    else
                                        where = where.Or(p => p.guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.guige.Contains(guige));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData =ServiceFactory.base_shangpinxxservice.CFDALoadCargos(int.Parse(custid)).Where(where.Compile()).ToPagedList<cfda_cargos_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cfda_cargo = tempData;
            return View(tempData);
        }
    }
}