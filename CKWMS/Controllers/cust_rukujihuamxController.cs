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
    public class cust_rukujihuamxController : Controller
    {
        private Icust_rukujihuamxService ob_cust_rukujihuamxservice = ServiceFactory.cust_rukujihuamxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cust_rukujihuamx_index";
            Expression<Func<cust_rukujihuamx, bool>> where = PredicateExtensionses.True<cust_rukujihuamx>();
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
                                        where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                                }
                                if (jihuaidequal.Equals(">"))
                                {
                                    if (jihuaidand.Equals("and"))
                                        where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                                }
                                if (jihuaidequal.Equals("<"))
                                {
                                    if (jihuaidand.Equals("and"))
                                        where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
                                    else
                                        where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cust_rukujihuamx => cust_rukujihuamx.IsDelete == false);

            var tempData = ob_cust_rukujihuamxservice.LoadSortEntities(where.Compile(), false, cust_rukujihuamx => cust_rukujihuamx.ID).ToPagedList<cust_rukujihuamx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_rukujihuamx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cust_rukujihuamx_index";
            string page = "1";
            string jihuaid = Request["jihuaid"] ?? "";
            string jihuaidequal = Request["jihuaidequal"] ?? "";
            string jihuaidand = Request["jihuaidand"] ?? "";
            Expression<Func<cust_rukujihuamx, bool>> where = PredicateExtensionses.True<cust_rukujihuamx>();
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
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals(">"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals("<"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
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
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID == int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals(">"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID > int.Parse(jihuaid));
                    }
                    if (jihuaidequal.Equals("<"))
                    {
                        if (jihuaidand.Equals("and"))
                            where = where.And(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
                        else
                            where = where.Or(cust_rukujihuamx => cust_rukujihuamx.JihuaID < int.Parse(jihuaid));
                    }
                }
                if (!string.IsNullOrEmpty(jihuaid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", jihuaid, jihuaidequal, jihuaidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jihuaid", "", jihuaidequal, jihuaidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cust_rukujihuamx => cust_rukujihuamx.IsDelete == false);

            var tempData = ob_cust_rukujihuamxservice.LoadSortEntities(where.Compile(), false, cust_rukujihuamx => cust_rukujihuamx.ID).ToPagedList<cust_rukujihuamx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_rukujihuamx = tempData;
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
            string daohuosl = Request["daohuosl"] ?? "";
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
                cust_rukujihuamx ob_cust_rukujihuamx = new cust_rukujihuamx();
                ob_cust_rukujihuamx.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_cust_rukujihuamx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_cust_rukujihuamx.ShangpinMC = shangpinmc.Trim();
                ob_cust_rukujihuamx.Zhucezheng = zhucezheng.Trim();
                ob_cust_rukujihuamx.Guige = guige.Trim();
                ob_cust_rukujihuamx.Pihao = pihao.Trim();
                ob_cust_rukujihuamx.Pihao1 = pihao1.Trim();
                ob_cust_rukujihuamx.Xuliema = xuliema.Trim();
                ob_cust_rukujihuamx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_cust_rukujihuamx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_cust_rukujihuamx.JihuaSL = jihuasl == "" ? 0 : float.Parse(jihuasl);
                ob_cust_rukujihuamx.DaohuoSL = daohuosl == "" ? 0 : float.Parse(daohuosl);
                ob_cust_rukujihuamx.JibenDW = jibendw.Trim();
                ob_cust_rukujihuamx.BaozhuangDW = baozhuangdw.Trim();
                ob_cust_rukujihuamx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_cust_rukujihuamx.Changjia = changjia.Trim();
                ob_cust_rukujihuamx.Chandi = chandi.Trim();
                ob_cust_rukujihuamx.Beizhu = beizhu.Trim();
                ob_cust_rukujihuamx.Col1 = col1.Trim();
                ob_cust_rukujihuamx.Col2 = col2.Trim();
                ob_cust_rukujihuamx.Col3 = col3.Trim();
                ob_cust_rukujihuamx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cust_rukujihuamx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_rukujihuamx.ShangpinDM = shangpindm.Trim();
                ob_cust_rukujihuamx = ob_cust_rukujihuamxservice.AddEntity(ob_cust_rukujihuamx);
                ViewBag.cust_rukujihuamx = ob_cust_rukujihuamx;
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
            cust_rukujihuamx tempData = ob_cust_rukujihuamxservice.GetEntityById(cust_rukujihuamx => cust_rukujihuamx.ID == id && cust_rukujihuamx.IsDelete == false);
            ViewBag.cust_rukujihuamx = tempData;
            if (tempData == null)
                return View();
            else
            {
                cust_rukujihuamxViewModel cust_rukujihuamxviewmodel = new cust_rukujihuamxViewModel();
                cust_rukujihuamxviewmodel.ID = tempData.ID;
                cust_rukujihuamxviewmodel.JihuaID = tempData.JihuaID;
                cust_rukujihuamxviewmodel.ShangpinID = tempData.ShangpinID;
                cust_rukujihuamxviewmodel.ShangpinMC = tempData.ShangpinMC;
                cust_rukujihuamxviewmodel.Zhucezheng = tempData.Zhucezheng;
                cust_rukujihuamxviewmodel.Guige = tempData.Guige;
                cust_rukujihuamxviewmodel.Pihao = tempData.Pihao;
                cust_rukujihuamxviewmodel.Pihao1 = tempData.Pihao1;
                cust_rukujihuamxviewmodel.Xuliema = tempData.Xuliema;
                cust_rukujihuamxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                cust_rukujihuamxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                cust_rukujihuamxviewmodel.JihuaSL = tempData.JihuaSL;
                cust_rukujihuamxviewmodel.DaohuoSL = tempData.DaohuoSL;
                cust_rukujihuamxviewmodel.JibenDW = tempData.JibenDW;
                cust_rukujihuamxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                cust_rukujihuamxviewmodel.Huansuanlv = tempData.Huansuanlv;
                cust_rukujihuamxviewmodel.Changjia = tempData.Changjia;
                cust_rukujihuamxviewmodel.Chandi = tempData.Chandi;
                cust_rukujihuamxviewmodel.Beizhu = tempData.Beizhu;
                cust_rukujihuamxviewmodel.Col1 = tempData.Col1;
                cust_rukujihuamxviewmodel.Col2 = tempData.Col2;
                cust_rukujihuamxviewmodel.Col3 = tempData.Col3;
                cust_rukujihuamxviewmodel.MakeDate = tempData.MakeDate;
                cust_rukujihuamxviewmodel.MakeMan = tempData.MakeMan;
                cust_rukujihuamxviewmodel.ShangpinDM = tempData.ShangpinDM;
                return View(cust_rukujihuamxviewmodel);
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
            string daohuosl = Request["daohuosl"] ?? "";
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
                cust_rukujihuamx p = ob_cust_rukujihuamxservice.GetEntityById(cust_rukujihuamx => cust_rukujihuamx.ID == uid);
                p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.JihuaSL = jihuasl == "" ? 0 : float.Parse(jihuasl);
                p.DaohuoSL = daohuosl == "" ? 0 : float.Parse(daohuosl);
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
                ob_cust_rukujihuamxservice.UpdateEntity(p);
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
            cust_rukujihuamx ob_cust_rukujihuamx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cust_rukujihuamx = ob_cust_rukujihuamxservice.GetEntityById(cust_rukujihuamx => cust_rukujihuamx.ID == id && cust_rukujihuamx.IsDelete == false);
                    ob_cust_rukujihuamx.IsDelete = true;
                    ob_cust_rukujihuamxservice.UpdateEntity(ob_cust_rukujihuamx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

