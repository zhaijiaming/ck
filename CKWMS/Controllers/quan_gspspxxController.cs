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
                                        where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
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
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            Expression<Func<quan_gspspxx, bool>> where = PredicateExtensionses.True<quan_gspspxx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(quan_gspspxx => quan_gspspxx.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
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
            return RedirectToAction("Edit", new { id = uid });
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
    }
}

