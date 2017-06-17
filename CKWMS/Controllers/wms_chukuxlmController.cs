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
    public class wms_chukuxlmController : Controller
    {
        private Iwms_chukuxlmService ob_wms_chukuxlmservice = ServiceFactory.wms_chukuxlmservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";

            PageMenu.Set("Index", "wms_chukuxlm", "仓库操作");

            Expression<Func<wms_outserial_v, bool>> where = PredicateExtensionses.True<wms_outserial_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "Chukudan":
                            string Chukudan = scld[1];
                            string Chukudanequal = scld[2];
                            string Chukudanand = scld[3];
                            if (!string.IsNullOrEmpty(Chukudan))
                            {
                                if (Chukudanequal.Equals("="))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan == Chukudan);
                                    else
                                        where = where.Or(p => p.Chukudan == Chukudan);
                                }
                                if (Chukudanequal.Equals("like"))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan.Contains(Chukudan));
                                    else
                                        where = where.Or(p => p.Chukudan.Contains(Chukudan));
                                }
                            }
                            break;
                        case "Xuliema":
                            string Xuliema = scld[1];
                            string Xuliemaequal = scld[2];
                            string Xuliemaand = scld[3];
                            if (!string.IsNullOrEmpty(Xuliema))
                            {
                                if (Xuliemaequal.Equals("="))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema == Xuliema);
                                    else
                                        where = where.Or(p => p.Xuliema == Xuliema);
                                }
                                if (Xuliemaequal.Equals("like"))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema.Contains(Xuliema));
                                    else
                                        where = where.Or(p => p.Xuliema.Contains(Xuliema));
                                }
                            }
                            break;
                        case "makedate":
                            string makedate = scld[1];
                            string makedateequal = scld[2];
                            string makedateand = scld[3];
                            if (!string.IsNullOrEmpty(makedate))
                            {
                                if (makedateequal.Equals("="))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                                    else
                                        where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                                }
                                if (makedateequal.Equals(">"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                                }
                                if (makedateequal.Equals("<"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //where = where.And(wms_chukuxlm => wms_chukuxlm.IsDelete == false);

            var tempData = ob_wms_chukuxlmservice.GetOutSerialList(where.Compile()).ToPagedList<wms_outserial_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukuxlm = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";
            string page = "1";
            //Chukudan
            string Chukudan = Request["Chukudan"] ?? "";
            string Chukudanequal = Request["Chukudanequal"] ?? "";
            string Chukudanand = Request["Chukudanand"] ?? "";
            //Xuliema
            string Xuliema = Request["Xuliema"] ?? "";
            string Xuliemaequal = Request["Xuliemaequal"] ?? "";
            string Xuliemaand = Request["Xuliemaand"] ?? "";
            //makedate
            string makedate = Request["makedate"] ?? "";
            string makedateequal = Request["makedateequal"] ?? "";
            string makedateand = Request["makedateand"] ?? "";

            PageMenu.Set("Index", "wms_chukuxlm", "仓库操作");

            Expression<Func<wms_outserial_v, bool>> where = PredicateExtensionses.True<wms_outserial_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //Chukudan
                if (!string.IsNullOrEmpty(Chukudan))
                {
                    if (Chukudanequal.Equals("="))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan == Chukudan);
                        else
                            where = where.Or(p => p.Chukudan == Chukudan);
                    }
                    if (Chukudanequal.Equals("like"))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan.Contains(Chukudan));
                        else
                            where = where.Or(p => p.Chukudan.Contains(Chukudan));
                    }
                }
                if (!string.IsNullOrEmpty(Chukudan))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", Chukudan, Chukudanequal, Chukudanand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", "", Chukudanequal, Chukudanand);
                //Xuliema
                if (!string.IsNullOrEmpty(Xuliema))
                {
                    if (Xuliemaequal.Equals("="))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema == Xuliema);
                        else
                            where = where.Or(p => p.Xuliema == Xuliema);
                    }
                    if (Xuliemaequal.Equals("like"))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema.Contains(Xuliema));
                        else
                            where = where.Or(p => p.Xuliema.Contains(Xuliema));
                    }
                }
                if (!string.IsNullOrEmpty(Xuliema))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", Xuliema, Xuliemaequal, Xuliemaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", "", Xuliemaequal, Xuliemaand);
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //Chukudan
                if (!string.IsNullOrEmpty(Chukudan))
                {
                    if (Chukudanequal.Equals("="))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan == Chukudan);
                        else
                            where = where.Or(p => p.Chukudan == Chukudan);
                    }
                    if (Chukudanequal.Equals("like"))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan.Contains(Chukudan));
                        else
                            where = where.Or(p => p.Chukudan.Contains(Chukudan));
                    }
                }
                if (!string.IsNullOrEmpty(Chukudan))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", Chukudan, Chukudanequal, Chukudanand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", "", Chukudanequal, Chukudanand);
                //Xuliema
                if (!string.IsNullOrEmpty(Xuliema))
                {
                    if (Xuliemaequal.Equals("="))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema == Xuliema);
                        else
                            where = where.Or(p => p.Xuliema == Xuliema);
                    }
                    if (Xuliemaequal.Equals("like"))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema.Contains(Xuliema));
                        else
                            where = where.Or(p => p.Xuliema.Contains(Xuliema));
                    }
                }
                if (!string.IsNullOrEmpty(Xuliema))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", Xuliema, Xuliemaequal, Xuliemaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", "", Xuliemaequal, Xuliemaand);
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            //where = where.And(wms_chukuxlm => wms_chukuxlm.IsDelete == false);

            var tempData = ob_wms_chukuxlmservice.GetOutSerialList(where.Compile()).ToPagedList<wms_outserial_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukuxlm = tempData;
            return View(tempData);
        }
        public JsonResult AddXLM()
        {
            int _userid = (int)Session["user_id"];
            var _xlm = Request["xlm"] ?? "";
            var _ck = Request["ck"] ?? "";
            var _ckd = Request["ckd"] ?? "";
            var _ph = Request["ph"] ?? "";
            if (_xlm == "" || _ck == "" || _ckd == "")
                return Json(-1);
            var _ckdxx = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ck) && p.IsDelete == false);
            if (_ckdxx == null)
                return Json(-2);
            if (_ckdxx.JihuaZT > 3)
                return Json(-3);
            //ServiceFactory.wms_chukudanservice.UploadU8(_ckdxx.ChukudanBH);
            List<wms_chukuxlm> _ckxlms = ob_wms_chukuxlmservice.LoadEntities(p => p.ChukuID == int.Parse(_ck) && p.IsDelete == false).ToList();
            string[] _xlms = _xlm.Split();
            foreach (var _m in _xlms.Distinct())
            {
                if (_m.Length > 0)
                {
                    //wms_chukuxlm _pm = ob_wms_chukuxlmservice.GetEntityById(p => p.ChukuID == int.Parse(_ck) && p.Xuliema == _m && p.IsDelete == false);
                    var _allreadyhas = false;
                    foreach (wms_chukuxlm ckxlm in _ckxlms)
                    {
                        if (_m == ckxlm.Xuliema)
                        {
                            _allreadyhas = true;
                            break;
                        }
                    }
                    if (!_allreadyhas)
                    {
                        wms_chukuxlm _nm = new wms_chukuxlm();
                        _nm.Chukudan = _ckd;
                        _nm.ChukuID = int.Parse(_ck);
                        _nm.MakeDate = DateTime.Now;
                        _nm.MakeMan = _userid;
                        _nm.Xuliema = _m;
                        _nm.Pihao = _ph;
                        _nm = ob_wms_chukuxlmservice.AddEntity(_nm);
                    }
                }
            }
            _ckdxx.JihuaZT = -12;
            ServiceFactory.wms_chukudanservice.UpdateEntity(_ckdxx);
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
            string chukuid = Request["chukuid"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudan = Request["chukudan"] ?? "";
            try
            {
                wms_chukuxlm ob_wms_chukuxlm = new wms_chukuxlm();
                ob_wms_chukuxlm.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_wms_chukuxlm.Xuliema = xuliema.Trim();
                ob_wms_chukuxlm.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukuxlm.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukuxlm.Chukudan = chukudan.Trim();
                ob_wms_chukuxlm = ob_wms_chukuxlmservice.AddEntity(ob_wms_chukuxlm);
                ViewBag.wms_chukuxlm = ob_wms_chukuxlm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration = 30)]
        public ActionResult SerialExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";
            Expression<Func<wms_outserial_v, bool>> where = PredicateExtensionses.True<wms_outserial_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "Chukudan":
                            string Chukudan = scld[1];
                            string Chukudanequal = scld[2];
                            string Chukudanand = scld[3];
                            if (!string.IsNullOrEmpty(Chukudan))
                            {
                                if (Chukudanequal.Equals("="))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan == Chukudan);
                                    else
                                        where = where.Or(p => p.Chukudan == Chukudan);
                                }
                                if (Chukudanequal.Equals("like"))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan.Contains(Chukudan));
                                    else
                                        where = where.Or(p => p.Chukudan.Contains(Chukudan));
                                }
                            }
                            break;
                        case "Xuliema":
                            string Xuliema = scld[1];
                            string Xuliemaequal = scld[2];
                            string Xuliemaand = scld[3];
                            if (!string.IsNullOrEmpty(Xuliema))
                            {
                                if (Xuliemaequal.Equals("="))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema == Xuliema);
                                    else
                                        where = where.Or(p => p.Xuliema == Xuliema);
                                }
                                if (Xuliemaequal.Equals("like"))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema.Contains(Xuliema));
                                    else
                                        where = where.Or(p => p.Xuliema.Contains(Xuliema));
                                }
                            }
                            break;
                        case "makedate":
                            string makedate = scld[1];
                            string makedateequal = scld[2];
                            string makedateand = scld[3];
                            if (!string.IsNullOrEmpty(makedate))
                            {
                                if (makedateequal.Equals("="))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                                    else
                                        where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate || p.MakeDate.ToString("yyyy/MM/dd") == makedate || p.MakeDate.ToString("yyyy.MM.dd") == makedate);
                                }
                                if (makedateequal.Equals(">"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                                }
                                if (makedateequal.Equals("<"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_wms_chukuxlmservice.GetOutSerialList(where.Compile()).ToList();
            ViewBag.wms_chukuxlm = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "SerialExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("SerialNumber_{0}.xls", DateTime.Now.ToShortDateString()));

        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_chukuxlm tempData = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == id && wms_chukuxlm.IsDelete == false);
            ViewBag.wms_chukuxlm = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukuxlmViewModel wms_chukuxlmviewmodel = new wms_chukuxlmViewModel();
                wms_chukuxlmviewmodel.ID = tempData.ID;
                wms_chukuxlmviewmodel.ChukuID = tempData.ChukuID;
                wms_chukuxlmviewmodel.Xuliema = tempData.Xuliema;
                wms_chukuxlmviewmodel.MakeDate = tempData.MakeDate;
                wms_chukuxlmviewmodel.MakeMan = tempData.MakeMan;
                wms_chukuxlmviewmodel.Chukudan = tempData.Chukudan;
                return View(wms_chukuxlmviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudan = Request["chukudan"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_chukuxlm p = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == uid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.Xuliema = xuliema.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Chukudan = chukudan.Trim();
                ob_wms_chukuxlmservice.UpdateEntity(p);
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
            wms_chukuxlm ob_wms_chukuxlm;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_chukuxlm = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == id && wms_chukuxlm.IsDelete == false);
                    ob_wms_chukuxlm.IsDelete = true;
                    ob_wms_chukuxlmservice.UpdateEntity(ob_wms_chukuxlm);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetXLM()
        {
            int _userid = (int)Session["user_id"];
            var _ckdid = Request["ckd"] ?? "";

            if (string.IsNullOrEmpty(_ckdid))
                return Json(-1);
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckdid) && p.IsDelete == false);
            if (_ckd == null)
                return Json(-2);
            if (_ckd.JihuaZT > 4)
                return Json(-3);
            var _xlms = ob_wms_chukuxlmservice.LoadEntities(p => p.ChukuID == _ckd.ID && p.IsDelete == false).ToList();
            if (_xlms.Count == 0)
                return Json("");
            return Json(_xlms);
        }
    }
}

