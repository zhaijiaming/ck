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
    public class wms_cunhuoController : Controller
    {
        private Iwms_cunhuoService ob_wms_cunhuoservice = ServiceFactory.wms_cunhuoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_index";
            Expression<Func<wms_cunhuo, bool>> where = PredicateExtensionses.True<wms_cunhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rkmxid":
                            string rkmxid = scld[1];
                            string rkmxidequal = scld[2];
                            string rkmxidand = scld[3];
                            if (!string.IsNullOrEmpty(rkmxid))
                            {
                                if (rkmxidequal.Equals("="))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals(">"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals("<"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_cunhuo => wms_cunhuo.IsDelete == false);

            var tempData = ob_wms_cunhuoservice.LoadSortEntities(where.Compile(), false, wms_cunhuo => wms_cunhuo.ID).ToPagedList<wms_cunhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cunhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_index";
            string page = "1";
            string rkmxid = Request["rkmxid"] ?? "";
            string rkmxidequal = Request["rkmxidequal"] ?? "";
            string rkmxidand = Request["rkmxidand"] ?? "";
            Expression<Func<wms_cunhuo, bool>> where = PredicateExtensionses.True<wms_cunhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_cunhuo => wms_cunhuo.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_cunhuo => wms_cunhuo.IsDelete == false);

            var tempData = ob_wms_cunhuoservice.LoadSortEntities(where.Compile(), false, wms_cunhuo => wms_cunhuo.ID).ToPagedList<wms_cunhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cunhuo = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult GetUploadList(int id)
        {
            int _userid = (int)Session["user_id"];
            var _username = Session["user_name"];

            var tempData = ob_wms_cunhuoservice.GetUploadList(id);
            ViewBag.username = _username;
            ViewBag.userid = _userid;
            ViewBag.wms_upload = tempData;
            return View();
        }
        public JsonResult AddUpload()
        {
            int _userid = (int)Session["user_id"];
            string _shmx = Request["shmx"] ?? "0";
            string _sl = Request["sl"] ?? "0";
            string _kw = Request["kw"] ?? "";
            string _kwid = Request["kid"] ?? "0";
            string _sjr = Request["sjr"] ?? "";
            string _zl = Request["zl"] ?? "0";
            string _tj = Request["tj"] ?? "0";
            string _jz = Request["jz"] ?? "0";
            string _jf = Request["jf"] ?? "0";
            string _sm = Request["sm"] ?? "";
            string _hg = Request["hg"] ?? "true";

            if (int.Parse(_shmx) == 0)
                return Json(-1);
            try
            {
                wms_cunhuo _cunhuo = new wms_cunhuo();
                _cunhuo.RKMXID = int.Parse(_shmx);
                _cunhuo.Shuliang = float.Parse(_sl);
                _cunhuo.Tiji = float.Parse(_tj);
                _cunhuo.Zhongliang = float.Parse(_zl);
                _cunhuo.Jingzhong = float.Parse(_jz);
                _cunhuo.Jifeidun = float.Parse(_jf);
                _cunhuo.CunhuoSM = _sm;
                _cunhuo.Kuwei = _kw;
                _cunhuo.RenSJ = _sjr;
                _cunhuo.KuweiID = int.Parse(_kwid);
                _cunhuo.MakeMan = _userid;
                _cunhuo.MakeDate = DateTime.Now;
                _cunhuo.HegeSF = bool.Parse(_hg);
                _cunhuo.SuodingSF = false;
                _cunhuo.CunhuoZT = 1;
                _cunhuo.JiahuoSF = true;
                _cunhuo = ob_wms_cunhuoservice.AddEntity(_cunhuo);
                if (_cunhuo == null)
                    return Json(-1);
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
        [OutputCache(Duration =30)]
        public ActionResult CurrentStorage()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_currentstorage";
            string page = "1";
            string rkmxid = Request["rkmxid"] ?? "";
            string rkmxidequal = Request["rkmxidequal"] ?? "";
            string rkmxidand = Request["rkmxidand"] ?? "";
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult CurrentStorage(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_currentstorage";
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rkmxid":
                            string rkmxid = scld[1];
                            string rkmxidequal = scld[2];
                            string rkmxidand = scld[3];
                            if (!string.IsNullOrEmpty(rkmxid))
                            {
                                if (rkmxidequal.Equals("="))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.RKMXID == int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals(">"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.RKMXID > int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals("<"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.RKMXID < int.Parse(rkmxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }        

            var tempData = ob_wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            return View(tempData);
        }
        public JsonResult GetCustStore()
        {
            int _userid = (int)Session["user_id"];
            var _custid = Request["cust"] ?? "";

            if (_custid.Length == 0)
                return Json(-1);
            var tempData = ob_wms_cunhuoservice.GetStorageList(p => p.HuozhuID == int.Parse(_custid));
            if (tempData == null)
                return Json(-1);

            return Json(tempData.ToList<wms_storage_v>());
        }
        public JsonResult GetCustStoreGood()
        {
            int _userid = (int)Session["user_id"];
            var _custid = Request["cust"] ?? "";
            var _mc = Request["mc"] ?? "";
            var _gg = Request["gg"] ?? "";
            var _ph = Request["ph"] ?? "";
            if (_custid.Length == 0)
                return Json(-1);
            Expression<Func<wms_invgoods_v, bool>> where = PredicateExtensionses.True<wms_invgoods_v>();
            if (!string.IsNullOrEmpty(_mc))
                where = where.And(p => p.ShangpinMC == _mc);
            if (!string.IsNullOrEmpty(_gg))
                where = where.And(p => p.Guige == _gg);
            if (!string.IsNullOrEmpty(_ph))
                where = where.And(p => p.Pihao == _ph);
            where = where.And(p => p.chsl > 0);
            //var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid),p=>p.chsl>0);
            var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid),where.Compile());
            if (tempData == null)
                return Json(-1);

            return Json(tempData.ToList<wms_invgoods_v>(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string rkmxid = Request["rkmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string suodingsf = Request["suodingsf"] ?? "";
            string cunhuozt = Request["cunhuozt"] ?? "";
            string cunhuosm = Request["cunhuosm"] ?? "";
            string jiahuosf = Request["jiahuosf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string hegesf = Request["hegesf"] ?? "";
            try
            {
                wms_cunhuo ob_wms_cunhuo = new wms_cunhuo();
                ob_wms_cunhuo.RKMXID = rkmxid == "" ? 0 : int.Parse(rkmxid);
                ob_wms_cunhuo.Kuwei = kuwei.Trim();
                ob_wms_cunhuo.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                ob_wms_cunhuo.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                ob_wms_cunhuo.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_cunhuo.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_cunhuo.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_cunhuo.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_cunhuo.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                ob_wms_cunhuo.SuodingSF = suodingsf == "" ? false : Boolean.Parse(suodingsf);
                ob_wms_cunhuo.CunhuoZT = cunhuozt == "" ? 0 : int.Parse(cunhuozt);
                ob_wms_cunhuo.CunhuoSM = cunhuosm.Trim();
                ob_wms_cunhuo.JiahuoSF = jiahuosf == "" ? false : Boolean.Parse(jiahuosf);
                ob_wms_cunhuo.Col1 = col1.Trim();
                ob_wms_cunhuo.Col2 = col2.Trim();
                ob_wms_cunhuo.Col3 = col3.Trim();
                ob_wms_cunhuo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_cunhuo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cunhuo.HegeSF = hegesf == "" ? false : Boolean.Parse(hegesf);
                ob_wms_cunhuo = ob_wms_cunhuoservice.AddEntity(ob_wms_cunhuo);
                ViewBag.wms_cunhuo = ob_wms_cunhuo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetStorageByCust()
        {
            int _userid = (int)Session["user_id"];
            var _custid = Request["cust"] ?? "";
            if (_custid == "")
                return Json(-1);

            var tempData = ob_wms_cunhuoservice.GetStorageList(int.Parse(_custid),p=>p.sshuliang > 0);
            if (tempData == null)
                return Json(-1);
            return Json(tempData.ToList<wms_storage_v>());
        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_cunhuo tempData = ob_wms_cunhuoservice.GetEntityById(wms_cunhuo => wms_cunhuo.ID == id && wms_cunhuo.IsDelete == false);
            ViewBag.wms_cunhuo = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_cunhuoViewModel wms_cunhuoviewmodel = new wms_cunhuoViewModel();
                wms_cunhuoviewmodel.ID = tempData.ID;
                wms_cunhuoviewmodel.RKMXID = tempData.RKMXID;
                wms_cunhuoviewmodel.Kuwei = tempData.Kuwei;
                wms_cunhuoviewmodel.KuweiID = tempData.KuweiID;
                wms_cunhuoviewmodel.Shuliang = tempData.Shuliang;
                wms_cunhuoviewmodel.Zhongliang = tempData.Zhongliang;
                wms_cunhuoviewmodel.Jingzhong = tempData.Jingzhong;
                wms_cunhuoviewmodel.Tiji = tempData.Tiji;
                wms_cunhuoviewmodel.Jifeidun = tempData.Jifeidun;
                wms_cunhuoviewmodel.DaijianSL = tempData.DaijianSL;
                wms_cunhuoviewmodel.SuodingSF = tempData.SuodingSF;
                wms_cunhuoviewmodel.CunhuoZT = tempData.CunhuoZT;
                wms_cunhuoviewmodel.CunhuoSM = tempData.CunhuoSM;
                wms_cunhuoviewmodel.JiahuoSF = tempData.JiahuoSF;
                wms_cunhuoviewmodel.Col1 = tempData.Col1;
                wms_cunhuoviewmodel.Col2 = tempData.Col2;
                wms_cunhuoviewmodel.Col3 = tempData.Col3;
                wms_cunhuoviewmodel.MakeDate = tempData.MakeDate;
                wms_cunhuoviewmodel.MakeMan = tempData.MakeMan;
                wms_cunhuoviewmodel.HegeSF = tempData.HegeSF;
                return View(wms_cunhuoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rkmxid = Request["rkmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string suodingsf = Request["suodingsf"] ?? "";
            string cunhuozt = Request["cunhuozt"] ?? "";
            string cunhuosm = Request["cunhuosm"] ?? "";
            string jiahuosf = Request["jiahuosf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string hegesf = Request["hegesf"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_cunhuo p = ob_wms_cunhuoservice.GetEntityById(wms_cunhuo => wms_cunhuo.ID == uid);
                p.RKMXID = rkmxid == "" ? 0 : int.Parse(rkmxid);
                p.Kuwei = kuwei.Trim();
                p.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                p.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                p.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                p.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                p.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                p.SuodingSF = suodingsf == "" ? false : Boolean.Parse(suodingsf);
                p.CunhuoZT = cunhuozt == "" ? 0 : int.Parse(cunhuozt);
                p.CunhuoSM = cunhuosm.Trim();
                p.JiahuoSF = jiahuosf == "" ? false : Boolean.Parse(jiahuosf);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.HegeSF = hegesf == "" ? false : Boolean.Parse(hegesf);
                ob_wms_cunhuoservice.UpdateEntity(p);
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
            wms_cunhuo ob_wms_cunhuo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_cunhuo = ob_wms_cunhuoservice.GetEntityById(wms_cunhuo => wms_cunhuo.ID == id && wms_cunhuo.IsDelete == false);
                    ob_wms_cunhuo.IsDelete = true;
                    ob_wms_cunhuoservice.UpdateEntity(ob_wms_cunhuo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

