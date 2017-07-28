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
    public class quan_gspspxxController : Controller
    {
        private Iquan_gspspxxService ob_quan_gspspxxservice = ServiceFactory.quan_gspspxxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspspxx_index";
            Expression<Func<quan_gspspxx, bool>> where = PredicateExtensionses.True<quan_gspspxx>();
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
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
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
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(p => p.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(p => p.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "shouying":
                            string shouying = scld[1];
                            string shouyingequal = scld[2];
                            string shouyingand = scld[3];
                            if (shouying == "0")
                            {
                                shouying = "";
                            }
                            if (!string.IsNullOrEmpty(shouying))
                            {
                                if (shouyingequal.Equals("="))
                                {
                                    if (shouyingand.Equals("and"))
                                        where = where.And(p => p.Shouying == int.Parse(shouying));
                                    else
                                        where = where.Or(p => p.Shouying == int.Parse(shouying));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_gspspxx => quan_gspspxx.IsDelete == false);

            var tempData = ob_quan_gspspxxservice.LoadSortEntities(where.Compile(), false, quan_gspspxx => quan_gspspxx.ID).ToPagedList<quan_gspspxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspspxx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspspxx_index";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            //shouying
            string shouying = Request["shouying"] ?? "";
            string shouyingequal = Request["shouyingequal"] ?? "";
            string shouyingand = Request["shouyingand"] ?? "";
            if (shouying == "0")
            {
                shouying = "";
            }

            Expression<Func<quan_gspspxx, bool>> where = PredicateExtensionses.True<quan_gspspxx>();
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
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.Daima == daima);
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.Daima.Contains(daima));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.Mingcheng == mingcheng);
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //shouying
                if (!string.IsNullOrEmpty(shouying))
                {
                    if (shouyingequal.Equals("="))
                    {
                        if (shouyingand.Equals("and"))
                            where = where.And(p => p.Shouying == int.Parse(shouying));
                        else
                            where = where.Or(p => p.Shouying == int.Parse(shouying));
                    }
                }
                if (!string.IsNullOrEmpty(shouying))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);

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
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

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
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng == mingcheng);
                        else
                            where = where.Or(p => p.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(p => p.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(p => p.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                //shouying
                if (!string.IsNullOrEmpty(shouying))
                {
                    if (shouyingequal.Equals("="))
                    {
                        if (shouyingand.Equals("and"))
                            where = where.And(p => p.Shouying == int.Parse(shouying));
                        else
                            where = where.Or(p => p.Shouying == int.Parse(shouying));
                    }
                }
                if (!string.IsNullOrEmpty(shouying))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_gspspxx => quan_gspspxx.IsDelete == false);

            var tempData = ob_quan_gspspxxservice.LoadSortEntities(where.Compile(), false, quan_gspspxx => quan_gspspxx.ID).ToPagedList<quan_gspspxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspspxx = tempData;
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
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhusqid = Request["huozhusqid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucezhengid = Request["zhucezhengid"] ?? "";
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string guige = Request["guige"] ?? "";
            string xinghao = Request["xinghao"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string volchang = Request["volchang"] ?? "";
            string volkuan = Request["volkuan"] ?? "";
            string volgao = Request["volgao"] ?? "";
            string chanpinxian = Request["chanpinxian"] ?? "";
            string muluxuhao = Request["muluxuhao"] ?? "";
            string guanlifenlei = Request["guanlifenlei"] ?? "";
            string baozhuangyaoqiu = Request["baozhuangyaoqiu"] ?? "";
            string cunchutiaojian = Request["cunchutiaojian"] ?? "";
            string qiyeid = Request["qiyeid"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingsqid = Request["gongyingsqid"] ?? "";
            string gongyingxsid = Request["gongyingxsid"] ?? "";
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
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shangpinms = Request["shangpinms"] ?? "";
            string spid = Request["spid"] ?? "";
            try
            {
                quan_gspspxx ob_quan_gspspxx = new quan_gspspxx();
                ob_quan_gspspxx.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_quan_gspspxx.HuozhuSQID = huozhusqid == "" ? 0 : int.Parse(huozhusqid);
                ob_quan_gspspxx.Daima = daima.Trim();
                ob_quan_gspspxx.Mingcheng = mingcheng.Trim();
                ob_quan_gspspxx.ZhucezhengID = zhucezhengid == "" ? 0 : int.Parse(zhucezhengid);
                ob_quan_gspspxx.ZhucezhengBH = zhucezhengbh.Trim();
                ob_quan_gspspxx.Guige = guige.Trim();
                ob_quan_gspspxx.Xinghao = xinghao.Trim();
                ob_quan_gspspxx.Danwei = danwei.Trim();
                ob_quan_gspspxx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_quan_gspspxx.Volchang = volchang == "" ? 0 : float.Parse(volchang);
                ob_quan_gspspxx.Volkuan = volkuan == "" ? 0 : float.Parse(volkuan);
                ob_quan_gspspxx.Volgao = volgao == "" ? 0 : float.Parse(volgao);
                ob_quan_gspspxx.Chanpinxian = chanpinxian == "" ? 0 : int.Parse(chanpinxian);
                ob_quan_gspspxx.Muluxuhao = muluxuhao == "" ? 0 : int.Parse(muluxuhao);
                ob_quan_gspspxx.Guanlifenlei = guanlifenlei == "" ? 0 : int.Parse(guanlifenlei);
                ob_quan_gspspxx.Baozhuangyaoqiu = baozhuangyaoqiu.Trim();
                ob_quan_gspspxx.Cunchutiaojian = cunchutiaojian.Trim();
                ob_quan_gspspxx.QiyeID = qiyeid == "" ? 0 : int.Parse(qiyeid);
                ob_quan_gspspxx.Qiyemingcheng = qiyemingcheng.Trim();
                ob_quan_gspspxx.GongyingID = gongyingid == "" ? 0 : int.Parse(gongyingid);
                ob_quan_gspspxx.GongyingSQID = gongyingsqid == "" ? 0 : int.Parse(gongyingsqid);
                ob_quan_gspspxx.GongyingXSID = gongyingxsid == "" ? 0 : int.Parse(gongyingxsid);
                ob_quan_gspspxx.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_quan_gspspxx.Col1 = col1.Trim();
                ob_quan_gspspxx.Col2 = col2.Trim();
                ob_quan_gspspxx.Col3 = col3.Trim();
                ob_quan_gspspxx.Col4 = col4.Trim();
                ob_quan_gspspxx.Col5 = col5.Trim();
                ob_quan_gspspxx.Col6 = col6.Trim();
                ob_quan_gspspxx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_gspspxx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_gspspxx.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_quan_gspspxx.JingyinSF = jingyinsf == "" ? false : Boolean.Parse(jingyinsf);
                ob_quan_gspspxx.BaozhuangDW = baozhuangdw.Trim();
                ob_quan_gspspxx.ShangpinTM = shangpintm.Trim();
                ob_quan_gspspxx.Chandi = chandi.Trim();
                ob_quan_gspspxx.ShangpinMS = shangpinms.Trim();
                ob_quan_gspspxx.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_quan_gspspxx = ob_quan_gspspxxservice.AddEntity(ob_quan_gspspxx);
                ViewBag.quan_gspspxx = ob_quan_gspspxx;
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
            quan_gspspxx tempData = ob_quan_gspspxxservice.GetEntityById(quan_gspspxx => quan_gspspxx.ID == id && quan_gspspxx.IsDelete == false);
            ViewBag.quan_gspspxx = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_gspspxxViewModel quan_gspspxxviewmodel = new quan_gspspxxViewModel();
                quan_gspspxxviewmodel.ID = tempData.ID;
                quan_gspspxxviewmodel.HuozhuID = tempData.HuozhuID;
                quan_gspspxxviewmodel.HuozhuSQID = tempData.HuozhuSQID;
                quan_gspspxxviewmodel.Daima = tempData.Daima;
                quan_gspspxxviewmodel.Mingcheng = tempData.Mingcheng;
                quan_gspspxxviewmodel.ZhucezhengID = tempData.ZhucezhengID;
                quan_gspspxxviewmodel.ZhucezhengBH = tempData.ZhucezhengBH;
                quan_gspspxxviewmodel.Guige = tempData.Guige;
                quan_gspspxxviewmodel.Xinghao = tempData.Xinghao;
                quan_gspspxxviewmodel.Danwei = tempData.Danwei;
                quan_gspspxxviewmodel.Huansuanlv = tempData.Huansuanlv;
                quan_gspspxxviewmodel.Volchang = tempData.Volchang;
                quan_gspspxxviewmodel.Volkuan = tempData.Volkuan;
                quan_gspspxxviewmodel.Volgao = tempData.Volgao;
                quan_gspspxxviewmodel.Chanpinxian = tempData.Chanpinxian;
                quan_gspspxxviewmodel.Muluxuhao = tempData.Muluxuhao;
                quan_gspspxxviewmodel.Guanlifenlei = tempData.Guanlifenlei;
                quan_gspspxxviewmodel.Baozhuangyaoqiu = tempData.Baozhuangyaoqiu;
                quan_gspspxxviewmodel.Cunchutiaojian = tempData.Cunchutiaojian;
                quan_gspspxxviewmodel.QiyeID = tempData.QiyeID;
                quan_gspspxxviewmodel.Qiyemingcheng = tempData.Qiyemingcheng;
                quan_gspspxxviewmodel.GongyingID = tempData.GongyingID;
                quan_gspspxxviewmodel.GongyingSQID = tempData.GongyingSQID;
                quan_gspspxxviewmodel.GongyingXSID = tempData.GongyingXSID;
                quan_gspspxxviewmodel.Shouying = tempData.Shouying;
                quan_gspspxxviewmodel.Col1 = tempData.Col1;
                quan_gspspxxviewmodel.Col2 = tempData.Col2;
                quan_gspspxxviewmodel.Col3 = tempData.Col3;
                quan_gspspxxviewmodel.Col4 = tempData.Col4;
                quan_gspspxxviewmodel.Col5 = tempData.Col5;
                quan_gspspxxviewmodel.Col6 = tempData.Col6;
                quan_gspspxxviewmodel.MakeDate = tempData.MakeDate;
                quan_gspspxxviewmodel.MakeMan = tempData.MakeMan;
                quan_gspspxxviewmodel.ShenchaSF = tempData.ShenchaSF;
                quan_gspspxxviewmodel.JingyinSF = tempData.JingyinSF;
                quan_gspspxxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                quan_gspspxxviewmodel.ShangpinTM = tempData.ShangpinTM;
                quan_gspspxxviewmodel.Chandi = tempData.Chandi;
                quan_gspspxxviewmodel.ShangpinMS = tempData.ShangpinMS;
                quan_gspspxxviewmodel.SPID = tempData.SPID;
                return View(quan_gspspxxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhusqid = Request["huozhusqid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucezhengid = Request["zhucezhengid"] ?? "";
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string guige = Request["guige"] ?? "";
            string xinghao = Request["xinghao"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string volchang = Request["volchang"] ?? "";
            string volkuan = Request["volkuan"] ?? "";
            string volgao = Request["volgao"] ?? "";
            string chanpinxian = Request["chanpinxian"] ?? "";
            string muluxuhao = Request["muluxuhao"] ?? "";
            string guanlifenlei = Request["guanlifenlei"] ?? "";
            string baozhuangyaoqiu = Request["baozhuangyaoqiu"] ?? "";
            string cunchutiaojian = Request["cunchutiaojian"] ?? "";
            string qiyeid = Request["qiyeid"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingsqid = Request["gongyingsqid"] ?? "";
            string gongyingxsid = Request["gongyingxsid"] ?? "";
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
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shangpinms = Request["shangpinms"] ?? "";
            string spid = Request["spid"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_gspspxx p = ob_quan_gspspxxservice.GetEntityById(quan_gspspxx => quan_gspspxx.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.HuozhuSQID = huozhusqid == "" ? 0 : int.Parse(huozhusqid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.ZhucezhengID = zhucezhengid == "" ? 0 : int.Parse(zhucezhengid);
                p.ZhucezhengBH = zhucezhengbh.Trim();
                p.Guige = guige.Trim();
                p.Xinghao = xinghao.Trim();
                p.Danwei = danwei.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Volchang = volchang == "" ? 0 : float.Parse(volchang);
                p.Volkuan = volkuan == "" ? 0 : float.Parse(volkuan);
                p.Volgao = volgao == "" ? 0 : float.Parse(volgao);
                p.Chanpinxian = chanpinxian == "" ? 0 : int.Parse(chanpinxian);
                p.Muluxuhao = muluxuhao == "" ? 0 : int.Parse(muluxuhao);
                p.Guanlifenlei = guanlifenlei == "" ? 0 : int.Parse(guanlifenlei);
                p.Baozhuangyaoqiu = baozhuangyaoqiu.Trim();
                p.Cunchutiaojian = cunchutiaojian.Trim();
                p.QiyeID = qiyeid == "" ? 0 : int.Parse(qiyeid);
                p.Qiyemingcheng = qiyemingcheng.Trim();
                p.GongyingID = gongyingid == "" ? 0 : int.Parse(gongyingid);
                p.GongyingSQID = gongyingsqid == "" ? 0 : int.Parse(gongyingsqid);
                p.GongyingXSID = gongyingxsid == "" ? 0 : int.Parse(gongyingxsid);
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
                p.JingyinSF = jingyinsf == "" ? false : Boolean.Parse(jingyinsf);
                p.BaozhuangDW = baozhuangdw.Trim();
                p.ShangpinTM = shangpintm.Trim();
                p.Chandi = chandi.Trim();
                p.ShangpinMS = shangpinms.Trim();
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_quan_gspspxxservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            quan_gspspxx ob_quan_gspspxx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_gspspxx = ob_quan_gspspxxservice.GetEntityById(quan_gspspxx => quan_gspspxx.ID == id && quan_gspspxx.IsDelete == false);
                    ob_quan_gspspxx.IsDelete = true;
                    ob_quan_gspspxxservice.UpdateEntity(ob_quan_gspspxx);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddGSP(int id)
        {
            int _userid = (int)Session["user_id"];
            var _ed = Request["ed"] ?? "0";
            base_shangpinxx _spxx=null;
            quan_gspspxx _gspsp=null;

            if (_ed == "1")
                _gspsp = ob_quan_gspspxxservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
            else
            {
                _spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
                if (_spxx == null)
                    return View();
                _gspsp = ob_quan_gspspxxservice.GetEntityById(p => p.SPID == _spxx.ID && p.Shouying < 5 && p.IsDelete == false);
            }
            if (_gspsp != null)
            {
                base_shangpinxxViewModel quan_gspspxxviewmodel = new base_shangpinxxViewModel();
                quan_gspspxxviewmodel.ID = _gspsp.SPID;// _gspsp.ID;
                quan_gspspxxviewmodel.HuozhuID = _gspsp.HuozhuID;
                quan_gspspxxviewmodel.HuozhuSQID = _gspsp.HuozhuSQID;
                quan_gspspxxviewmodel.Daima = _gspsp.Daima;
                quan_gspspxxviewmodel.Mingcheng = _gspsp.Mingcheng;
                quan_gspspxxviewmodel.ZhucezhengID = _gspsp.ZhucezhengID;
                quan_gspspxxviewmodel.ZhucezhengBH = _gspsp.ZhucezhengBH;
                quan_gspspxxviewmodel.Guige = _gspsp.Guige;
                quan_gspspxxviewmodel.Xinghao = _gspsp.Xinghao;
                quan_gspspxxviewmodel.Danwei = _gspsp.Danwei;
                quan_gspspxxviewmodel.Huansuanlv = _gspsp.Huansuanlv;
                quan_gspspxxviewmodel.Volchang = _gspsp.Volchang;
                quan_gspspxxviewmodel.Volkuan = _gspsp.Volkuan;
                quan_gspspxxviewmodel.Volgao = _gspsp.Volgao;
                quan_gspspxxviewmodel.Chanpinxian = _gspsp.Chanpinxian;
                quan_gspspxxviewmodel.Muluxuhao = _gspsp.Muluxuhao;
                quan_gspspxxviewmodel.Guanlifenlei = _gspsp.Guanlifenlei;
                quan_gspspxxviewmodel.Baozhuangyaoqiu = _gspsp.Baozhuangyaoqiu;
                quan_gspspxxviewmodel.Cunchutiaojian = _gspsp.Cunchutiaojian;
                quan_gspspxxviewmodel.QiyeID = _gspsp.QiyeID;
                quan_gspspxxviewmodel.Qiyemingcheng = _gspsp.Qiyemingcheng;
                quan_gspspxxviewmodel.GongyingID = _gspsp.GongyingID;
                quan_gspspxxviewmodel.GongyingSQID = _gspsp.GongyingSQID;
                quan_gspspxxviewmodel.GongyingXSID = _gspsp.GongyingXSID;
                quan_gspspxxviewmodel.Shouying = _gspsp.Shouying;
                quan_gspspxxviewmodel.Col1 = _gspsp.Col1;
                quan_gspspxxviewmodel.Col2 = _gspsp.Col2;
                quan_gspspxxviewmodel.Col3 = _gspsp.Col3;
                quan_gspspxxviewmodel.Col4 = _gspsp.Col4;
                quan_gspspxxviewmodel.Col5 = _gspsp.Col5;
                quan_gspspxxviewmodel.Col6 = _gspsp.Col6;
                quan_gspspxxviewmodel.MakeDate = _gspsp.MakeDate;
                quan_gspspxxviewmodel.MakeMan = _gspsp.MakeMan;
                quan_gspspxxviewmodel.ShenchaSF = _gspsp.ShenchaSF;
                quan_gspspxxviewmodel.JingyinSF = _gspsp.JingyinSF;
                quan_gspspxxviewmodel.BaozhuangDW = _gspsp.BaozhuangDW;
                quan_gspspxxviewmodel.ShangpinTM = _gspsp.ShangpinTM;
                quan_gspspxxviewmodel.Chandi = _gspsp.Chandi;
                quan_gspspxxviewmodel.ShangpinMS = _gspsp.ShangpinMS;
                //quan_gspspxxviewmodel.SPID = _gspsp.SPID;
                ViewBag.gspspid = _gspsp.ID;
                return View(quan_gspspxxviewmodel);
            }

            _gspsp = new quan_gspspxx();
            _gspsp.HuozhuID = (int)_spxx.HuozhuID;
            _gspsp.HuozhuSQID = _spxx.HuozhuSQID;
            _gspsp.Daima = _spxx.Daima;
            _gspsp.Mingcheng = _spxx.Mingcheng;
            _gspsp.ZhucezhengID = _spxx.ZhucezhengID;
            _gspsp.ZhucezhengBH = _spxx.ZhucezhengBH;
            _gspsp.Guige = _spxx.Guige;
            _gspsp.Xinghao = _spxx.Xinghao;
            _gspsp.Danwei = _spxx.Danwei;
            _gspsp.Huansuanlv = _spxx.Huansuanlv;
            _gspsp.Volchang = _spxx.Volchang;
            _gspsp.Volkuan = _spxx.Volkuan;
            _gspsp.Volgao = _spxx.Volgao;
            _gspsp.Chanpinxian = _spxx.Chanpinxian;
            _gspsp.Muluxuhao = _spxx.Muluxuhao;
            _gspsp.Guanlifenlei = _spxx.Guanlifenlei;
            _gspsp.Baozhuangyaoqiu = _spxx.Baozhuangyaoqiu;
            _gspsp.Cunchutiaojian = _spxx.Cunchutiaojian;
            _gspsp.QiyeID = _spxx.QiyeID;
            _gspsp.Qiyemingcheng = _spxx.Qiyemingcheng;
            _gspsp.GongyingID = _spxx.GongyingID;
            _gspsp.GongyingSQID = _spxx.GongyingSQID;
            _gspsp.GongyingXSID = _spxx.GongyingXSID;
            _gspsp.Shouying = 1;
            _gspsp.Col1 = _spxx.Col1;
            _gspsp.Col2 = _spxx.Col2;
            _gspsp.Col3 = _spxx.Col3;
            _gspsp.Col4 = _spxx.Col4;
            _gspsp.Col5 = _spxx.Col5;
            _gspsp.Col6 = _spxx.Col6;
            _gspsp.MakeDate = DateTime.Now;
            _gspsp.MakeMan = _userid;
            _gspsp.ShenchaSF = _spxx.ShenchaSF;
            _gspsp.JingyinSF = _spxx.JingyinSF;
            _gspsp.BaozhuangDW = _spxx.BaozhuangDW;
            _gspsp.ShangpinTM = _spxx.ShangpinTM;
            _gspsp.Chandi = _spxx.Chandi;
            _gspsp.ShangpinTM = _spxx.ShangpinTM;
            _gspsp.SPID = _spxx.ID;
            _gspsp = ob_quan_gspspxxservice.AddEntity(_gspsp);
            if (_gspsp != null)
            {
                base_shangpinxxViewModel quan_gspspxxviewmodel = new base_shangpinxxViewModel();
                quan_gspspxxviewmodel.ID = _gspsp.SPID;// _gspsp.ID;
                quan_gspspxxviewmodel.HuozhuID = _gspsp.HuozhuID;
                quan_gspspxxviewmodel.HuozhuSQID = _gspsp.HuozhuSQID;
                quan_gspspxxviewmodel.Daima = _gspsp.Daima;
                quan_gspspxxviewmodel.Mingcheng = _gspsp.Mingcheng;
                quan_gspspxxviewmodel.ZhucezhengID = _gspsp.ZhucezhengID;
                quan_gspspxxviewmodel.ZhucezhengBH = _gspsp.ZhucezhengBH;
                quan_gspspxxviewmodel.Guige = _gspsp.Guige;
                quan_gspspxxviewmodel.Xinghao = _gspsp.Xinghao;
                quan_gspspxxviewmodel.Danwei = _gspsp.Danwei;
                quan_gspspxxviewmodel.Huansuanlv = _gspsp.Huansuanlv;
                quan_gspspxxviewmodel.Volchang = _gspsp.Volchang;
                quan_gspspxxviewmodel.Volkuan = _gspsp.Volkuan;
                quan_gspspxxviewmodel.Volgao = _gspsp.Volgao;
                quan_gspspxxviewmodel.Chanpinxian = _gspsp.Chanpinxian;
                quan_gspspxxviewmodel.Muluxuhao = _gspsp.Muluxuhao;
                quan_gspspxxviewmodel.Guanlifenlei = _gspsp.Guanlifenlei;
                quan_gspspxxviewmodel.Baozhuangyaoqiu = _gspsp.Baozhuangyaoqiu;
                quan_gspspxxviewmodel.Cunchutiaojian = _gspsp.Cunchutiaojian;
                quan_gspspxxviewmodel.QiyeID = _gspsp.QiyeID;
                quan_gspspxxviewmodel.Qiyemingcheng = _gspsp.Qiyemingcheng;
                quan_gspspxxviewmodel.GongyingID = _gspsp.GongyingID;
                quan_gspspxxviewmodel.GongyingSQID = _gspsp.GongyingSQID;
                quan_gspspxxviewmodel.GongyingXSID = _gspsp.GongyingXSID;
                quan_gspspxxviewmodel.Shouying = _gspsp.Shouying;
                quan_gspspxxviewmodel.Col1 = _gspsp.Col1;
                quan_gspspxxviewmodel.Col2 = _gspsp.Col2;
                quan_gspspxxviewmodel.Col3 = _gspsp.Col3;
                quan_gspspxxviewmodel.Col4 = _gspsp.Col4;
                quan_gspspxxviewmodel.Col5 = _gspsp.Col5;
                quan_gspspxxviewmodel.Col6 = _gspsp.Col6;
                quan_gspspxxviewmodel.MakeDate = _gspsp.MakeDate;
                quan_gspspxxviewmodel.MakeMan = _gspsp.MakeMan;
                quan_gspspxxviewmodel.ShenchaSF = _gspsp.ShenchaSF;
                quan_gspspxxviewmodel.JingyinSF = _gspsp.JingyinSF;
                quan_gspspxxviewmodel.BaozhuangDW = _gspsp.BaozhuangDW;
                quan_gspspxxviewmodel.ShangpinTM = _gspsp.ShangpinTM;
                quan_gspspxxviewmodel.Chandi = _gspsp.Chandi;
                quan_gspspxxviewmodel.ShangpinMS = _gspsp.ShangpinMS;
                //quan_gspspxxviewmodel.SPID = _gspsp.SPID;
                ViewBag.gspspid = _gspsp.ID;
                return View(quan_gspspxxviewmodel);
            }

            return View();
        }
    }
}

