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
                                        where = where.And(p => p.Daima == daima);
                                    else
                                        where = where.Or(p => p.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(p => p.Daima.Contains(daima));
                                    else
                                        where = where.Or(p => p.Daima.Contains(daima));
                                }
                            }
                            break;
                        case "jiancheng":
                            string jiancheng = scld[1];
                            string jianchengequal = scld[2];
                            string jianchengand = scld[3];
                            if (!string.IsNullOrEmpty(jiancheng))
                            {
                                if (jianchengequal.Equals("="))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(p => p.Jiancheng == jiancheng);
                                    else
                                        where = where.Or(p => p.Jiancheng == jiancheng);
                                }
                                if (jianchengequal.Equals("like"))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(p => p.Jiancheng.Contains(jiancheng));
                                    else
                                        where = where.Or(p => p.Jiancheng.Contains(jiancheng));
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
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";

            //jiancheng
            string jiancheng = Request["jiancheng"] ?? "";
            string jianchengequal = Request["jianchengequal"] ?? "";
            string jianchengand = Request["jianchengand"] ?? "";
            Expression<Func<quan_gspwtkh, bool>> where = PredicateExtensionses.True<quan_gspwtkh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.Daima == daima);
                        else
                            where = where.Or(p => p.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.Daima.Contains(daima));
                        else
                            where = where.Or(p => p.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                //jiancheng
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(p => p.Jiancheng == jiancheng);
                        else
                            where = where.Or(p => p.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(p => p.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(p => p.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.Daima == daima);
                        else
                            where = where.Or(p => p.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.Daima.Contains(daima));
                        else
                            where = where.Or(p => p.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                //jiancheng
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(p => p.Jiancheng == jiancheng);
                        else
                            where = where.Or(p => p.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(p => p.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(p => p.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
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
            if (tiezwbq.IndexOf("true") > -1)
                tiezwbq = "true";
            else
                tiezwbq = "false";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            if (heyisf.IndexOf("true") > -1)
                heyisf = "true";
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
        public ActionResult AddGSPVendor(int id)
        {
            int _userid = (int)Session["user_id"];
            var _ed = Request["ed"] ?? "0";
            base_weituokehu _wtkh=null;
            quan_gspwtkh tempData=null;
            if (_ed == "1")
                tempData = ob_quan_gspwtkhservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
            else
            {
                _wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
                if (_wtkh == null)
                    return View();
                tempData = ob_quan_gspwtkhservice.GetEntityById(p => p.WTKHID == id && p.IsDelete == false && p.Shouying < 5);
            }
            if (tempData != null)
            {
                base_weituokehuViewModel base_weituokehuviewmodel = new base_weituokehuViewModel();
                base_weituokehuviewmodel.ID = tempData.WTKHID;
                base_weituokehuviewmodel.Daima = tempData.Daima;
                base_weituokehuviewmodel.Jiancheng = tempData.Jiancheng;
                base_weituokehuviewmodel.Kehumingcheng = tempData.Kehumingcheng;
                base_weituokehuviewmodel.Hetongbianhao = tempData.Hetongbianhao;
                base_weituokehuviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                base_weituokehuviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                base_weituokehuviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                base_weituokehuviewmodel.ZuzhijigouBH = tempData.ZuzhijigouBH;
                base_weituokehuviewmodel.ZuzhijigouYXQ = tempData.ZuzhijigouYXQ;
                base_weituokehuviewmodel.ZuzhijigouTP = tempData.ZuzhijigouTP;
                base_weituokehuviewmodel.ShuiwudengjiBH = tempData.ShuiwudengjiBH;
                base_weituokehuviewmodel.ShuiwudengjiYXQ = tempData.ShuiwudengjiYXQ;
                base_weituokehuviewmodel.ShuiwudengjiTP = tempData.ShuiwudengjiTP;
                base_weituokehuviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
                base_weituokehuviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
                base_weituokehuviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
                base_weituokehuviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
                base_weituokehuviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
                base_weituokehuviewmodel.Lianxiren = tempData.Lianxiren;
                base_weituokehuviewmodel.Lianxidianhua = tempData.Lianxidianhua;
                base_weituokehuviewmodel.Beizhu = tempData.Beizhu;
                base_weituokehuviewmodel.Shouying = tempData.Shouying;
                base_weituokehuviewmodel.Col1 = tempData.Col1;
                base_weituokehuviewmodel.Col2 = tempData.Col2;
                base_weituokehuviewmodel.Col3 = tempData.Col3;
                base_weituokehuviewmodel.Col4 = tempData.Col4;
                base_weituokehuviewmodel.Col5 = tempData.Col5;
                base_weituokehuviewmodel.Col6 = tempData.Col6;
                base_weituokehuviewmodel.MakeDate = tempData.MakeDate;
                base_weituokehuviewmodel.MakeMan = tempData.MakeMan;
                base_weituokehuviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_weituokehuviewmodel.HezuoSF = tempData.HezuoSF;
                base_weituokehuviewmodel.BeianBH = tempData.BeianBH;
                base_weituokehuviewmodel.BeianYXQ = tempData.BeianYXQ;
                base_weituokehuviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                base_weituokehuviewmodel.BeianFZJG = tempData.BeianFZJG;
                base_weituokehuviewmodel.BeianTP = tempData.BeianTP;
                base_weituokehuviewmodel.XukePZRQ = tempData.XukePZRQ;
                base_weituokehuviewmodel.XukeFZJG = tempData.XukeFZJG;
                base_weituokehuviewmodel.ZhuceDZ = tempData.ZhuceDZ;
                base_weituokehuviewmodel.JingyinDZ = tempData.JingyinDZ;
                base_weituokehuviewmodel.KufangDZ = tempData.KufangDZ;
                base_weituokehuviewmodel.WeituoNR = tempData.WeituoNR;
                base_weituokehuviewmodel.WeituoKSRQ = tempData.WeituoKSRQ;
                base_weituokehuviewmodel.WeituoJSRQ = tempData.WeituoJSRQ;
                base_weituokehuviewmodel.WeituoQX = tempData.WeituoQX;
                base_weituokehuviewmodel.TieZWBQ = tempData.TieZWBQ;
                base_weituokehuviewmodel.HetongTP = tempData.HetongTP;
                base_weituokehuviewmodel.Faren = tempData.Faren;
                base_weituokehuviewmodel.Fuzeren = tempData.Fuzeren;
                base_weituokehuviewmodel.HeyiSF = tempData.HeyiSF;
                ViewBag.gspkhid = tempData.ID;
                return View(base_weituokehuviewmodel);
            }
            quan_gspwtkh _gspkh = new quan_gspwtkh();
            _gspkh.WTKHID = _wtkh.ID;
            _gspkh.BeianBH = _wtkh.BeianBH;
            _gspkh.BeianFZJG = _wtkh.BeianFZJG;
            _gspkh.BeianPZRQ = _wtkh.BeianPZRQ;
            _gspkh.BeianTP = _wtkh.BeianTP;
            _gspkh.BeianYXQ = _wtkh.BeianYXQ;
            _gspkh.Beizhu = _wtkh.Beizhu;
            _gspkh.Col1 = _wtkh.Col1;
            _gspkh.Col2 = _wtkh.Col2;
            _gspkh.Col3 = _wtkh.Col3;
            _gspkh.Col4 = _wtkh.Col4;
            _gspkh.Col5 = _wtkh.Col5;
            _gspkh.Col6 = _wtkh.Col6;
            _gspkh.Daima = _wtkh.Daima;
            _gspkh.Faren = _wtkh.Faren;
            _gspkh.Fuzeren = _wtkh.Fuzeren;
            _gspkh.Hetongbianhao = _wtkh.Hetongbianhao;
            _gspkh.HetongTP = _wtkh.HetongTP;
            _gspkh.HeyiSF = _wtkh.HeyiSF;
            _gspkh.HezuoSF = _wtkh.HezuoSF;
            _gspkh.Jiancheng = _wtkh.Jiancheng;
            _gspkh.JingyinDZ = _wtkh.JingyinDZ;
            _gspkh.Jingyingfanwei = _wtkh.Jingyingfanwei;
            _gspkh.JingyingfanweiDM = _wtkh.JingyingfanweiDM;
            _gspkh.JingyingxukeBH = _wtkh.JingyingxukeBH;
            _gspkh.JingyingxukeTP = _wtkh.JingyingxukeTP;
            _gspkh.JingyingxukeYXQ = _wtkh.JingyingxukeYXQ;
            _gspkh.Kehumingcheng = _wtkh.Kehumingcheng;
            _gspkh.KufangDZ = _wtkh.KufangDZ;
            _gspkh.Lianxidianhua = _wtkh.Lianxidianhua;
            _gspkh.Lianxiren = _wtkh.Lianxiren;
            _gspkh.MakeDate = DateTime.Now;
            _gspkh.MakeMan = _userid;
            _gspkh.ShenchaSF = _wtkh.ShenchaSF;
            _gspkh.Shouying = 1;
            _gspkh.ShuiwudengjiBH = _wtkh.ShuiwudengjiBH;
            _gspkh.ShuiwudengjiTP = _wtkh.ShuiwudengjiTP;
            _gspkh.ShuiwudengjiYXQ = _wtkh.ShuiwudengjiYXQ;
            _gspkh.TieZWBQ = _wtkh.TieZWBQ;
            _gspkh.WeituoJSRQ = _wtkh.WeituoJSRQ;
            _gspkh.WeituoKSRQ = _wtkh.WeituoKSRQ;
            _gspkh.WeituoNR = _wtkh.WeituoNR;
            _gspkh.WeituoQX = _wtkh.WeituoQX;
            _gspkh.XukeFZJG = _wtkh.XukeFZJG;
            _gspkh.XukePZRQ = _wtkh.XukePZRQ;
            _gspkh.YingyezhizhaoBH = _wtkh.YingyezhizhaoBH;
            _gspkh.YingyezhizhaoTP = _wtkh.YingyezhizhaoTP;
            _gspkh.YingyezhizhaoYXQ = _wtkh.YingyezhizhaoYXQ;
            _gspkh.ZhuceDZ = _wtkh.ZhuceDZ;
            _gspkh.ZuzhijigouBH = _wtkh.ZuzhijigouBH;
            _gspkh.ZuzhijigouTP = _wtkh.ZuzhijigouTP;
            _gspkh.ZuzhijigouYXQ = _wtkh.ZuzhijigouYXQ;
            _gspkh = ob_quan_gspwtkhservice.AddEntity(_gspkh);
            if (_gspkh != null)
            {
                tempData = _gspkh;
                base_weituokehuViewModel base_weituokehuviewmodel = new base_weituokehuViewModel();
                base_weituokehuviewmodel.ID = tempData.WTKHID;
                base_weituokehuviewmodel.Daima = tempData.Daima;
                base_weituokehuviewmodel.Jiancheng = tempData.Jiancheng;
                base_weituokehuviewmodel.Kehumingcheng = tempData.Kehumingcheng;
                base_weituokehuviewmodel.Hetongbianhao = tempData.Hetongbianhao;
                base_weituokehuviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                base_weituokehuviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                base_weituokehuviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                base_weituokehuviewmodel.ZuzhijigouBH = tempData.ZuzhijigouBH;
                base_weituokehuviewmodel.ZuzhijigouYXQ = tempData.ZuzhijigouYXQ;
                base_weituokehuviewmodel.ZuzhijigouTP = tempData.ZuzhijigouTP;
                base_weituokehuviewmodel.ShuiwudengjiBH = tempData.ShuiwudengjiBH;
                base_weituokehuviewmodel.ShuiwudengjiYXQ = tempData.ShuiwudengjiYXQ;
                base_weituokehuviewmodel.ShuiwudengjiTP = tempData.ShuiwudengjiTP;
                base_weituokehuviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
                base_weituokehuviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
                base_weituokehuviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
                base_weituokehuviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
                base_weituokehuviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
                base_weituokehuviewmodel.Lianxiren = tempData.Lianxiren;
                base_weituokehuviewmodel.Lianxidianhua = tempData.Lianxidianhua;
                base_weituokehuviewmodel.Beizhu = tempData.Beizhu;
                base_weituokehuviewmodel.Shouying = tempData.Shouying;
                base_weituokehuviewmodel.Col1 = tempData.Col1;
                base_weituokehuviewmodel.Col2 = tempData.Col2;
                base_weituokehuviewmodel.Col3 = tempData.Col3;
                base_weituokehuviewmodel.Col4 = tempData.Col4;
                base_weituokehuviewmodel.Col5 = tempData.Col5;
                base_weituokehuviewmodel.Col6 = tempData.Col6;
                base_weituokehuviewmodel.MakeDate = tempData.MakeDate;
                base_weituokehuviewmodel.MakeMan = tempData.MakeMan;
                base_weituokehuviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_weituokehuviewmodel.HezuoSF = tempData.HezuoSF;
                base_weituokehuviewmodel.BeianBH = tempData.BeianBH;
                base_weituokehuviewmodel.BeianYXQ = tempData.BeianYXQ;
                base_weituokehuviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                base_weituokehuviewmodel.BeianFZJG = tempData.BeianFZJG;
                base_weituokehuviewmodel.BeianTP = tempData.BeianTP;
                base_weituokehuviewmodel.XukePZRQ = tempData.XukePZRQ;
                base_weituokehuviewmodel.XukeFZJG = tempData.XukeFZJG;
                base_weituokehuviewmodel.ZhuceDZ = tempData.ZhuceDZ;
                base_weituokehuviewmodel.JingyinDZ = tempData.JingyinDZ;
                base_weituokehuviewmodel.KufangDZ = tempData.KufangDZ;
                base_weituokehuviewmodel.WeituoNR = tempData.WeituoNR;
                base_weituokehuviewmodel.WeituoKSRQ = tempData.WeituoKSRQ;
                base_weituokehuviewmodel.WeituoJSRQ = tempData.WeituoJSRQ;
                base_weituokehuviewmodel.WeituoQX = tempData.WeituoQX;
                base_weituokehuviewmodel.TieZWBQ = tempData.TieZWBQ;
                base_weituokehuviewmodel.HetongTP = tempData.HetongTP;
                base_weituokehuviewmodel.Faren = tempData.Faren;
                base_weituokehuviewmodel.Fuzeren = tempData.Fuzeren;
                base_weituokehuviewmodel.HeyiSF = tempData.HeyiSF;
                ViewBag.gspkhid = tempData.ID;
                return View(base_weituokehuviewmodel);
            }
            return View();
        }
    }
}

