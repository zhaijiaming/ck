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
    public class quan_gspgysController : Controller
    {
        private Iquan_gspgysService ob_quan_gspgysservice = ServiceFactory.quan_gspgysservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspgys_index";
            Expression<Func<quan_gspgys, bool>> where = PredicateExtensionses.True<quan_gspgys>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
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
                                        where = where.And(quan_gspgys => quan_gspgys.Daima == daima);
                                    else
                                        where = where.Or(quan_gspgys => quan_gspgys.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                                    else
                                        where = where.Or(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_gspgys => quan_gspgys.IsDelete == false);

            var tempData = ob_quan_gspgysservice.LoadSortEntities(where.Compile(), false, quan_gspgys => quan_gspgys.ID).ToPagedList<quan_gspgys>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspgys = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspgys_index";
            string page = "1";
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            Expression<Func<quan_gspgys, bool>> where = PredicateExtensionses.True<quan_gspgys>();
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
                            where = where.And(quan_gspgys => quan_gspgys.Daima == daima);
                        else
                            where = where.Or(quan_gspgys => quan_gspgys.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                        else
                            where = where.Or(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
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
                            where = where.And(quan_gspgys => quan_gspgys.Daima == daima);
                        else
                            where = where.Or(quan_gspgys => quan_gspgys.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                        else
                            where = where.Or(quan_gspgys => quan_gspgys.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_gspgys => quan_gspgys.IsDelete == false);

            var tempData = ob_quan_gspgysservice.LoadSortEntities(where.Compile(), false, quan_gspgys => quan_gspgys.ID).ToPagedList<quan_gspgys>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspgys = tempData;
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
            string mingcheng = Request["mingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string col5 = Request["col5"] ?? "";
            string col6 = Request["col6"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string gysid = Request["gysid"] ?? "";
            try
            {
                quan_gspgys ob_quan_gspgys = new quan_gspgys();
                ob_quan_gspgys.Daima = daima.Trim();
                ob_quan_gspgys.Mingcheng = mingcheng.Trim();
                ob_quan_gspgys.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_quan_gspgys.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_quan_gspgys.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_quan_gspgys.JingyingxukeBH = jingyingxukebh.Trim();
                ob_quan_gspgys.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_quan_gspgys.JingyingxukeTP = jingyingxuketp.Trim();
                ob_quan_gspgys.Jingyingfanwei = jingyingfanwei.Trim();
                ob_quan_gspgys.JingyingfanweiDM = jingyingfanweidm.Trim();
                ob_quan_gspgys.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_quan_gspgys.Col1 = col1.Trim();
                ob_quan_gspgys.Col2 = col2.Trim();
                ob_quan_gspgys.Col3 = col3.Trim();
                ob_quan_gspgys.Col4 = col4.Trim();
                ob_quan_gspgys.Col5 = col5.Trim();
                ob_quan_gspgys.Col6 = col6.Trim();
                ob_quan_gspgys.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_gspgys.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_gspgys.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_quan_gspgys.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_quan_gspgys.BeianBH = beianbh.Trim();
                ob_quan_gspgys.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_quan_gspgys.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_quan_gspgys.BeianFZJG = beianfzjg.Trim();
                ob_quan_gspgys.BeianTP = beiantp.Trim();
                ob_quan_gspgys.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_quan_gspgys.XukeFZJG = xukefzjg.Trim();
                ob_quan_gspgys.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                ob_quan_gspgys = ob_quan_gspgysservice.AddEntity(ob_quan_gspgys);
                ViewBag.quan_gspgys = ob_quan_gspgys;
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
            quan_gspgys tempData = ob_quan_gspgysservice.GetEntityById(quan_gspgys => quan_gspgys.GYSID == id && quan_gspgys.Shouying<5 && quan_gspgys.IsDelete == false);
            ViewBag.quan_gspgys = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_gspgysViewModel quan_gspgysviewmodel = new quan_gspgysViewModel();
                quan_gspgysviewmodel.ID = tempData.ID;
                quan_gspgysviewmodel.Daima = tempData.Daima;
                quan_gspgysviewmodel.Mingcheng = tempData.Mingcheng;
                quan_gspgysviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                quan_gspgysviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                quan_gspgysviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                quan_gspgysviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
                quan_gspgysviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
                quan_gspgysviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
                quan_gspgysviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
                quan_gspgysviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
                quan_gspgysviewmodel.Shouying = tempData.Shouying;
                quan_gspgysviewmodel.Col1 = tempData.Col1;
                quan_gspgysviewmodel.Col2 = tempData.Col2;
                quan_gspgysviewmodel.Col3 = tempData.Col3;
                quan_gspgysviewmodel.Col4 = tempData.Col4;
                quan_gspgysviewmodel.Col5 = tempData.Col5;
                quan_gspgysviewmodel.Col6 = tempData.Col6;
                quan_gspgysviewmodel.MakeDate = tempData.MakeDate;
                quan_gspgysviewmodel.MakeMan = tempData.MakeMan;
                quan_gspgysviewmodel.ShenchaSF = tempData.ShenchaSF;
                quan_gspgysviewmodel.HezuoSF = tempData.HezuoSF;
                quan_gspgysviewmodel.BeianBH = tempData.BeianBH;
                quan_gspgysviewmodel.BeianYXQ = tempData.BeianYXQ;
                quan_gspgysviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                quan_gspgysviewmodel.BeianFZJG = tempData.BeianFZJG;
                quan_gspgysviewmodel.BeianTP = tempData.BeianTP;
                quan_gspgysviewmodel.XukePZRQ = tempData.XukePZRQ;
                quan_gspgysviewmodel.XukeFZJG = tempData.XukeFZJG;
                quan_gspgysviewmodel.GYSID = tempData.GYSID;
                return View(quan_gspgysviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string col5 = Request["col5"] ?? "";
            string col6 = Request["col6"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string gysid = Request["gysid"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_gspgys p = ob_quan_gspgysservice.GetEntityById(quan_gspgys => quan_gspgys.ID == uid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Jingyingfanwei = jingyingfanwei.Trim();
                p.JingyingfanweiDM = jingyingfanweidm.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.Col4 = col4.Trim();
                p.Col5 = col5.Trim();
                p.Col6 = col6.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                p.BeianBH = beianbh.Trim();
                p.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                p.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                p.BeianFZJG = beianfzjg.Trim();
                p.BeianTP = beiantp.Trim();
                p.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                p.XukeFZJG = xukefzjg.Trim();
                p.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                ob_quan_gspgysservice.UpdateEntity(p);
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
            quan_gspgys ob_quan_gspgys;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_gspgys = ob_quan_gspgysservice.GetEntityById(quan_gspgys => quan_gspgys.ID == id && quan_gspgys.IsDelete == false);
                    ob_quan_gspgys.IsDelete = true;
                    ob_quan_gspgysservice.UpdateEntity(ob_quan_gspgys);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult GSPEdit(int id)
        {
            int _userid = (int)Session["user_id"];
            var _gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
            if (_gys != null)
            {
                quan_gspgys _gsp = new quan_gspgys();
                _gsp.GYSID = _gys.ID;
                _gsp.BeianBH = _gys.BeianBH;
                _gsp.BeianFZJG = _gys.BeianFZJG;
                _gsp.BeianPZRQ = _gys.BeianPZRQ;
                _gsp.BeianTP = _gys.BeianTP;
                _gsp.BeianYXQ = _gys.BeianYXQ;
                _gsp.Col1 = _gys.Col1;
                _gsp.Col2 = _gys.Col2;
                _gsp.Col3 = _gys.Col3;
                _gsp.Col4 = _gys.Col4;
                _gsp.Col5 = _gys.Col5;
                _gsp.Col6 = _gys.Col6;
                _gsp.Daima = _gys.Daima;
                _gsp.HezuoSF = _gys.HezuoSF;
                _gsp.Jingyingfanwei = _gys.Jingyingfanwei;
                _gsp.JingyingfanweiDM = _gys.JingyingfanweiDM;
                _gsp.JingyingxukeBH = _gys.JingyingxukeBH;
                _gsp.JingyingxukeTP = _gys.JingyingxukeTP;
                _gsp.JingyingxukeYXQ = _gys.JingyingxukeYXQ;
                _gsp.MakeMan = _userid;
                _gsp.MakeDate = DateTime.Now;
                _gsp.Mingcheng = _gys.Mingcheng;
                _gsp.ShenchaSF = _gys.ShenchaSF;
                _gsp.Shouying = 1;
                _gsp.XukeFZJG = _gys.XukeFZJG;
                _gsp.XukePZRQ = _gys.XukePZRQ;
                _gsp.YingyezhizhaoBH = _gys.YingyezhizhaoBH;
                _gsp.YingyezhizhaoTP = _gys.YingyezhizhaoTP;
                _gsp.YingyezhizhaoYXQ = _gys.YingyezhizhaoYXQ;
            }
            return View();
        }
    }
}

