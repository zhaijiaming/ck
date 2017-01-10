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
    public class u8_lackcargoController : Controller
    {
        private Iu8_lackcargoService ob_u8_lackcargoservice = ServiceFactory.u8_lackcargoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "u8_lackcargo_index";
            Expression<Func<u8_lackcargo, bool>> where = PredicateExtensionses.True<u8_lackcargo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "u8code":
                            string u8code = scld[1];
                            string u8codeequal = scld[2];
                            string u8codeand = scld[3];
                            if (!string.IsNullOrEmpty(u8code))
                            {
                                if (u8codeequal.Equals("="))
                                {
                                    if (u8codeand.Equals("and"))
                                        where = where.And(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                                    else
                                        where = where.Or(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                                }
                                if (u8codeequal.Equals("like"))
                                {
                                    if (u8codeand.Equals("and"))
                                        where = where.And(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                                    else
                                        where = where.Or(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(u8_lackcargo => u8_lackcargo.IsDelete == false);

            var tempData = ob_u8_lackcargoservice.LoadSortEntities(where.Compile(), false, u8_lackcargo => u8_lackcargo.ID).ToPagedList<u8_lackcargo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.u8_lackcargo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "u8_lackcargo_index";
            string page = "1";
            string u8code = Request["u8code"] ?? "";
            string u8codeequal = Request["u8codeequal"] ?? "";
            string u8codeand = Request["u8codeand"] ?? "";
            Expression<Func<u8_lackcargo, bool>> where = PredicateExtensionses.True<u8_lackcargo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(u8code))
                {
                    if (u8codeequal.Equals("="))
                    {
                        if (u8codeand.Equals("and"))
                            where = where.And(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                        else
                            where = where.Or(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                    }
                    if (u8codeequal.Equals("like"))
                    {
                        if (u8codeand.Equals("and"))
                            where = where.And(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                        else
                            where = where.Or(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                    }
                }
                if (!string.IsNullOrEmpty(u8code))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "u8code", u8code, u8codeequal, u8codeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "u8code", "", u8codeequal, u8codeand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(u8code))
                {
                    if (u8codeequal.Equals("="))
                    {
                        if (u8codeand.Equals("and"))
                            where = where.And(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                        else
                            where = where.Or(u8_lackcargo => u8_lackcargo.U8Code == u8code);
                    }
                    if (u8codeequal.Equals("like"))
                    {
                        if (u8codeand.Equals("and"))
                            where = where.And(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                        else
                            where = where.Or(u8_lackcargo => u8_lackcargo.U8Code.Contains(u8code));
                    }
                }
                if (!string.IsNullOrEmpty(u8code))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "u8code", u8code, u8codeequal, u8codeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "u8code", "", u8codeequal, u8codeand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(u8_lackcargo => u8_lackcargo.IsDelete == false);

            var tempData = ob_u8_lackcargoservice.LoadSortEntities(where.Compile(), false, u8_lackcargo => u8_lackcargo.ID).ToPagedList<u8_lackcargo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.u8_lackcargo = tempData;
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
            string u8code = Request["u8code"] ?? "";
            string cargocode = Request["cargocode"] ?? "";
            string recievenumber = Request["recievenumber"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                u8_lackcargo ob_u8_lackcargo = new u8_lackcargo();
                ob_u8_lackcargo.U8Code = u8code.Trim();
                ob_u8_lackcargo.CargoCode = cargocode.Trim();
                ob_u8_lackcargo.RecieveNumber = recievenumber == "" ? 0 : float.Parse(recievenumber);
                ob_u8_lackcargo.Col1 = col1.Trim();
                ob_u8_lackcargo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_u8_lackcargo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_u8_lackcargo = ob_u8_lackcargoservice.AddEntity(ob_u8_lackcargo);
                ViewBag.u8_lackcargo = ob_u8_lackcargo;
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
            u8_lackcargo tempData = ob_u8_lackcargoservice.GetEntityById(u8_lackcargo => u8_lackcargo.ID == id && u8_lackcargo.IsDelete == false);
            ViewBag.u8_lackcargo = tempData;
            if (tempData == null)
                return View();
            else
            {
                u8_lackcargoViewModel u8_lackcargoviewmodel = new u8_lackcargoViewModel();
                u8_lackcargoviewmodel.ID = tempData.ID;
                u8_lackcargoviewmodel.U8Code = tempData.U8Code;
                u8_lackcargoviewmodel.CargoCode = tempData.CargoCode;
                u8_lackcargoviewmodel.RecieveNumber = tempData.RecieveNumber;
                u8_lackcargoviewmodel.Col1 = tempData.Col1;
                u8_lackcargoviewmodel.MakeDate = tempData.MakeDate;
                u8_lackcargoviewmodel.MakeMan = tempData.MakeMan;
                return View(u8_lackcargoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string u8code = Request["u8code"] ?? "";
            string cargocode = Request["cargocode"] ?? "";
            string recievenumber = Request["recievenumber"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                u8_lackcargo p = ob_u8_lackcargoservice.GetEntityById(u8_lackcargo => u8_lackcargo.ID == uid);
                p.U8Code = u8code.Trim();
                p.CargoCode = cargocode.Trim();
                p.RecieveNumber = recievenumber == "" ? 0 : float.Parse(recievenumber);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_u8_lackcargoservice.UpdateEntity(p);
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
            u8_lackcargo ob_u8_lackcargo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_u8_lackcargo = ob_u8_lackcargoservice.GetEntityById(u8_lackcargo => u8_lackcargo.ID == id && u8_lackcargo.IsDelete == false);
                    ob_u8_lackcargo.IsDelete = true;
                    ob_u8_lackcargoservice.UpdateEntity(ob_u8_lackcargo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

