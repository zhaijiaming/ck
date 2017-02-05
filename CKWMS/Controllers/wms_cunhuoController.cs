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
            var _sjdid = id;
            int _userid = (int)Session["user_id"];
            var _username = Session["user_name"];

            var tempData = ob_wms_cunhuoservice.GetUploadList(id).ToList<wms_upload_v>();
            ViewBag.username = _username;
            ViewBag.userid = _userid;
            ViewBag.wms_upload = tempData;
            ViewBag.sjdid = _sjdid;

            ViewBag.linecount = tempData.Count;
            ViewBag.totalproduct = tempData.Sum(p => p.sshuliang/p.Huansuanlv);
            ViewBag.shangjiaSL = tempData.Sum(p => p.sshuliang);
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
            var _shtemp = ServiceFactory.wms_shouhuomxservice.GetEntityById(p => p.ID == int.Parse(_shmx) && p.IsDelete == false);
            if (_shtemp == null)
                return Json(-2);
            if (_shtemp.Yanshou == false)
                return Json(-3);
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
                _cunhuo.ChushiSL = _cunhuo.Shuliang;
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
        [OutputCache(Duration = 30)]
        public ActionResult CurrentStorageExport()
        {
            //string page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_currentstorage";
            Expression<Func<wms_inventory_v, bool>> where = PredicateExtensionses.True<wms_inventory_v>();
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
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "kuwei":
                            string kuwei = scld[1];
                            string kuweiequal = scld[2];
                            string kuweiand = scld[3];
                            if (!string.IsNullOrEmpty(kuwei))
                            {
                                if (kuweiequal.Equals("="))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                                }
                                if (kuweiequal.Equals("like"))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                                }
                            }
                            break;
                        case "shangpindm":
                            string shangpindm = scld[1];
                            string shangpindmequal = scld[2];
                            string shangpindmand = scld[3];
                            if (!string.IsNullOrEmpty(shangpindm))
                            {
                                if (shangpindmequal.Equals("="))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        case "chanpinxian":
                            string chanpinxian = scld[1];
                            string chanpinxianequal = scld[2];
                            string chanpinxianand = scld[3];
                            if (!string.IsNullOrEmpty(chanpinxian))
                            {
                                if (chanpinxianequal.Equals("="))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng == chanpinxian);
                                    else
                                        where = where.Or(p => p.cpxmingcheng == chanpinxian);
                                }
                                if (chanpinxianequal.Equals("like"))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                                    else
                                        where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                                }
                            }
                            break;
                        case "shixiaorq":
                            string shixiaorq = scld[1];
                            string shixiaorqequal = scld[2];
                            string shixiaorqand = scld[3];
                            if (!string.IsNullOrEmpty(shixiaorq))
                            {
                                if (shixiaorqequal.Equals("="))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                                    else
                                        where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals(">"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals("<"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                            }
                            break;
                        case "rukurq":
                            string rukurq = scld[1];
                            string rukurqequal = scld[2];
                            string rukurqand = scld[3];
                            if (!string.IsNullOrEmpty(rukurq))
                            {
                                if (rukurqequal.Equals("="))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals(">"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals("<"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_wms_cunhuoservice.GetInventory(where.Compile()).ToList<wms_inventory_v>();
            ViewBag.wms_storage_v = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "CurrentStorageExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("inventory_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CurrentStorage()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_currentstorage";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //rukudanbh
            string rukudanbh = Request["rukudanbh"] ?? "";
            string rukudanbhequal = Request["rukudanbhequal"] ?? "";
            string rukudanbhand = Request["rukudanbhand"] ?? "";
            //kuwei
            string kuwei = Request["kuwei"] ?? "";
            string kuweiequal = Request["kuweiequal"] ?? "";
            string kuweiand = Request["kuweiand"] ?? "";
            //shangpindm
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpindmequal = Request["shangpindmequal"] ?? "";
            string shangpindmand = Request["shangpindmand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            //shixiaorq
            string shixiaorq = Request["shixiaorq"] ?? "";
            string shixiaorqequal = Request["shixiaorqequal"] ?? "";
            string shixiaorqand = Request["shixiaorqand"] ?? "";
            //Èë¿âÈÕÆÚ
            string rukurq = Request["rukurq"] ?? "";
            string rukurqequal = Request["rukurqequal"] ?? "";
            string rukurqand = Request["rukurqand"] ?? "";
            //chanpinxian
            string chanpinxian = Request["chanpinxian"] ?? "";
            string chanpinxianequal = Request["chanpinxianequal"] ?? "";
            string chanpinxianand = Request["chanpinxianand"] ?? "";

            Expression<Func<wms_inventory_v, bool>> where = PredicateExtensionses.True<wms_inventory_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //kuwei
                if (!string.IsNullOrEmpty(kuwei))
                {
                    if (kuweiequal.Equals("="))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                    }
                    if (kuweiequal.Equals("like"))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                    }
                }
                if (!string.IsNullOrEmpty(kuwei))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", kuwei, kuweiequal, kuweiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", "", kuweiequal, kuweiand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //shixiaorq
                if (!string.IsNullOrEmpty(shixiaorq))
                {
                    if (shixiaorqequal.Equals("="))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                        else
                            where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals(">"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals("<"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                }
                if (!string.IsNullOrEmpty(shixiaorq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", shixiaorq, shixiaorqequal, shixiaorqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", "", shixiaorqequal, shixiaorqand);
                //rukurq
                if (!string.IsNullOrEmpty(rukurq))
                {
                    if (rukurqequal.Equals("="))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals(">"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals("<"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                    }
                }
                if (!string.IsNullOrEmpty(rukurq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", rukurq, rukurqequal, rukurqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", "", rukurqequal, rukurqand);
                //chanpinxian
                if (!string.IsNullOrEmpty(chanpinxian))
                {
                    if (chanpinxianequal.Equals("="))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng == chanpinxian);
                        else
                            where = where.Or(p => p.cpxmingcheng == chanpinxian);
                    }
                    if (chanpinxianequal.Equals("like"))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                        else
                            where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", chanpinxian, chanpinxianequal, chanpinxianand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", "", chanpinxianequal, chanpinxianand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //kuwei
                if (!string.IsNullOrEmpty(kuwei))
                {
                    if (kuweiequal.Equals("="))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                    }
                    if (kuweiequal.Equals("like"))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                    }
                }
                if (!string.IsNullOrEmpty(kuwei))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", kuwei, kuweiequal, kuweiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", "", kuweiequal, kuweiand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //shixiaorq
                if (!string.IsNullOrEmpty(shixiaorq))
                {
                    if (shixiaorqequal.Equals("="))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                        else
                            where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals(">"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals("<"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                }
                if (!string.IsNullOrEmpty(shixiaorq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", shixiaorq, shixiaorqequal, shixiaorqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", "", shixiaorqequal, shixiaorqand);
                //rukurq
                if (!string.IsNullOrEmpty(rukurq))
                {
                    if (rukurqequal.Equals("="))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals(">"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals("<"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                    }
                }
                if (!string.IsNullOrEmpty(rukurq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", rukurq, rukurqequal, rukurqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", "", rukurqequal, rukurqand);
                //chanpinxian
                if (!string.IsNullOrEmpty(chanpinxian))
                {
                    if (chanpinxianequal.Equals("="))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng == chanpinxian);
                        else
                            where = where.Or(p => p.cpxmingcheng == chanpinxian);
                    }
                    if (chanpinxianequal.Equals("like"))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                        else
                            where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                    }
                }
                if (!string.IsNullOrEmpty(chanpinxian))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", chanpinxian, chanpinxianequal, chanpinxianand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", "", chanpinxianequal, chanpinxianand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_inventory_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            ViewBag.totalproduct = tempData.Sum(p => p.sshuliang);
            ViewBag.totalbox = tempData.Sum(p => p.sshuliang / (p.Huansuanlv != 0 ? p.Huansuanlv : 1));
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult CurrentStorage(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cunhuo_currentstorage";
            Expression<Func<wms_inventory_v, bool>> where = PredicateExtensionses.True<wms_inventory_v>();
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
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "kuwei":
                            string kuwei = scld[1];
                            string kuweiequal = scld[2];
                            string kuweiand = scld[3];
                            if (!string.IsNullOrEmpty(kuwei))
                            {
                                if (kuweiequal.Equals("="))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei == kuwei);
                                }
                                if (kuweiequal.Equals("like"))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.Kuwei.Contains(kuwei));
                                }
                            }
                            break;
                        case "shangpindm":
                            string shangpindm = scld[1];
                            string shangpindmequal = scld[2];
                            string shangpindmand = scld[3];
                            if (!string.IsNullOrEmpty(shangpindm))
                            {
                                if (shangpindmequal.Equals("="))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        case "shixiaorq":
                            string shixiaorq = scld[1];
                            string shixiaorqequal = scld[2];
                            string shixiaorqand = scld[3];
                            if (!string.IsNullOrEmpty(shixiaorq))
                            {
                                if (shixiaorqequal.Equals("="))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                                    else
                                        where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals(">"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals("<"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                            }
                            break;
                        case "rukurq":
                            string rukurq = scld[1];
                            string rukurqequal = scld[2];
                            string rukurqand = scld[3];
                            if (!string.IsNullOrEmpty(rukurq))
                            {
                                if (rukurqequal.Equals("="))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals(">"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals("<"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                }
                            }
                            break;
                        case "chanpinxian":
                            string chanpinxian = scld[1];
                            string chanpinxianequal = scld[2];
                            string chanpinxianand = scld[3];
                            if (!string.IsNullOrEmpty(chanpinxian))
                            {
                                if (chanpinxianequal.Equals("="))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng == chanpinxian);
                                    else
                                        where = where.Or(p => p.cpxmingcheng == chanpinxian);
                                }
                                if (chanpinxianequal.Equals("like"))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                                    else
                                        where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            ViewBag.totalproduct = tempData.Sum(p => p.sshuliang);
            ViewBag.totalbox = tempData.Sum(p => p.sshuliang / (p.Huansuanlv!=0?p.Huansuanlv:1));
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
                where = where.And(p => p.ShangpinMC.Contains(_mc));
            if (!string.IsNullOrEmpty(_gg))
                where = where.And(p => p.Guige.Contains(_gg));
            if (!string.IsNullOrEmpty(_ph))
                where = where.And(p => p.Pihao.Contains(_ph));
            where = where.And(p => p.chsl > 0);
            //var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid),p=>p.chsl>0);
            var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid), where.Compile());
            if (tempData == null)
                return Json(-1);

            return Json(tempData.ToList<wms_invgoods_v>());
        }
        public JsonResult GetCustStoreGood2()
        {
            int _userid = (int)Session["user_id"];
            var _custid = Request["cust"] ?? "";
            var _mc = Request["mc"] ?? "";
            var _gg = Request["gg"] ?? "";
            var _ph = Request["ph"] ?? "";
            if (int.Parse(_custid)== 0)
            {
                //if (string.IsNullOrEmpty(_ph))
                //    return Json(-1);
                //var _pp = ob_wms_cunhuoservice.GetInventoryGoods(p => p.Pihao == _ph).ToList<wms_invgoods_v>();
                //if (_pp.Count == 0)
                //    return Json(-1);
                if (string.IsNullOrEmpty(_gg))
                    return Json(-1);
                var _pp = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.Guige == _gg);
                if (_pp == null)
                    return Json(-1);
                _custid = _pp.HuozhuID.ToString();
            }
            //return Json(-1);
            //Expression<Func<wms_invgoods_v, bool>> where = PredicateExtensionses.True<wms_invgoods_v>();
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();
            if (!string.IsNullOrEmpty(_mc))
                where = where.And(p => p.ShangpinMC.Contains(_mc));
            if (!string.IsNullOrEmpty(_gg))
                where = where.And(p => p.Guige.Contains(_gg));
            if (!string.IsNullOrEmpty(_ph))
                where = where.And(p => p.Pihao.Contains(_ph));
            //where = where.And(p => p.chsl > 0);
            where = where.And(p => p.sshuliang > 0);
            //var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid),p=>p.chsl>0);
            //var tempData = ob_wms_cunhuoservice.GetInventoryGoodsByCust(int.Parse(_custid), where.Compile());
            var tempData = ob_wms_cunhuoservice.GetStorageList(int.Parse(_custid), where.Compile());
            if (tempData == null)
                return Json(-1);

            return Json(tempData.ToList());
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
                ob_wms_cunhuo.ChushiSL = ob_wms_cunhuo.Shuliang;
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

            var tempData = ob_wms_cunhuoservice.GetStorageList(int.Parse(_custid), p => p.sshuliang > 0);
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
        public JsonResult UpDelete()
        {
            int _userid = (int)Session["user_id"];
            var _upid = Request["up"] ?? "";
            if (string.IsNullOrEmpty(_upid))
                return Json(-1);
            wms_cunhuo _up = ob_wms_cunhuoservice.GetEntityById(p => p.ID == int.Parse(_upid));
            if (_up == null)
                return Json(-1);
            _up.IsDelete = true;
            if (!ob_wms_cunhuoservice.UpdateEntity(_up))
                return Json(-1);
            return Json(1);
        }
        public ActionResult PrintShouHuoList()
        {
            var _sjdid = Request["sjdid"] ?? "";
            ViewBag.sjdid = _sjdid;
            return View();
        }
        [OutputCache(Duration = 30)]
        public ActionResult RemindOverdue(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";

            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_wms_cunhuoservice.GetStorageList(p=>p.ShixiaoRQ<=DateTime.Now.AddDays(int.Parse(period))).ToPagedList<wms_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            ViewBag.period = period;
            return View(tempData);
        }
        public ActionResult RemindOverdueExport()
        {
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";
            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_wms_cunhuoservice.GetStorageList(p => p.ShixiaoRQ <= DateTime.Now.AddDays(int.Parse(period)));
            ViewBag.wms_storage_v = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "RemindOverdueExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("Overdue_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult InoutCheck(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_inoutcheck";
            Expression<Func<wms_inoutcheckgood_v, bool>> where = PredicateExtensionses.True<wms_inoutcheckgood_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
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
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.pihao == pihao);
                                    else
                                        where = where.Or(p => p.pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.pihao.Contains(pihao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ob_wms_cunhuoservice.InoutCheck(where.Compile()).ToPagedList<wms_inoutcheckgood_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cunhuo = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult InoutCheck()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_inoutcheck";
            string page = "1";
            //guige
            string guige = Request["guige"] ?? "";
            string guigeequal = Request["guigeequal"] ?? "";
            string guigeand = Request["guigeand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            //cysl
            string cysl = Request["cysl"] ?? "";
            string cyslequal = Request["cyslequal"] ?? "";
            string cysland = Request["cysland"] ?? "";
            Expression<Func<wms_inoutcheckgood_v, bool>> where = PredicateExtensionses.True<wms_inoutcheckgood_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
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
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.pihao == pihao);
                        else
                            where = where.Or(p => p.pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //cysl
                if (!string.IsNullOrEmpty(cysl))
                {
                    if (cyslequal.Equals("="))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl == double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl == double.Parse(cysl));
                    }
                    if (cyslequal.Equals(">"))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl > double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl > double.Parse(cysl));
                    }
                    if (cyslequal.Equals("<"))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl < double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl < double.Parse(cysl));
                    }
                }
                if (!string.IsNullOrEmpty(cysl))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cysl", cysl, cyslequal, cysland);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cysl", "", cyslequal, cysland);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
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
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.pihao == pihao);
                        else
                            where = where.Or(p => p.pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //cysl
                if (!string.IsNullOrEmpty(cysl))
                {
                    if (cyslequal.Equals("="))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl == double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl == double.Parse(cysl));
                    }
                    if (cyslequal.Equals(">"))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl > double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl > double.Parse(cysl));
                    }
                    if (cyslequal.Equals("<"))
                    {
                        if (cysland.Equals("and"))
                            where = where.And(p => p.cysl < double.Parse(cysl));
                        else
                            where = where.Or(p => p.cysl < double.Parse(cysl));
                    }
                }
                if (!string.IsNullOrEmpty(cysl))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cysl", cysl, cyslequal, cysland);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cysl", "", cyslequal, cysland);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_wms_cunhuoservice.InoutCheck(where.Compile()).ToPagedList<wms_inoutcheckgood_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cunhuo = tempData;
            return View(tempData);
        }
    }
}

