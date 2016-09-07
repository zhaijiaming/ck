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
                        case "Mingcheng":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
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
            //daima
            string daima = Request["Mingcheng"] ?? "";
            string daimaequal = Request["Mingchengqual"] ?? "";
            string daimaand = Request["Mingchengand"] ?? "";

            Expression<Func<base_weituokehu, bool>> where = PredicateExtensionses.True<base_weituokehu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", daimaequal, daimaand);
            }
            else
            {
                sc.ConditionInfo = "";
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Kehumingcheng.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", daimaequal, daimaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_weituokehu => base_weituokehu.IsDelete == false && base_weituokehu.Shouying>4 && base_weituokehu.ShenchaSF==true);
            var tempData = ServiceFactory.base_weituokehuservice.LoadSortEntities(where.Compile(), false, base_weituokehu => base_weituokehu.ID).ToPagedList<base_weituokehu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            return View(tempData);
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
                        case "bianhao":
                            string bianhao = scld[1];
                            string bianhaoequal = scld[2];
                            string bianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(bianhao))
                            {
                                if (bianhaoequal.Equals("="))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(cfda_cargos_v => cfda_cargos_v.Kehumingcheng== bianhao);
                                    else
                                        where = where.Or(cfda_cargos_v => cfda_cargos_v.Kehumingcheng ==bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(cfda_cargos_v => cfda_cargos_v.Kehumingcheng.Contains(bianhao));
                                    else
                                        where = where.Or(cfda_cargos_v => cfda_cargos_v.Kehumingcheng.Contains(bianhao));
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