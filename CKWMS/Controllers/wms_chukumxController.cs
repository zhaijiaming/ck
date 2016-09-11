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
    public class wms_chukumxController : Controller
    {
        private Iwms_chukumxService ob_wms_chukumxservice = ServiceFactory.wms_chukumxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukumx_index";
            Expression<Func<wms_chukumx, bool>> where = PredicateExtensionses.True<wms_chukumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukuid":
                            string chukuid = scld[1];
                            string chukuidequal = scld[2];
                            string chukuidand = scld[3];
                            if (!string.IsNullOrEmpty(chukuid))
                            {
                                if (chukuidequal.Equals("="))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals(">"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals("<"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukumx => wms_chukumx.IsDelete == false);

            var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID).ToPagedList<wms_chukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukumx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukumx_index";
            string page = "1";
            string chukuid = Request["chukuid"] ?? "";
            string chukuidequal = Request["chukuidequal"] ?? "";
            string chukuidand = Request["chukuidand"] ?? "";
            Expression<Func<wms_chukumx, bool>> where = PredicateExtensionses.True<wms_chukumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chukuid))
                {
                    if (chukuidequal.Equals("="))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals(">"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals("<"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                    }
                }
                if (!string.IsNullOrEmpty(chukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", chukuid, chukuidequal, chukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", "", chukuidequal, chukuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chukuid))
                {
                    if (chukuidequal.Equals("="))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID == int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals(">"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID > int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals("<"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ChukuID < int.Parse(chukuid));
                    }
                }
                if (!string.IsNullOrEmpty(chukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", chukuid, chukuidequal, chukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", "", chukuidequal, chukuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_chukumx => wms_chukumx.IsDelete == false);

            var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID).ToPagedList<wms_chukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukumx = tempData;
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
            string chukuid = Request["chukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string huopinzt = Request["huopinzt"] ?? "";
            string jianhuo = Request["jianhuo"] ?? "";
            try
            {
                wms_chukumx ob_wms_chukumx = new wms_chukumx();
                ob_wms_chukumx.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_wms_chukumx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_wms_chukumx.ShangpinMC = shangpinmc.Trim();
                ob_wms_chukumx.Zhucezheng = zhucezheng.Trim();
                ob_wms_chukumx.Guige = guige.Trim();
                ob_wms_chukumx.Pihao = pihao.Trim();
                ob_wms_chukumx.Pihao1 = pihao1.Trim();
                ob_wms_chukumx.Xuliema = xuliema.Trim();
                ob_wms_chukumx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_wms_chukumx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_wms_chukumx.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                ob_wms_chukumx.JibenDW = jibendw.Trim();
                ob_wms_chukumx.BaozhuangDW = baozhuangdw.Trim();
                ob_wms_chukumx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_wms_chukumx.Changjia = changjia.Trim();
                ob_wms_chukumx.Chandi = chandi.Trim();
                ob_wms_chukumx.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_chukumx.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_chukumx.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_chukumx.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_chukumx.Beizhu = beizhu.Trim();
                ob_wms_chukumx.Col1 = col1.Trim();
                ob_wms_chukumx.Col2 = col2.Trim();
                ob_wms_chukumx.Col3 = col3.Trim();
                ob_wms_chukumx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukumx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukumx.ShangpinDM = shangpindm.Trim();
                ob_wms_chukumx.HuopinZT = huopinzt == "" ? 0 : int.Parse(huopinzt);
                ob_wms_chukumx.Jianhuo = jianhuo == "" ? false : Boolean.Parse(jianhuo);
                ob_wms_chukumx = ob_wms_chukumxservice.AddEntity(ob_wms_chukumx);
                ViewBag.wms_chukumx = ob_wms_chukumx;
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
            wms_chukumx tempData = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == id && wms_chukumx.IsDelete == false);
            ViewBag.wms_chukumx = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukumxViewModel wms_chukumxviewmodel = new wms_chukumxViewModel();
                wms_chukumxviewmodel.ID = tempData.ID;
                wms_chukumxviewmodel.ChukuID = tempData.ChukuID;
                wms_chukumxviewmodel.ShangpinID = tempData.ShangpinID;
                wms_chukumxviewmodel.ShangpinMC = tempData.ShangpinMC;
                wms_chukumxviewmodel.Zhucezheng = tempData.Zhucezheng;
                wms_chukumxviewmodel.Guige = tempData.Guige;
                wms_chukumxviewmodel.Pihao = tempData.Pihao;
                wms_chukumxviewmodel.Pihao1 = tempData.Pihao1;
                wms_chukumxviewmodel.Xuliema = tempData.Xuliema;
                wms_chukumxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                wms_chukumxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                wms_chukumxviewmodel.ChukuSL = tempData.ChukuSL;
                wms_chukumxviewmodel.JibenDW = tempData.JibenDW;
                wms_chukumxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                wms_chukumxviewmodel.Huansuanlv = tempData.Huansuanlv;
                wms_chukumxviewmodel.Changjia = tempData.Changjia;
                wms_chukumxviewmodel.Chandi = tempData.Chandi;
                wms_chukumxviewmodel.Zhongliang = tempData.Zhongliang;
                wms_chukumxviewmodel.Jingzhong = tempData.Jingzhong;
                wms_chukumxviewmodel.Tiji = tempData.Tiji;
                wms_chukumxviewmodel.Jifeidun = tempData.Jifeidun;
                wms_chukumxviewmodel.Beizhu = tempData.Beizhu;
                wms_chukumxviewmodel.Col1 = tempData.Col1;
                wms_chukumxviewmodel.Col2 = tempData.Col2;
                wms_chukumxviewmodel.Col3 = tempData.Col3;
                wms_chukumxviewmodel.MakeDate = tempData.MakeDate;
                wms_chukumxviewmodel.MakeMan = tempData.MakeMan;
                wms_chukumxviewmodel.ShangpinDM = tempData.ShangpinDM;
                wms_chukumxviewmodel.HuopinZT = tempData.HuopinZT;
                wms_chukumxviewmodel.Jianhuo = tempData.Jianhuo;
                return View(wms_chukumxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string huopinzt = Request["huopinzt"] ?? "";
            string jianhuo = Request["jianhuo"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_chukumx p = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == uid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                p.JibenDW = jibendw.Trim();
                p.BaozhuangDW = baozhuangdw.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Changjia = changjia.Trim();
                p.Chandi = chandi.Trim();
                p.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                p.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShangpinDM = shangpindm.Trim();
                p.HuopinZT = huopinzt == "" ? 0 : int.Parse(huopinzt);
                p.Jianhuo = jianhuo == "" ? false : Boolean.Parse(jianhuo);
                ob_wms_chukumxservice.UpdateEntity(p);
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
            wms_chukumx ob_wms_chukumx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_chukumx = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == id && wms_chukumx.IsDelete == false);
                    ob_wms_chukumx.IsDelete = true;
                    ob_wms_chukumxservice.UpdateEntity(ob_wms_chukumx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

