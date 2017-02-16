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
    public class quan_chukufhController : Controller
    {
        private Iquan_chukufhService ob_quan_chukufhservice = ServiceFactory.quan_chukufhservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_chukufh_index";
            Expression<Func<quan_outrec_v, bool>> where = PredicateExtensionses.True<quan_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mingxiid":
                            string mingxiid = scld[1];
                            string mingxiidequal = scld[2];
                            string mingxiidand = scld[3];
                            if (!string.IsNullOrEmpty(mingxiid))
                            {
                                if (mingxiidequal.Equals("="))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_outrec_v => quan_outrec_v.ID == int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_outrec_v => quan_outrec_v.ID == int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals(">"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_outrec_v => quan_outrec_v.ID > int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_outrec_v => quan_outrec_v.ID > int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals("<"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_outrec_v => quan_outrec_v.ID < int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_outrec_v => quan_outrec_v.ID < int.Parse(mingxiid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //var tempData = ob_quan_chukufhservice.LoadSortEntities(where.Compile(), false, quan_chukufh => quan_chukufh.ID).ToPagedList<quan_chukufh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            var tempData = ob_quan_chukufhservice.GetOutrec(where.Compile()).ToPagedList<quan_outrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_outrec = tempData;
            return View(tempData);
        }
        public ActionResult OutInventoryCheck(int id)
        {
            var outid = id;
            int _userid = (int)Session["user_id"];
            var _username = Session["user_name"];
            //var _ckid = Request["ck"] ?? "";
            //if (_ckid == "")
            //    _ckid = "0";

            var tempData = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(id,p=>p.JianhuoSL>0).ToList<quan_outcheck_v>();
            ViewBag.waitcheck = tempData;
            ViewBag.userid = _userid;
            ViewBag.username = _username;
            ViewBag.outid = outid;
            
            ViewBag.linecount = tempData.Count;
            ViewBag.fuhesls = tempData.Sum(p => p.FuheSL);
            ViewBag.fuhejss = tempData.Sum(p => p.FuheSL/p.Huansuanlv);
            return View();
        }
        public ActionResult PrintFuheDan()
        {
            var id = Request["ckfhid"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintFuhejianyan()
        {
            var id = Request["ckfhid"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public JsonResult CheckOver()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckid))
                return Json(-1);
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            if (_ckd == null)
                return Json(-1);
            if (_ckd.JihuaZT > 3)
                return Json(-2);
            _ckd.JihuaZT = 3;
            if (!ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd))
                return Json(-1);
            return Json(1);
        }
        public JsonResult CheckDelete()
        {
            int _userid = (int)Session["user_id"];
            var _fh = Request["fh"] ?? "";
            var _ckid = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckid) || string.IsNullOrEmpty(_fh))
                return Json(-1);
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            if (_ckd.JihuaZT > 2)
                return Json(-2);
            var _check = ob_quan_chukufhservice.GetEntityById(p => p.ID == int.Parse(_fh) && p.IsDelete == false);
            if (_check == null)
                return Json(-1);
            _check.IsDelete = true;
            if (!ob_quan_chukufhservice.UpdateEntity(_check))
                return Json(-1);
            return Json(1);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_chukufh_index";
            string page = "1";
            //chukudanbh
            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "";
            Expression<Func<quan_outrec_v, bool>> where = PredicateExtensionses.True<quan_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(p => p.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals(">"))
                    {
                        if (chukudanbhand.Equals("like"))
                            where = where.And(p => p.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(p => p.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(p => p.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(p => p.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_quan_chukufhservice.GetOutrec(where.Compile()).ToPagedList<quan_outrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_outrec = tempData;
            return View(tempData);
        }
        public JsonResult AddCheck()
        {
            int _userid = (int)Session["user_id"];
            string _mxid = Request["shmx"] ?? "";
            string _fhsl = Request["sl"] ?? "";
            string _fhresult = Request["ys"] ?? "";
            string _fhren = Request["ysr"] ?? "";
            string _fhsm = Request["yssm"] ?? "";
            try
            {
                quan_chukufh ob_quan_rukuys = new quan_chukufh();
                ob_quan_rukuys.MingxiID = _mxid == "" ? 0 : int.Parse(_mxid);
                ob_quan_rukuys.FuheSL = _fhsl == "" ? 0 : float.Parse(_fhsl);
                ob_quan_rukuys.Fuhe = _fhresult == "" ? 0 : int.Parse(_fhresult);
                ob_quan_rukuys.Fuheren = _fhren.Trim();
                ob_quan_rukuys.FuheSM = _fhsm.Trim();
                ob_quan_rukuys.FuheZT = 3;
                ob_quan_rukuys.MakeDate = DateTime.Now;
                ob_quan_rukuys.MakeMan = _userid;
                ob_quan_rukuys = ob_quan_chukufhservice.AddEntity(ob_quan_rukuys);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(-1);
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
            string mingxiid = Request["mingxiid"] ?? "";
            string fuhesl = Request["fuhesl"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            string fuheren = Request["fuheren"] ?? "";
            string fuhesm = Request["fuhesm"] ?? "";
            string fuhezt = Request["fuhezt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                quan_chukufh ob_quan_chukufh = new quan_chukufh();
                ob_quan_chukufh.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                ob_quan_chukufh.FuheSL = fuhesl == "" ? 0 : float.Parse(fuhesl);
                ob_quan_chukufh.Fuhe = fuhe == "" ? 0 : int.Parse(fuhe);
                ob_quan_chukufh.Fuheren = fuheren.Trim();
                ob_quan_chukufh.FuheSM = fuhesm.Trim();
                ob_quan_chukufh.FuheZT = fuhezt == "" ? 0 : int.Parse(fuhezt);
                ob_quan_chukufh.Col1 = col1.Trim();
                ob_quan_chukufh.Col2 = col2.Trim();
                ob_quan_chukufh.Col3 = col3.Trim();
                ob_quan_chukufh.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_chukufh.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_chukufh = ob_quan_chukufhservice.AddEntity(ob_quan_chukufh);
                ViewBag.quan_chukufh = ob_quan_chukufh;
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
            quan_chukufh tempData = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == id && quan_chukufh.IsDelete == false);
            ViewBag.quan_chukufh = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_chukufhViewModel quan_chukufhviewmodel = new quan_chukufhViewModel();
                quan_chukufhviewmodel.ID = tempData.ID;
                quan_chukufhviewmodel.MingxiID = tempData.MingxiID;
                quan_chukufhviewmodel.FuheSL = tempData.FuheSL;
                quan_chukufhviewmodel.Fuhe = tempData.Fuhe;
                quan_chukufhviewmodel.Fuheren = tempData.Fuheren;
                quan_chukufhviewmodel.FuheSM = tempData.FuheSM;
                quan_chukufhviewmodel.FuheZT = tempData.FuheZT;
                quan_chukufhviewmodel.Col1 = tempData.Col1;
                quan_chukufhviewmodel.Col2 = tempData.Col2;
                quan_chukufhviewmodel.Col3 = tempData.Col3;
                quan_chukufhviewmodel.MakeDate = tempData.MakeDate;
                quan_chukufhviewmodel.MakeMan = tempData.MakeMan;
                return View(quan_chukufhviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string mingxiid = Request["mingxiid"] ?? "";
            string fuhesl = Request["fuhesl"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            string fuheren = Request["fuheren"] ?? "";
            string fuhesm = Request["fuhesm"] ?? "";
            string fuhezt = Request["fuhezt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_chukufh p = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == uid);
                p.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                p.FuheSL = fuhesl == "" ? 0 : float.Parse(fuhesl);
                p.Fuhe = fuhe == "" ? 0 : int.Parse(fuhe);
                p.Fuheren = fuheren.Trim();
                p.FuheSM = fuhesm.Trim();
                p.FuheZT = fuhezt == "" ? 0 : int.Parse(fuhezt);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_chukufhservice.UpdateEntity(p);
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
            quan_chukufh ob_quan_chukufh;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_chukufh = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == id && quan_chukufh.IsDelete == false);
                    ob_quan_chukufh.IsDelete = true;
                    ob_quan_chukufhservice.UpdateEntity(ob_quan_chukufh);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PrintCukuFH()
        {
            var zlgl_ckfhid = Request["zlgl_ckfhid"] ?? "";
            ViewBag.zlgl_ckfhid = zlgl_ckfhid;
            return View();
        }
        public JsonResult Check_ckfhid()
        {
            var zlgl_ckfhbh = Request["zlgl_ckfhid"] ?? "";
            var flag = 0;
            var tempdata = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == zlgl_ckfhbh);
            var firstdata = tempdata.FirstOrDefault();
            if (firstdata != null)
            {
                flag = 1;
            }
            return Json(flag);
        }
        public JsonResult CheckCancel()
        {
            int _userid = (int)Session["user_id"];
            var _ckdid = Request["ckd"] ?? "";
            if (string.IsNullOrEmpty(_ckdid))
                return Json(-1);
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckdid) && p.IsDelete == false);
            if (_ckd == null)
                return Json(-1);
            if (_ckd.JihuaZT == 3)
            {
                _ckd.JihuaZT = 1;
                ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
            }
            else
                return Json(-2);
            return Json(1);
        }

    }
}

