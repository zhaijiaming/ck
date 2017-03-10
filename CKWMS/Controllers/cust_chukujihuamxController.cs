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
    public class cust_chukujihuamxController : Controller
    {
        private Icust_chukujihuamxService ob_cust_chukujihuamxservice = ServiceFactory.cust_chukujihuamxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cust_chukujihuamx_index";
            PageMenu.Set("Index", "cust_chukujihuamx", "客户服务");
            Expression<Func<cust_chukujihuamx, bool>> where = PredicateExtensionses.True<cust_chukujihuamx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "jihuaid":
                            string jihuaid = scld[1];
                            string jihuaidequal = scld[2];
                            string jihuaidand = scld[3];
                            if (!string.IsNullOrEmpty(jihuaid))
                            {
                                if (jihuaidequal.Equals("="))
                                {
                                    if (jihuaidand.Equals("and"))
                                        where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                                }
                                if (jihuaidequal.Equals(">"))
                                {
                                    if (jihuaidand.Equals("and"))
                                        where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                                }
                                if (jihuaidequal.Equals("<"))
                                {
                                    if (jihuaidand.Equals("and"))
                                        where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cust_chukujihuamx => cust_chukujihuamx.IsDelete == false);

            var tempData = ob_cust_chukujihuamxservice.LoadSortEntities(where.Compile(), false, cust_chukujihuamx => cust_chukujihuamx.ID).ToPagedList<cust_chukujihuamx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_chukujihuamx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cust_chukujihuamx_index";
            string page = "1";
            string jihuaid = Request["jihuaid"] ?? "";
            string jihuaidequal = Request["jihuaidequal"] ?? "";
            string jihuaidand = Request["jihuaidand"] ?? "";
            PageMenu.Set("Index", "cust_chukujihuamx", "客户服务");
            Expression<Func<cust_chukujihuamx, bool>> where = PredicateExtensionses.True<cust_chukujihuamx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(jihuaid))
                {
                    if (jihuaidequal.Equals("="))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals(">"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals("<"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                    }
                }
                if (!string.IsNullOrEmpty(jihuaid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", jihuaid, jihuaidequal, jihuaidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", "", jihuaidequal, jihuaidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(jihuaid))
                {
                    if (jihuaidequal.Equals("="))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID == int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals(">"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID > int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals("<"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                        else
                            where = where.Or(cust_chukujihuamx => cust_chukujihuamx.JihuaID < int.Parse(jihuaid));
                    }
                }
                if (!string.IsNullOrEmpty(jihuaid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", jihuaid, jihuaidequal, jihuaidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", "", jihuaidequal, jihuaidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cust_chukujihuamx => cust_chukujihuamx.IsDelete == false);

            var tempData = ob_cust_chukujihuamxservice.LoadSortEntities(where.Compile(), false, cust_chukujihuamx => cust_chukujihuamx.ID).ToPagedList<cust_chukujihuamx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_chukujihuamx = tempData;
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
            string jihuaid = Request["jihuaid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string jihuasl = Request["jihuasl"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            try
            {
                cust_chukujihuamx ob_cust_chukujihuamx = new cust_chukujihuamx();
                ob_cust_chukujihuamx.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_cust_chukujihuamx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_cust_chukujihuamx.ShangpinMC = shangpinmc.Trim();
                ob_cust_chukujihuamx.Zhucezheng = zhucezheng.Trim();
                ob_cust_chukujihuamx.Guige = guige.Trim();
                ob_cust_chukujihuamx.Pihao = pihao.Trim();
                ob_cust_chukujihuamx.Pihao1 = pihao1.Trim();
                ob_cust_chukujihuamx.Xuliema = xuliema.Trim();
                ob_cust_chukujihuamx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_cust_chukujihuamx.ShiXiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_cust_chukujihuamx.JihuaSL = jihuasl == "" ? 0 : float.Parse(jihuasl);
                ob_cust_chukujihuamx.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                ob_cust_chukujihuamx.JibenDW = jibendw.Trim();
                ob_cust_chukujihuamx.BaozhuangDW = baozhuangdw.Trim();
                ob_cust_chukujihuamx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_cust_chukujihuamx.Changjia = changjia.Trim();
                ob_cust_chukujihuamx.Chandi = chandi.Trim();
                ob_cust_chukujihuamx.Beizhu = beizhu.Trim();
                ob_cust_chukujihuamx.Col1 = col1.Trim();
                ob_cust_chukujihuamx.Col2 = col2.Trim();
                ob_cust_chukujihuamx.Col3 = col3.Trim();
                ob_cust_chukujihuamx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cust_chukujihuamx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_chukujihuamx.ShangpinDM = shangpindm.Trim();
                ob_cust_chukujihuamx = ob_cust_chukujihuamxservice.AddEntity(ob_cust_chukujihuamx);
                ViewBag.cust_chukujihuamx = ob_cust_chukujihuamx;
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
            cust_chukujihuamx tempData = ob_cust_chukujihuamxservice.GetEntityById(cust_chukujihuamx => cust_chukujihuamx.ID == id && cust_chukujihuamx.IsDelete == false);
            ViewBag.cust_chukujihuamx = tempData;
            if (tempData == null)
                return View();
            else
            {
                cust_chukujihuamxViewModel cust_chukujihuamxviewmodel = new cust_chukujihuamxViewModel();
                cust_chukujihuamxviewmodel.ID = tempData.ID;
                cust_chukujihuamxviewmodel.JihuaID = tempData.JihuaID;
                cust_chukujihuamxviewmodel.ShangpinID = tempData.ShangpinID;
                cust_chukujihuamxviewmodel.ShangpinMC = tempData.ShangpinMC;
                cust_chukujihuamxviewmodel.Zhucezheng = tempData.Zhucezheng;
                cust_chukujihuamxviewmodel.Guige = tempData.Guige;
                cust_chukujihuamxviewmodel.Pihao = tempData.Pihao;
                cust_chukujihuamxviewmodel.Pihao1 = tempData.Pihao1;
                cust_chukujihuamxviewmodel.Xuliema = tempData.Xuliema;
                cust_chukujihuamxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                cust_chukujihuamxviewmodel.ShiXiaoRQ = tempData.ShiXiaoRQ;
                cust_chukujihuamxviewmodel.JihuaSL = tempData.JihuaSL;
                cust_chukujihuamxviewmodel.ChukuSL = tempData.ChukuSL;
                cust_chukujihuamxviewmodel.JibenDW = tempData.JibenDW;
                cust_chukujihuamxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                cust_chukujihuamxviewmodel.Huansuanlv = tempData.Huansuanlv;
                cust_chukujihuamxviewmodel.Changjia = tempData.Changjia;
                cust_chukujihuamxviewmodel.Chandi = tempData.Chandi;
                cust_chukujihuamxviewmodel.Beizhu = tempData.Beizhu;
                cust_chukujihuamxviewmodel.Col1 = tempData.Col1;
                cust_chukujihuamxviewmodel.Col2 = tempData.Col2;
                cust_chukujihuamxviewmodel.Col3 = tempData.Col3;
                cust_chukujihuamxviewmodel.MakeDate = tempData.MakeDate;
                cust_chukujihuamxviewmodel.MakeMan = tempData.MakeMan;
                cust_chukujihuamxviewmodel.ShangpinDM = tempData.ShangpinDM;
                return View(cust_chukujihuamxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string jihuasl = Request["jihuasl"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            int uid = int.Parse(id);
            try
            {
                cust_chukujihuamx p = ob_cust_chukujihuamxservice.GetEntityById(cust_chukujihuamx => cust_chukujihuamx.ID == uid);
                p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShiXiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.JihuaSL = jihuasl == "" ? 0 : float.Parse(jihuasl);
                p.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                p.JibenDW = jibendw.Trim();
                p.BaozhuangDW = baozhuangdw.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Changjia = changjia.Trim();
                p.Chandi = chandi.Trim();
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShangpinDM = shangpindm.Trim();
                ob_cust_chukujihuamxservice.UpdateEntity(p);
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
            cust_chukujihuamx ob_cust_chukujihuamx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cust_chukujihuamx = ob_cust_chukujihuamxservice.GetEntityById(cust_chukujihuamx => cust_chukujihuamx.ID == id && cust_chukujihuamx.IsDelete == false);
                    ob_cust_chukujihuamx.IsDelete = true;
                    ob_cust_chukujihuamxservice.UpdateEntity(ob_cust_chukujihuamx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

