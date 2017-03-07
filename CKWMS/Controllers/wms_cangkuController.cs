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
    public class wms_cangkuController : Controller
    {
        private Iwms_cangkuService ob_wms_cangkuservice = ServiceFactory.wms_cangkuservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangku_index";
            PageMenu.Set("Index", "wms_cangku", "仓库定义");
            Expression<Func<wms_cangku, bool>> where = PredicateExtensionses.True<wms_cangku>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "jiancheng":
                            string jiancheng = scld[1];
                            string jianchengequal = scld[2];
                            string jianchengand = scld[3];
                            if (!string.IsNullOrEmpty(jiancheng))
                            {
                                if (jianchengequal.Equals("="))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                                    else
                                        where = where.Or(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                                }
                                if (jianchengequal.Equals("like"))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                                    else
                                        where = where.Or(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_cangku => wms_cangku.IsDelete == false);

            var tempData = ob_wms_cangkuservice.LoadSortEntities(where.Compile(), false, wms_cangku => wms_cangku.ID).ToPagedList<wms_cangku>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangku = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangku_index";
            string page = "1";
            string jiancheng = Request["jiancheng"] ?? "";
            string jianchengequal = Request["jianchengequal"] ?? "";
            string jianchengand = Request["jianchengand"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "wms_cangku", "仓库定义");
            Expression<Func<wms_cangku, bool>> where = PredicateExtensionses.True<wms_cangku>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                        else
                            where = where.Or(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Mingcheng == mingcheng);
                        else
                            where = where.Or(wms_cangku => wms_cangku.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(wms_cangku => wms_cangku.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                        else
                            where = where.Or(wms_cangku => wms_cangku.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(wms_cangku => wms_cangku.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Mingcheng == mingcheng);
                        else
                            where = where.Or(wms_cangku => wms_cangku.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(wms_cangku => wms_cangku.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(wms_cangku => wms_cangku.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_cangku => wms_cangku.IsDelete == false);

            var tempData = ob_wms_cangkuservice.LoadSortEntities(where.Compile(), false, wms_cangku => wms_cangku.ID).ToPagedList<wms_cangku>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangku = tempData;
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
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucedz = Request["zhucedz"] ?? "";
            string shouhuodz = Request["shouhuodz"] ?? "";
            string fuzerenid = Request["fuzerenid"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string fapiaott = Request["fapiaott"] ?? "";
            string yinhangzh = Request["yinhangzh"] ?? "";
            string shuihao = Request["shuihao"] ?? "";
            string yyzztp = Request["yyzztp"] ?? "";
            string jyxktp = Request["jyxktp"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_cangku ob_wms_cangku = new wms_cangku();
                ob_wms_cangku.Jiancheng = jiancheng.Trim();
                ob_wms_cangku.Mingcheng = mingcheng.Trim();
                ob_wms_cangku.ZhuceDZ = zhucedz.Trim();
                ob_wms_cangku.ShouhuoDZ = shouhuodz.Trim();
                ob_wms_cangku.FuzerenID = fuzerenid == "" ? 0 : int.Parse(fuzerenid);
                ob_wms_cangku.Fuzeren = fuzeren.Trim();
                ob_wms_cangku.LianxiDH = lianxidh.Trim();
                ob_wms_cangku.FapiaoTT = fapiaott.Trim();
                ob_wms_cangku.YinhangZH = yinhangzh.Trim();
                ob_wms_cangku.Shuihao = shuihao.Trim();
                ob_wms_cangku.YYZZTP = yyzztp.Trim();
                ob_wms_cangku.JYXKTP = jyxktp.Trim();
                ob_wms_cangku.Col1 = col1.Trim();
                ob_wms_cangku.Col2 = col2.Trim();
                ob_wms_cangku.Col3 = col3.Trim();
                ob_wms_cangku.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_cangku.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cangku = ob_wms_cangkuservice.AddEntity(ob_wms_cangku);
                ViewBag.wms_cangku = ob_wms_cangku;
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
            wms_cangku tempData = ob_wms_cangkuservice.GetEntityById(wms_cangku => wms_cangku.ID == id && wms_cangku.IsDelete == false);
            ViewBag.wms_cangku = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_cangkuViewModel wms_cangkuviewmodel = new wms_cangkuViewModel();
                wms_cangkuviewmodel.ID = tempData.ID;
                wms_cangkuviewmodel.Jiancheng = tempData.Jiancheng;
                wms_cangkuviewmodel.Mingcheng = tempData.Mingcheng;
                wms_cangkuviewmodel.ZhuceDZ = tempData.ZhuceDZ;
                wms_cangkuviewmodel.ShouhuoDZ = tempData.ShouhuoDZ;
                wms_cangkuviewmodel.FuzerenID = tempData.FuzerenID;
                wms_cangkuviewmodel.Fuzeren = tempData.Fuzeren;
                wms_cangkuviewmodel.LianxiDH = tempData.LianxiDH;
                wms_cangkuviewmodel.FapiaoTT = tempData.FapiaoTT;
                wms_cangkuviewmodel.YinhangZH = tempData.YinhangZH;
                wms_cangkuviewmodel.Shuihao = tempData.Shuihao;
                wms_cangkuviewmodel.YYZZTP = tempData.YYZZTP;
                wms_cangkuviewmodel.JYXKTP = tempData.JYXKTP;
                wms_cangkuviewmodel.Col1 = tempData.Col1;
                wms_cangkuviewmodel.Col2 = tempData.Col2;
                wms_cangkuviewmodel.Col3 = tempData.Col3;
                wms_cangkuviewmodel.MakeDate = tempData.MakeDate;
                wms_cangkuviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_cangkuviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucedz = Request["zhucedz"] ?? "";
            string shouhuodz = Request["shouhuodz"] ?? "";
            string fuzerenid = Request["fuzerenid"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string fapiaott = Request["fapiaott"] ?? "";
            string yinhangzh = Request["yinhangzh"] ?? "";
            string shuihao = Request["shuihao"] ?? "";
            string yyzztp = Request["yyzztp"] ?? "";
            string jyxktp = Request["jyxktp"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_cangku p = ob_wms_cangkuservice.GetEntityById(wms_cangku => wms_cangku.ID == uid);
                p.Jiancheng = jiancheng.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.ZhuceDZ = zhucedz.Trim();
                p.ShouhuoDZ = shouhuodz.Trim();
                p.FuzerenID = fuzerenid == "" ? 0 : int.Parse(fuzerenid);
                p.Fuzeren = fuzeren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.FapiaoTT = fapiaott.Trim();
                p.YinhangZH = yinhangzh.Trim();
                p.Shuihao = shuihao.Trim();
                p.YYZZTP = yyzztp.Trim();
                p.JYXKTP = jyxktp.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cangkuservice.UpdateEntity(p);
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
            wms_cangku ob_wms_cangku;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_cangku = ob_wms_cangkuservice.GetEntityById(wms_cangku => wms_cangku.ID == id && wms_cangku.IsDelete == false);
                    ob_wms_cangku.IsDelete = true;
                    ob_wms_cangkuservice.UpdateEntity(ob_wms_cangku);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

