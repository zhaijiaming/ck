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
    public class base_shengchanqiyeController : Controller
    {
        private Ibase_shengchanqiyeService ob_base_shengchanqiyeservice = ServiceFactory.base_shengchanqiyeservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shengchanqiye_index";
            Expression<Func<base_shengchanqiye, bool>> where = PredicateExtensionses.True<base_shengchanqiye>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shengchanqiye => base_shengchanqiye.IsDelete == false);

            var tempData = ob_base_shengchanqiyeservice.LoadSortEntities(where.Compile(), false, base_shengchanqiye => base_shengchanqiye.ID).ToPagedList<base_shengchanqiye>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shengchanqiye = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shengchanqiye_index";
            string page = "1";
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            Expression<Func<base_shengchanqiye, bool>> where = PredicateExtensionses.True<base_shengchanqiye>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shengchanqiye => base_shengchanqiye.IsDelete == false);

            var tempData = ob_base_shengchanqiyeservice.LoadSortEntities(where.Compile(), false, base_shengchanqiye => base_shengchanqiye.ID).ToPagedList<base_shengchanqiye>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shengchanqiye = tempData;
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
            string daima = Request["daima"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string shengchanxukebh = Request["shengchanxukebh"] ?? "";
            string shengchanxukeyxq = Request["shengchanxukeyxq"] ?? "";
            string shengchanxuketp = Request["shengchanxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_shengchanqiye ob_base_shengchanqiye = new base_shengchanqiye();
                ob_base_shengchanqiye.Daima = daima.Trim();
                ob_base_shengchanqiye.Qiyemingcheng = qiyemingcheng.Trim();
                ob_base_shengchanqiye.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_shengchanqiye.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_shengchanqiye.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_shengchanqiye.ShengchanxukeBH = shengchanxukebh.Trim();
                ob_base_shengchanqiye.ShengchanxukeYXQ = shengchanxukeyxq == "" ? DateTime.Now : DateTime.Parse(shengchanxukeyxq);
                ob_base_shengchanqiye.ShengchanxukeTP = shengchanxuketp.Trim();
                ob_base_shengchanqiye.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_shengchanqiye.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shengchanqiye.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shengchanqiye = ob_base_shengchanqiyeservice.AddEntity(ob_base_shengchanqiye);
                ViewBag.base_shengchanqiye = ob_base_shengchanqiye;
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
            base_shengchanqiye tempData = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == id && base_shengchanqiye.IsDelete == false);
            ViewBag.base_shengchanqiye = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shengchanqiyeViewModel base_shengchanqiyeviewmodel = new base_shengchanqiyeViewModel();
                base_shengchanqiyeviewmodel.ID = tempData.ID;
                base_shengchanqiyeviewmodel.Daima = tempData.Daima;
                base_shengchanqiyeviewmodel.Qiyemingcheng = tempData.Qiyemingcheng;
                base_shengchanqiyeviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                base_shengchanqiyeviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                base_shengchanqiyeviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                base_shengchanqiyeviewmodel.ShengchanxukeBH = tempData.ShengchanxukeBH;
                base_shengchanqiyeviewmodel.ShengchanxukeYXQ = tempData.ShengchanxukeYXQ;
                base_shengchanqiyeviewmodel.ShengchanxukeTP = tempData.ShengchanxukeTP;
                base_shengchanqiyeviewmodel.Shouying = tempData.Shouying;
                base_shengchanqiyeviewmodel.MakeDate = tempData.MakeDate;
                base_shengchanqiyeviewmodel.MakeMan = tempData.MakeMan;
                return View(base_shengchanqiyeviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string shengchanxukebh = Request["shengchanxukebh"] ?? "";
            string shengchanxukeyxq = Request["shengchanxukeyxq"] ?? "";
            string shengchanxuketp = Request["shengchanxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shengchanqiye p = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == uid);
                p.Daima = daima.Trim();
                p.Qiyemingcheng = qiyemingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.ShengchanxukeBH = shengchanxukebh.Trim();
                p.ShengchanxukeYXQ = shengchanxukeyxq == "" ? DateTime.Now : DateTime.Parse(shengchanxukeyxq);
                p.ShengchanxukeTP = shengchanxuketp.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shengchanqiyeservice.UpdateEntity(p);
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
            base_shengchanqiye ob_base_shengchanqiye;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shengchanqiye = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == id && base_shengchanqiye.IsDelete == false);
                    ob_base_shengchanqiye.IsDelete = true;
                    ob_base_shengchanqiyeservice.UpdateEntity(ob_base_shengchanqiye);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

