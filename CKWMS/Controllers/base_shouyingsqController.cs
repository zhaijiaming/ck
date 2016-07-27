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
    public class base_shouyingsqController : Controller
    {
        private Ibase_shouyingsqService ob_base_shouyingsqservice = ServiceFactory.base_shouyingsqservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouyingsq_index";
            Expression<Func<base_shouyingsq, bool>> where = PredicateExtensionses.True<base_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "leibie":
                            string leibie = scld[1];
                            string leibieequal = scld[2];
                            string leibieand = scld[3];
                            if (!string.IsNullOrEmpty(leibie))
                            {
                                if (leibieequal.Equals("="))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                                    else
                                        where = where.Or(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                                }
                                if (leibieequal.Equals(">"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                                    else
                                        where = where.Or(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                                }
                                if (leibieequal.Equals("<"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                                    else
                                        where = where.Or(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shouyingsq => base_shouyingsq.IsDelete == false);

            var tempData = ob_base_shouyingsqservice.LoadSortEntities(where.Compile(), false, base_shouyingsq => base_shouyingsq.ID).ToPagedList<base_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouyingsq = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouyingsq_index";
            string page = "1";
            string leibie = Request["leibie"] ?? "";
            string leibieequal = Request["leibieequal"] ?? "";
            string leibieand = Request["leibieand"] ?? "";
            Expression<Func<base_shouyingsq, bool>> where = PredicateExtensionses.True<base_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(base_shouyingsq => base_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shouyingsq => base_shouyingsq.IsDelete == false);

            var tempData = ob_base_shouyingsqservice.LoadSortEntities(where.Compile(), false, base_shouyingsq => base_shouyingsq.ID).ToPagedList<base_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouyingsq = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string neirong = Request["neirong"] ?? "";
            string sqshijian = Request["sqshijian"] ?? "";
            string sqren = Request["sqren"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string shenheren = Request["shenheren"] ?? "";
            string shenheshuoming = Request["shenheshuoming"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_shouyingsq ob_base_shouyingsq = new base_shouyingsq();
                ob_base_shouyingsq.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                ob_base_shouyingsq.Neirong = neirong.Trim();
                ob_base_shouyingsq.SQshijian = sqshijian == "" ? DateTime.Now : DateTime.Parse(sqshijian);
                ob_base_shouyingsq.SQren = sqren.Trim();
                ob_base_shouyingsq.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_base_shouyingsq.Shenheren = shenheren.Trim();
                ob_base_shouyingsq.Shenheshuoming = shenheshuoming.Trim();
                ob_base_shouyingsq.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shouyingsq.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouyingsq = ob_base_shouyingsqservice.AddEntity(ob_base_shouyingsq);
                ViewBag.base_shouyingsq = ob_base_shouyingsq;
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
            base_shouyingsq tempData = ob_base_shouyingsqservice.GetEntityById(base_shouyingsq => base_shouyingsq.ID == id && base_shouyingsq.IsDelete == false);
            ViewBag.base_shouyingsq = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shouyingsqViewModel base_shouyingsqviewmodel = new base_shouyingsqViewModel();
                base_shouyingsqviewmodel.ID = tempData.ID;
                base_shouyingsqviewmodel.Leibie = tempData.Leibie;
                base_shouyingsqviewmodel.Neirong = tempData.Neirong;
                base_shouyingsqviewmodel.SQshijian = tempData.SQshijian;
                base_shouyingsqviewmodel.SQren = tempData.SQren;
                base_shouyingsqviewmodel.Zhuangtai = tempData.Zhuangtai;
                base_shouyingsqviewmodel.Shenheren = tempData.Shenheren;
                base_shouyingsqviewmodel.Shenheshuoming = tempData.Shenheshuoming;
                base_shouyingsqviewmodel.MakeDate = tempData.MakeDate;
                base_shouyingsqviewmodel.MakeMan = tempData.MakeMan;
                return View(base_shouyingsqviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string neirong = Request["neirong"] ?? "";
            string sqshijian = Request["sqshijian"] ?? "";
            string sqren = Request["sqren"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string shenheren = Request["shenheren"] ?? "";
            string shenheshuoming = Request["shenheshuoming"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shouyingsq p = ob_base_shouyingsqservice.GetEntityById(base_shouyingsq => base_shouyingsq.ID == uid);
                p.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                p.Neirong = neirong.Trim();
                p.SQshijian = sqshijian == "" ? DateTime.Now : DateTime.Parse(sqshijian);
                p.SQren = sqren.Trim();
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.Shenheren = shenheren.Trim();
                p.Shenheshuoming = shenheshuoming.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouyingsqservice.UpdateEntity(p);
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
            base_shouyingsq ob_base_shouyingsq;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shouyingsq = ob_base_shouyingsqservice.GetEntityById(base_shouyingsq => base_shouyingsq.ID == id && base_shouyingsq.IsDelete == false);
                    ob_base_shouyingsq.IsDelete = true;
                    ob_base_shouyingsqservice.UpdateEntity(ob_base_shouyingsq);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

