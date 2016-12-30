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
    public class quan_gspwtkhController : Controller
    {
        private Iquan_gspwtkhService ob_quan_gspwtkhservice = ServiceFactory.quan_gspwtkhservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspwtkh_index";
            Expression<Func<quan_gspwtkh, bool>> where = PredicateExtensionses.True<quan_gspwtkh>();
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
                                        where = where.And(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                                    else
                                        where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
                                    else
                                        where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_gspwtkh => quan_gspwtkh.IsDelete == false);

            var tempData = ob_quan_gspwtkhservice.LoadSortEntities(where.Compile(), false, quan_gspwtkh => quan_gspwtkh.ID).ToPagedList<quan_gspwtkh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspwtkh = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspwtkh_index";
            string page = "1";
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            Expression<Func<quan_gspwtkh, bool>> where = PredicateExtensionses.True<quan_gspwtkh>();
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
                            where = where.And(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                        else
                            where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
                        else
                            where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
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
                            where = where.And(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                        else
                            where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
                        else
                            where = where.Or(quan_gspwtkh => quan_gspwtkh.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_gspwtkh => quan_gspwtkh.IsDelete == false);

            var tempData = ob_quan_gspwtkhservice.LoadSortEntities(where.Compile(), false, quan_gspwtkh => quan_gspwtkh.ID).ToPagedList<quan_gspwtkh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspwtkh = tempData;
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
            string jiancheng = Request["jiancheng"] ?? "";
            string kehumingcheng = Request["kehumingcheng"] ?? "";
            string hetongbianhao = Request["hetongbianhao"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string zuzhijigoubh = Request["zuzhijigoubh"] ?? "";
            string zuzhijigouyxq = Request["zuzhijigouyxq"] ?? "";
            string zuzhijigoutp = Request["zuzhijigoutp"] ?? "";
            string shuiwudengjibh = Request["shuiwudengjibh"] ?? "";
            string shuiwudengjiyxq = Request["shuiwudengjiyxq"] ?? "";
            string shuiwudengjitp = Request["shuiwudengjitp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
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
            string zhucedz = Request["zhucedz"] ?? "";
            string jingyindz = Request["jingyindz"] ?? "";
            string kufangdz = Request["kufangdz"] ?? "";
            string weituonr = Request["weituonr"] ?? "";
            string weituoksrq = Request["weituoksrq"] ?? "";
            string weituojsrq = Request["weituojsrq"] ?? "";
            string weituoqx = Request["weituoqx"] ?? "";
            string tiezwbq = Request["tiezwbq"] ?? "";
            string hetongtp = Request["hetongtp"] ?? "";
            string faren = Request["faren"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string heyisf = Request["heyisf"] ?? "";
            string wtkhid = Request["wtkhid"] ?? "";
            try
            {
                quan_gspwtkh ob_quan_gspwtkh = new quan_gspwtkh();
                ob_quan_gspwtkh.Daima = daima.Trim();
                ob_quan_gspwtkh.Jiancheng = jiancheng.Trim();
                ob_quan_gspwtkh.Kehumingcheng = kehumingcheng.Trim();
                ob_quan_gspwtkh.Hetongbianhao = hetongbianhao.Trim();
                ob_quan_gspwtkh.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_quan_gspwtkh.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_quan_gspwtkh.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_quan_gspwtkh.ZuzhijigouBH = zuzhijigoubh.Trim();
                ob_quan_gspwtkh.ZuzhijigouYXQ = zuzhijigouyxq == "" ? DateTime.Now : DateTime.Parse(zuzhijigouyxq);
                ob_quan_gspwtkh.ZuzhijigouTP = zuzhijigoutp.Trim();
                ob_quan_gspwtkh.ShuiwudengjiBH = shuiwudengjibh.Trim();
                ob_quan_gspwtkh.ShuiwudengjiYXQ = shuiwudengjiyxq == "" ? DateTime.Now : DateTime.Parse(shuiwudengjiyxq);
                ob_quan_gspwtkh.ShuiwudengjiTP = shuiwudengjitp.Trim();
                ob_quan_gspwtkh.JingyingxukeBH = jingyingxukebh.Trim();
                ob_quan_gspwtkh.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_quan_gspwtkh.JingyingxukeTP = jingyingxuketp.Trim();
                ob_quan_gspwtkh.Jingyingfanwei = jingyingfanwei.Trim();
                ob_quan_gspwtkh.JingyingfanweiDM = jingyingfanweidm.Trim();
                ob_quan_gspwtkh.Lianxiren = lianxiren.Trim();
                ob_quan_gspwtkh.Lianxidianhua = lianxidianhua.Trim();
                ob_quan_gspwtkh.Beizhu = beizhu.Trim();
                ob_quan_gspwtkh.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_quan_gspwtkh.Col1 = col1.Trim();
                ob_quan_gspwtkh.Col2 = col2.Trim();
                ob_quan_gspwtkh.Col3 = col3.Trim();
                ob_quan_gspwtkh.Col4 = col4.Trim();
                ob_quan_gspwtkh.Col5 = col5.Trim();
                ob_quan_gspwtkh.Col6 = col6.Trim();
                ob_quan_gspwtkh.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_gspwtkh.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_gspwtkh.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_quan_gspwtkh.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_quan_gspwtkh.BeianBH = beianbh.Trim();
                ob_quan_gspwtkh.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_quan_gspwtkh.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_quan_gspwtkh.BeianFZJG = beianfzjg.Trim();
                ob_quan_gspwtkh.BeianTP = beiantp.Trim();
                ob_quan_gspwtkh.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_quan_gspwtkh.XukeFZJG = xukefzjg.Trim();
                ob_quan_gspwtkh.ZhuceDZ = zhucedz.Trim();
                ob_quan_gspwtkh.JingyinDZ = jingyindz.Trim();
                ob_quan_gspwtkh.KufangDZ = kufangdz.Trim();
                ob_quan_gspwtkh.WeituoNR = weituonr.Trim();
                ob_quan_gspwtkh.WeituoKSRQ = weituoksrq == "" ? DateTime.Now : DateTime.Parse(weituoksrq);
                ob_quan_gspwtkh.WeituoJSRQ = weituojsrq == "" ? DateTime.Now : DateTime.Parse(weituojsrq);
                ob_quan_gspwtkh.WeituoQX = weituoqx == "" ? 0 : int.Parse(weituoqx);
                ob_quan_gspwtkh.TieZWBQ = tiezwbq == "" ? false : Boolean.Parse(tiezwbq);
                ob_quan_gspwtkh.HetongTP = hetongtp.Trim();
                ob_quan_gspwtkh.Faren = faren.Trim();
                ob_quan_gspwtkh.Fuzeren = fuzeren.Trim();
                ob_quan_gspwtkh.HeyiSF = heyisf == "" ? false : Boolean.Parse(heyisf);
                ob_quan_gspwtkh.WTKHID = wtkhid == "" ? 0 : int.Parse(wtkhid);
                ob_quan_gspwtkh = ob_quan_gspwtkhservice.AddEntity(ob_quan_gspwtkh);
                ViewBag.quan_gspwtkh = ob_quan_gspwtkh;
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
            quan_gspwtkh tempData = ob_quan_gspwtkhservice.GetEntityById(quan_gspwtkh => quan_gspwtkh.ID == id && quan_gspwtkh.IsDelete == false);
            ViewBag.quan_gspwtkh = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_gspwtkhViewModel quan_gspwtkhviewmodel = new quan_gspwtkhViewModel();
                quan_gspwtkhviewmodel.ID = tempData.ID;
                quan_gspwtkhviewmodel.Daima = tempData.Daima;
                quan_gspwtkhviewmodel.Jiancheng = tempData.Jiancheng;
                quan_gspwtkhviewmodel.Kehumingcheng = tempData.Kehumingcheng;
                quan_gspwtkhviewmodel.Hetongbianhao = tempData.Hetongbianhao;
                quan_gspwtkhviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                quan_gspwtkhviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                quan_gspwtkhviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                quan_gspwtkhviewmodel.ZuzhijigouBH = tempData.ZuzhijigouBH;
                quan_gspwtkhviewmodel.ZuzhijigouYXQ = tempData.ZuzhijigouYXQ;
                quan_gspwtkhviewmodel.ZuzhijigouTP = tempData.ZuzhijigouTP;
                quan_gspwtkhviewmodel.ShuiwudengjiBH = tempData.ShuiwudengjiBH;
                quan_gspwtkhviewmodel.ShuiwudengjiYXQ = tempData.ShuiwudengjiYXQ;
                quan_gspwtkhviewmodel.ShuiwudengjiTP = tempData.ShuiwudengjiTP;
                quan_gspwtkhviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
                quan_gspwtkhviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
                quan_gspwtkhviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
                quan_gspwtkhviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
                quan_gspwtkhviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
                quan_gspwtkhviewmodel.Lianxiren = tempData.Lianxiren;
                quan_gspwtkhviewmodel.Lianxidianhua = tempData.Lianxidianhua;
                quan_gspwtkhviewmodel.Beizhu = tempData.Beizhu;
                quan_gspwtkhviewmodel.Shouying = tempData.Shouying;
                quan_gspwtkhviewmodel.Col1 = tempData.Col1;
                quan_gspwtkhviewmodel.Col2 = tempData.Col2;
                quan_gspwtkhviewmodel.Col3 = tempData.Col3;
                quan_gspwtkhviewmodel.Col4 = tempData.Col4;
                quan_gspwtkhviewmodel.Col5 = tempData.Col5;
                quan_gspwtkhviewmodel.Col6 = tempData.Col6;
                quan_gspwtkhviewmodel.MakeDate = tempData.MakeDate;
                quan_gspwtkhviewmodel.MakeMan = tempData.MakeMan;
                quan_gspwtkhviewmodel.ShenchaSF = tempData.ShenchaSF;
                quan_gspwtkhviewmodel.HezuoSF = tempData.HezuoSF;
                quan_gspwtkhviewmodel.BeianBH = tempData.BeianBH;
                quan_gspwtkhviewmodel.BeianYXQ = tempData.BeianYXQ;
                quan_gspwtkhviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                quan_gspwtkhviewmodel.BeianFZJG = tempData.BeianFZJG;
                quan_gspwtkhviewmodel.BeianTP = tempData.BeianTP;
                quan_gspwtkhviewmodel.XukePZRQ = tempData.XukePZRQ;
                quan_gspwtkhviewmodel.XukeFZJG = tempData.XukeFZJG;
                quan_gspwtkhviewmodel.ZhuceDZ = tempData.ZhuceDZ;
                quan_gspwtkhviewmodel.JingyinDZ = tempData.JingyinDZ;
                quan_gspwtkhviewmodel.KufangDZ = tempData.KufangDZ;
                quan_gspwtkhviewmodel.WeituoNR = tempData.WeituoNR;
                quan_gspwtkhviewmodel.WeituoKSRQ = tempData.WeituoKSRQ;
                quan_gspwtkhviewmodel.WeituoJSRQ = tempData.WeituoJSRQ;
                quan_gspwtkhviewmodel.WeituoQX = tempData.WeituoQX;
                quan_gspwtkhviewmodel.TieZWBQ = tempData.TieZWBQ;
                quan_gspwtkhviewmodel.HetongTP = tempData.HetongTP;
                quan_gspwtkhviewmodel.Faren = tempData.Faren;
                quan_gspwtkhviewmodel.Fuzeren = tempData.Fuzeren;
                quan_gspwtkhviewmodel.HeyiSF = tempData.HeyiSF;
                quan_gspwtkhviewmodel.WTKHID = tempData.WTKHID;
                return View(quan_gspwtkhviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string jiancheng = Request["jiancheng"] ?? "";
            string kehumingcheng = Request["kehumingcheng"] ?? "";
            string hetongbianhao = Request["hetongbianhao"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string zuzhijigoubh = Request["zuzhijigoubh"] ?? "";
            string zuzhijigouyxq = Request["zuzhijigouyxq"] ?? "";
            string zuzhijigoutp = Request["zuzhijigoutp"] ?? "";
            string shuiwudengjibh = Request["shuiwudengjibh"] ?? "";
            string shuiwudengjiyxq = Request["shuiwudengjiyxq"] ?? "";
            string shuiwudengjitp = Request["shuiwudengjitp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
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
            string zhucedz = Request["zhucedz"] ?? "";
            string jingyindz = Request["jingyindz"] ?? "";
            string kufangdz = Request["kufangdz"] ?? "";
            string weituonr = Request["weituonr"] ?? "";
            string weituoksrq = Request["weituoksrq"] ?? "";
            string weituojsrq = Request["weituojsrq"] ?? "";
            string weituoqx = Request["weituoqx"] ?? "";
            string tiezwbq = Request["tiezwbq"] ?? "";
            string hetongtp = Request["hetongtp"] ?? "";
            string faren = Request["faren"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string heyisf = Request["heyisf"] ?? "";
            string wtkhid = Request["wtkhid"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_gspwtkh p = ob_quan_gspwtkhservice.GetEntityById(quan_gspwtkh => quan_gspwtkh.ID == uid);
                p.Daima = daima.Trim();
                p.Jiancheng = jiancheng.Trim();
                p.Kehumingcheng = kehumingcheng.Trim();
                p.Hetongbianhao = hetongbianhao.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.ZuzhijigouBH = zuzhijigoubh.Trim();
                p.ZuzhijigouYXQ = zuzhijigouyxq == "" ? DateTime.Now : DateTime.Parse(zuzhijigouyxq);
                p.ZuzhijigouTP = zuzhijigoutp.Trim();
                p.ShuiwudengjiBH = shuiwudengjibh.Trim();
                p.ShuiwudengjiYXQ = shuiwudengjiyxq == "" ? DateTime.Now : DateTime.Parse(shuiwudengjiyxq);
                p.ShuiwudengjiTP = shuiwudengjitp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Jingyingfanwei = jingyingfanwei.Trim();
                p.JingyingfanweiDM = jingyingfanweidm.Trim();
                p.Lianxiren = lianxiren.Trim();
                p.Lianxidianhua = lianxidianhua.Trim();
                p.Beizhu = beizhu.Trim();
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
                p.ZhuceDZ = zhucedz.Trim();
                p.JingyinDZ = jingyindz.Trim();
                p.KufangDZ = kufangdz.Trim();
                p.WeituoNR = weituonr.Trim();
                p.WeituoKSRQ = weituoksrq == "" ? DateTime.Now : DateTime.Parse(weituoksrq);
                p.WeituoJSRQ = weituojsrq == "" ? DateTime.Now : DateTime.Parse(weituojsrq);
                p.WeituoQX = weituoqx == "" ? 0 : int.Parse(weituoqx);
                p.TieZWBQ = tiezwbq == "" ? false : Boolean.Parse(tiezwbq);
                p.HetongTP = hetongtp.Trim();
                p.Faren = faren.Trim();
                p.Fuzeren = fuzeren.Trim();
                p.HeyiSF = heyisf == "" ? false : Boolean.Parse(heyisf);
                p.WTKHID = wtkhid == "" ? 0 : int.Parse(wtkhid);
                ob_quan_gspwtkhservice.UpdateEntity(p);
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
            quan_gspwtkh ob_quan_gspwtkh;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_gspwtkh = ob_quan_gspwtkhservice.GetEntityById(quan_gspwtkh => quan_gspwtkh.ID == id && quan_gspwtkh.IsDelete == false);
                    ob_quan_gspwtkh.IsDelete = true;
                    ob_quan_gspwtkhservice.UpdateEntity(ob_quan_gspwtkh);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

