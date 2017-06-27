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
using System.Text;

namespace CKWMS.Controllers
{
    public class wms_rukudanController : Controller
    {
        private Iwms_rukudanService ob_wms_rukudanservice = ServiceFactory.wms_rukudanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_index";
            PageMenu.Set("Index", "wms_rukudan", "仓库操作");
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        
                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "gongyingshangid":
                            string gongyingshangid = scld[1];
                            string gongyingshangidequal = scld[2];
                            string gongyingshangidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingshangid))
                            {
                                if (gongyingshangidequal.Equals("="))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals(">"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals("<"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                }
                            }
                            break;
                        case "fahuodizhi":
                            string fahuodizhi = scld[1];
                            string fahuodizhiequal = scld[2];
                            string fahuodizhiand = scld[3];
                            if (!string.IsNullOrEmpty(fahuodizhi))
                            {
                                if (fahuodizhiequal.Equals("="))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                }
                                if (fahuodizhiequal.Equals("like"))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_index";
            string page = "1";
            //rukudanbh
            string rukudanbh = Request["rukudanbh"] ?? "";
            string rukudanbhequal = Request["rukudanbhequal"] ?? "";
            string rukudanbhand = Request["rukudanbhand"] ?? "";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //KehuDH 
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "";
            //gongyingshangid
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string gongyingshangidequal = Request["gongyingshangidequal"] ?? "";
            string gongyingshangidand = Request["gongyingshangidand"] ?? "";
            //fahuodizhi
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string fahuodizhiequal = Request["fahuodizhiequal"] ?? "";
            string fahuodizhiand = Request["fahuodizhiand"] ?? "";
            PageMenu.Set("Index", "wms_rukudan", "仓库操作");
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);
                //fahuodizhi
                if (!string.IsNullOrEmpty(fahuodizhi))
                {
                    if (fahuodizhiequal.Equals("="))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                    }
                    if (fahuodizhiequal.Equals("like"))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                    }
                }
                if (!string.IsNullOrEmpty(fahuodizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", fahuodizhi, fahuodizhiequal, fahuodizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", "", fahuodizhiequal, fahuodizhiand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);
                //fahuodizhi
                if (!string.IsNullOrEmpty(fahuodizhi))
                {
                    if (fahuodizhiequal.Equals("="))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                    }
                    if (fahuodizhiequal.Equals("like"))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                    }
                }
                if (!string.IsNullOrEmpty(fahuodizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", fahuodizhi, fahuodizhiequal, fahuodizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", "", fahuodizhiequal, fahuodizhiand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }
        public ActionResult OperateList()
        {
            int userid = (int)Session["user_id"];

            PageMenu.Set("OperateList", "wms_rukudan", "仓库操作");
            var tempData = ob_wms_rukudanservice.LoadSortEntities(p => p.IsDelete == false && p.RukuZT < 5, false, s => s.MakeDate);
            ViewBag.wms_rukudan = tempData;
            return View();
        }
        public JsonResult EntryFinish()
        {
            var _ids = Request["rk"] ?? "";
            if (string.IsNullOrEmpty(_ids))
                return Json(-1);
            string[] _efs = _ids.Split(',');
            foreach (var _rkid in _efs)
            {
                wms_rukudan _rkd = ob_wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkid));
                if (_rkd != null)
                {
                    var _ng = false;
                    if (_rkd.YanshouSF && _rkd.RukuZT < 3)
                        _ng = true;
                    else
                    {
                        var _shmx = ServiceFactory.wms_shouhuomxservice.LoadEntities(p => p.RukuID == _rkd.ID && p.IsDelete == false).ToList<wms_shouhuomx>();
                        if (_shmx.Count == 0)
                            _ng = true;
                        foreach(var mx in _shmx)
                        {
                            if (mx.Shuliang != mx.ShangjiaSL || mx.Yanshou==false)
                            {
                                _ng = true;
                                break;
                            }
                        }
                        var _shtotal = _shmx.Sum(p => p.ShangjiaSL);
                        if(_rkd.JihuaID!=null && _rkd.JihuaID > 0)
                        {
                            var _jhmx = ServiceFactory.cust_rukujihuamxservice.LoadEntities(p => p.JihuaID == _rkd.JihuaID && p.IsDelete == false).ToList();
                            if (_jhmx.Count == 0)
                                _ng = true;
                            var _jhmxtotaljh = _jhmx.Sum(p => p.JihuaSL);
                            var _jhmxtotaldh = _jhmx.Sum(p => p.DaohuoSL);
                            var _jhmxtotal = _jhmxtotaljh - _jhmxtotaldh;
                            if (_shtotal > _jhmxtotal)
                                return Json(-3);
                        }
                    }
                    if (!_ng)
                    {
                        _rkd.RukuZT = 5;
                        _rkd.MakeDate = DateTime.Now;
                        ob_wms_rukudanservice.UpdateEntity(_rkd);
                        if(_rkd.JihuaID!=null && _rkd.JihuaID > 0)
                        {
                            var _jhmx = ServiceFactory.cust_rukujihuamxservice.LoadEntities(p => p.JihuaID == _rkd.JihuaID).ToList();
                            foreach(var jhmx in _jhmx)
                            {
                                if (jhmx.JihuaSL - jhmx.DaohuoSL > 0)
                                {
                                    var _rkmx = ServiceFactory.wms_shouhuomxservice.LoadEntities(p =>p.RukuID==_rkd.ID && p.ShangpinDM == jhmx.ShangpinDM && p.IsDelete == false).ToList();
                                    var _rksl = _rkmx.Sum(p => p.ShangjiaSL);
                                    if (_rksl > jhmx.JihuaSL - jhmx.DaohuoSL)
                                        return Json(-4);
                                    jhmx.DaohuoSL = jhmx.DaohuoSL + _rksl;
                                    ServiceFactory.cust_rukujihuamxservice.UpdateEntity(jhmx);
                                }
                            }
                        }
                    }
                    else
                        return Json(-1);
                }
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
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string yanshousf = Request["yanshousf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string rukutm = Request["rukutm"] ?? "";
            string rukuzt = Request["rukuzt"] ?? "";
            string duifangqy = Request["duifangqy"] ?? "";
            string shouhuoren = Request["shouhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string rukudanbh = Request["rukudanbh"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            string zhijiesh = Request["zhijiesh"] ?? "";
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            if (yanshousf.IndexOf("true") >= 0)
                yanshousf = "true";
            if (zhijiesh.IndexOf("true") >= 0)
                zhijiesh = "true";
            try
            {
                wms_rukudan ob_wms_rukudan = new wms_rukudan();
                ob_wms_rukudan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_wms_rukudan.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_wms_rukudan.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                ob_wms_rukudan.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                ob_wms_rukudan.Fahuodizhi = fahuodizhi.Trim();
                ob_wms_rukudan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_wms_rukudan.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                ob_wms_rukudan.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_wms_rukudan.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_wms_rukudan.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_wms_rukudan.YanshouSF = yanshousf == "" ? false : Boolean.Parse(yanshousf);
                ob_wms_rukudan.ChunyunYQ = chunyunyq.Trim();
                ob_wms_rukudan.KehuDH = kehudh.Trim();
                ob_wms_rukudan.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_wms_rukudan.Lianxiren = lianxiren.Trim();
                ob_wms_rukudan.LianxiDH = lianxidh.Trim();
                ob_wms_rukudan.RukuTM = rukutm.Trim();
                ob_wms_rukudan.RukuZT = rukuzt == "" ? 0 : int.Parse(rukuzt);
                ob_wms_rukudan.DuifangQY = duifangqy.Trim();
                ob_wms_rukudan.Shouhuoren = shouhuoren == "" ? 0 : int.Parse(shouhuoren);
                ob_wms_rukudan.Beizhu = beizhu.Trim();
                ob_wms_rukudan.Col1 = col1.Trim();
                ob_wms_rukudan.Col2 = col2.Trim();
                ob_wms_rukudan.Col3 = col3.Trim();
                ob_wms_rukudan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_rukudan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_rukudan.RukudanBH = rukudanbh.Trim();
                ob_wms_rukudan.ZhijieSH = zhijiesh == "" ? false : Boolean.Parse(zhijiesh);
                ob_wms_rukudan.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                ob_wms_rukudan = ob_wms_rukudanservice.AddEntity(ob_wms_rukudan);
                id = ob_wms_rukudan.ID.ToString();
                ViewBag.wms_rukudan = ob_wms_rukudan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OperateList");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_rukudan tempData = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == id && wms_rukudan.IsDelete == false);
            ViewBag.wms_rukudan = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_rukudanViewModel wms_rukudanviewmodel = new wms_rukudanViewModel();
                wms_rukudanviewmodel.ID = tempData.ID;
                wms_rukudanviewmodel.HuozhuID = tempData.HuozhuID;
                wms_rukudanviewmodel.JihuaID = tempData.JihuaID;
                wms_rukudanviewmodel.XinxiLY = tempData.XinxiLY;
                wms_rukudanviewmodel.GongyingshangID = tempData.GongyingshangID;
                wms_rukudanviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                wms_rukudanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                wms_rukudanviewmodel.RukuRQ = tempData.RukuRQ;
                wms_rukudanviewmodel.YewuLX = tempData.YewuLX;
                wms_rukudanviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                wms_rukudanviewmodel.JianguanSF = tempData.JianguanSF;
                wms_rukudanviewmodel.YanshouSF = tempData.YanshouSF;
                wms_rukudanviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                wms_rukudanviewmodel.KehuDH = tempData.KehuDH;
                wms_rukudanviewmodel.KefuID = tempData.KefuID;
                wms_rukudanviewmodel.Lianxiren = tempData.Lianxiren;
                wms_rukudanviewmodel.LianxiDH = tempData.LianxiDH;
                wms_rukudanviewmodel.RukuTM = tempData.RukuTM;
                wms_rukudanviewmodel.RukuZT = tempData.RukuZT;
                wms_rukudanviewmodel.DuifangQY = tempData.DuifangQY;
                wms_rukudanviewmodel.Shouhuoren = tempData.Shouhuoren;
                wms_rukudanviewmodel.Beizhu = tempData.Beizhu;
                wms_rukudanviewmodel.Col1 = tempData.Col1;
                wms_rukudanviewmodel.Col2 = tempData.Col2;
                wms_rukudanviewmodel.Col3 = tempData.Col3;
                wms_rukudanviewmodel.MakeDate = tempData.MakeDate;
                wms_rukudanviewmodel.MakeMan = tempData.MakeMan;
                wms_rukudanviewmodel.RukudanBH = tempData.RukudanBH;
                wms_rukudanviewmodel.ZhijieSH = tempData.ZhijieSH;
                wms_rukudanviewmodel.CangkuID = tempData.CangkuID;
                return View(wms_rukudanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string yanshousf = Request["yanshousf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string rukutm = Request["rukutm"] ?? "";
            string rukuzt = Request["rukuzt"] ?? "";
            string duifangqy = Request["duifangqy"] ?? "";
            string shouhuoren = Request["shouhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string rukudanbh = Request["rukudanbh"] ?? "";
            string zhijiesh = Request["zhijiesh"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            int uid = int.Parse(id);
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            if (yanshousf.IndexOf("true") >= 0)
                yanshousf = "true";
            if (zhijiesh.IndexOf("true") >= 0)
                zhijiesh = "true";
            try
            {
                wms_rukudan p = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                //p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                //p.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                p.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.YanshouSF = yanshousf == "" ? false : Boolean.Parse(yanshousf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.RukuTM = rukutm.Trim();
                p.RukuZT = rukuzt == "" ? 0 : int.Parse(rukuzt);
                p.DuifangQY = duifangqy.Trim();
                p.Shouhuoren = shouhuoren == "" ? 0 : int.Parse(shouhuoren);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.RukudanBH = rukudanbh.Trim();
                p.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                p.ZhijieSH = zhijiesh == "" ? false : Boolean.Parse(zhijiesh);
                ob_wms_rukudanservice.UpdateEntity(p);
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
            wms_rukudan ob_wms_rukudan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_rukudan = ob_wms_rukudanservice.GetEntityById(wms_rukudan => wms_rukudan.ID == id && wms_rukudan.IsDelete == false);
                    ob_wms_rukudan.IsDelete = true;
                    ob_wms_rukudanservice.UpdateEntity(ob_wms_rukudan);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult HistoryOfInExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_index";
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "gongyingshangid":
                            string gongyingshangid = scld[1];
                            string gongyingshangidequal = scld[2];
                            string gongyingshangidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingshangid))
                            {
                                if (gongyingshangidequal.Equals("="))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals(">"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals("<"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                }
                            }
                            break;
                        case "fahuodizhi":
                            string fahuodizhi = scld[1];
                            string fahuodizhiequal = scld[2];
                            string fahuodizhiand = scld[3];
                            if (!string.IsNullOrEmpty(fahuodizhi))
                            {
                                if (fahuodizhiequal.Equals("="))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                }
                                if (fahuodizhiequal.Equals("like"))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID);
            ViewBag.NoPaging = true;
            ViewBag.HistoryOfIn = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "HistoryOfInExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("HistoryOfIn_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult PrintRuKuXiangDan()
        {
            var rkxdid = Request["rkxdid"] ?? "";
            ViewBag.rkxdid = rkxdid;
            return View();
        }
        public JsonResult AddByPurchasePlan()
        {
            int _userid = (int)Session["user_id"];
            var _rkinfo = Request["rk"] ?? "";
            if (string.IsNullOrEmpty(_rkinfo))
                return Json(-1);
            var _rks = _rkinfo.Split(',');
            if (_rks.Count() == 0)
                return Json(-1);
            foreach(var rk in _rks)
            {
                if (rk.Length > 0)
                {
                    cust_rukujihua _rkjh = ServiceFactory.cust_rukujihuaservice.GetEntityById(p => p.ID == int.Parse(rk) && p.IsDelete==false);
                    if (_rkjh != null)
                    {
                        wms_rukudan _rkd = new wms_rukudan();
                        _rkd.BaoshuiSF = false;
                        _rkd.JianguanSF = true;
                        _rkd.KefuID = 0;
                        _rkd.CangkuID = 1;
                        _rkd.ChunyunYQ = MvcApplication.TranCondition[1];// "";
                        _rkd.MakeDate = DateTime.Now;
                        _rkd.RukuRQ = DateTime.Now;
                        _rkd.MakeMan = _userid;
                        _rkd.RukuZT = 1;
                        _rkd.Shouhuoren = 0;
                        _rkd.XinxiLY = 2;
                        _rkd.YanshouSF = true;
                        _rkd.ZhijieSH = false;
                        _rkd.DuifangQY = "";
                        //_rkd.RukuRQ = _rkjh.RukuRQ;

                        _rkd.JihuaID = _rkjh.ID;
                        _rkd.HuozhuID = _rkjh.HuozhuID;
                        _rkd.KehuDH = _rkjh.KehuDH;
                        _rkd.Fahuodizhi = _rkjh.Fahuodizhi;
                        _rkd.GongyingshangID = _rkjh.GongyingshangID;
                        _rkd.LianxiDH = _rkjh.LianxiDH;
                        _rkd.Lianxiren = _rkjh.Lianxiren;
                        _rkd.YewuLX = _rkjh.YewuLX;
                        _rkd.Yunsongdizhi = _rkjh.Yunsongdizhi;
                        _rkd=ob_wms_rukudanservice.AddEntity(_rkd);
                        if(_rkd!=null)
                        {
                            _rkjh.JihuaZT = 2;
                            if (_rkjh.RukudanSL == null)
                                _rkjh.RukudanSL = 1;
                            else
                                _rkjh.RukudanSL = _rkjh.RukudanSL + 1;
                            ServiceFactory.cust_rukujihuaservice.UpdateEntity(_rkjh);
                        }
                    }
                }
            }
            return Json(1);
        }

        [OutputCache(Duration = 30)]
        public ActionResult CustomerInList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_CustomerInList";
            PageMenu.Set("CustomerInList", "wms_rukudan", "客户服务");
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {

                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "gongyingshangid":
                            string gongyingshangid = scld[1];
                            string gongyingshangidequal = scld[2];
                            string gongyingshangidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingshangid))
                            {
                                if (gongyingshangidequal.Equals("="))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals(">"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals("<"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                                }
                            }
                            break;
                        case "fahuodizhi":
                            string fahuodizhi = scld[1];
                            string fahuodizhiequal = scld[2];
                            string fahuodizhiand = scld[3];
                            if (!string.IsNullOrEmpty(fahuodizhi))
                            {
                                if (fahuodizhiequal.Equals("="))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                                }
                                if (fahuodizhiequal.Equals("like"))
                                {
                                    if (fahuodizhiand.Equals("and"))
                                        where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                    else
                                        where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == userid && p.IsDelete == false);
            if (_user == null)
            {
                where = where.And(wms_rukudan => wms_rukudan.ID < 1);
            }
            else
            {
                switch (_user.AccountType)
                {
                    case 100:
                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 200:
                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 300:
                        where = where.And(wms_rukudan => wms_rukudan.KefuID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 0:
                    default:
                        where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                }
            }
            //where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerInList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukudan_CustomerInList";
            string page = "1";
            //rukudanbh
            string rukudanbh = Request["rukudanbh"] ?? "";
            string rukudanbhequal = Request["rukudanbhequal"] ?? "";
            string rukudanbhand = Request["rukudanbhand"] ?? "";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //KehuDH 
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "";
            //gongyingshangid
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string gongyingshangidequal = Request["gongyingshangidequal"] ?? "";
            string gongyingshangidand = Request["gongyingshangidand"] ?? "";
            //fahuodizhi
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string fahuodizhiequal = Request["fahuodizhiequal"] ?? "";
            string fahuodizhiand = Request["fahuodizhiand"] ?? "";
            PageMenu.Set("CustomerInList", "wms_rukudan", "客户服务");
            Expression<Func<wms_rukudan, bool>> where = PredicateExtensionses.True<wms_rukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);
                //fahuodizhi
                if (!string.IsNullOrEmpty(fahuodizhi))
                {
                    if (fahuodizhiequal.Equals("="))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                    }
                    if (fahuodizhiequal.Equals("like"))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                    }
                }
                if (!string.IsNullOrEmpty(fahuodizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", fahuodizhi, fahuodizhiequal, fahuodizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", "", fahuodizhiequal, fahuodizhiand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);
                //fahuodizhi
                if (!string.IsNullOrEmpty(fahuodizhi))
                {
                    if (fahuodizhiequal.Equals("="))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi == fahuodizhi);
                    }
                    if (fahuodizhiequal.Equals("like"))
                    {
                        if (fahuodizhiand.Equals("and"))
                            where = where.And(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                        else
                            where = where.Or(wms_rukudan => wms_rukudan.Fahuodizhi.Contains(fahuodizhi));
                    }
                }
                if (!string.IsNullOrEmpty(fahuodizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", fahuodizhi, fahuodizhiequal, fahuodizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "fahuodizhi", "", fahuodizhiequal, fahuodizhiand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == userid && p.IsDelete == false);
            if (_user == null)
            {
                where = where.And(wms_rukudan => wms_rukudan.ID < 1);
            }
            else
            {
                switch (_user.AccountType)
                {
                    case 100:
                        where = where.And(wms_rukudan => wms_rukudan.HuozhuID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 200:
                        where = where.And(wms_rukudan => wms_rukudan.GongyingshangID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 300:
                        where = where.And(wms_rukudan => wms_rukudan.KefuID == _user.EmployeeID && wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                    case 0:
                    default:
                        where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);
                        break;
                }
            }
            //where = where.And(wms_rukudan => wms_rukudan.IsDelete == false && wms_rukudan.RukuZT > 4);

            var tempData = ob_wms_rukudanservice.LoadSortEntities(where.Compile(), false, wms_rukudan => wms_rukudan.ID).ToPagedList<wms_rukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukudan = tempData;
            return View(tempData);
        }

    }
}
