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
    public class gsp_hpyhmxController : Controller
    {
        private Igsp_hpyhmxService ob_gsp_hpyhmxservice = ServiceFactory.gsp_hpyhmxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "gsp_hpyhmx_index";
            Expression<Func<quan_maintenance_v, bool>> where = PredicateExtensionses.True<quan_maintenance_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "yhid":
                            string yhid = scld[1];
                            string yhidequal = scld[2];
                            string yhidand = scld[3];
                            if (!string.IsNullOrEmpty(yhid))
                            {
                                if (yhidequal.Equals("="))
                                {
                                    if (yhidand.Equals("and"))
                                        where = where.And(gsp_hpyhmx => gsp_hpyhmx.YHID == int.Parse(yhid));
                                    else
                                        where = where.Or(gsp_hpyhmx => gsp_hpyhmx.YHID == int.Parse(yhid));
                                }
                                if (yhidequal.Equals(">"))
                                {
                                    if (yhidand.Equals("and"))
                                        where = where.And(gsp_hpyhmx => gsp_hpyhmx.YHID > int.Parse(yhid));
                                    else
                                        where = where.Or(gsp_hpyhmx => gsp_hpyhmx.YHID > int.Parse(yhid));
                                }
                                if (yhidequal.Equals("<"))
                                {
                                    if (yhidand.Equals("and"))
                                        where = where.And(gsp_hpyhmx => gsp_hpyhmx.YHID < int.Parse(yhid));
                                    else
                                        where = where.Or(gsp_hpyhmx => gsp_hpyhmx.YHID < int.Parse(yhid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }            

            var tempData = ob_gsp_hpyhmxservice.LoadDetail(where.Compile()).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.gsp_hpyhmx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "gsp_hpyhmx_index";
            string page = "1";
            string yhid = Request["yhid"] ?? "";
            string yhidequal = Request["yhidequal"] ?? "";
            string yhidand = Request["yhidand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";

            Expression<Func<quan_maintenance_v, bool>> where = PredicateExtensionses.True<quan_maintenance_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_gsp_hpyhmxservice.LoadDetail(where.Compile()).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.gsp_hpyhmx = tempData;
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
            string yhid = Request["yhid"] ?? "";
            string chid = Request["chid"] ?? "";
            string jiancha = Request["jiancha"] ?? "";
            string cljg = Request["cljg"] ?? "";
            string yhr = Request["yhr"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                gsp_hpyhmx ob_gsp_hpyhmx = new gsp_hpyhmx();
                ob_gsp_hpyhmx.YHID = yhid == "" ? 0 : int.Parse(yhid);
                ob_gsp_hpyhmx.CHID = chid == "" ? 0 : int.Parse(chid);
                ob_gsp_hpyhmx.Jiancha = jiancha.Trim();
                ob_gsp_hpyhmx.CLJG = cljg.Trim();
                ob_gsp_hpyhmx.YHR = yhr.Trim();
                ob_gsp_hpyhmx.Beizhu = beizhu.Trim();
                ob_gsp_hpyhmx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_gsp_hpyhmx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_gsp_hpyhmx = ob_gsp_hpyhmxservice.AddEntity(ob_gsp_hpyhmx);
                ViewBag.gsp_hpyhmx = ob_gsp_hpyhmx;
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
            gsp_hpyhmx tempData = ob_gsp_hpyhmxservice.GetEntityById(gsp_hpyhmx => gsp_hpyhmx.ID == id && gsp_hpyhmx.IsDelete == false);
            ViewBag.gsp_hpyhmx = tempData;
            if (tempData == null)
                return View();
            else
            {
                gsp_hpyhmxViewModel gsp_hpyhmxviewmodel = new gsp_hpyhmxViewModel();
                gsp_hpyhmxviewmodel.ID = tempData.ID;
                gsp_hpyhmxviewmodel.YHID = tempData.YHID;
                gsp_hpyhmxviewmodel.CHID = tempData.CHID;
                gsp_hpyhmxviewmodel.Jiancha = tempData.Jiancha;
                gsp_hpyhmxviewmodel.CLJG = tempData.CLJG;
                gsp_hpyhmxviewmodel.YHR = tempData.YHR;
                gsp_hpyhmxviewmodel.Beizhu = tempData.Beizhu;
                gsp_hpyhmxviewmodel.MakeDate = tempData.MakeDate;
                gsp_hpyhmxviewmodel.MakeMan = tempData.MakeMan;
                return View(gsp_hpyhmxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string yhid = Request["yhid"] ?? "";
            string chid = Request["chid"] ?? "";
            string jiancha = Request["jiancha"] ?? "";
            string cljg = Request["cljg"] ?? "";
            string yhr = Request["yhr"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                gsp_hpyhmx p = ob_gsp_hpyhmxservice.GetEntityById(gsp_hpyhmx => gsp_hpyhmx.ID == uid);
                p.YHID = yhid == "" ? 0 : int.Parse(yhid);
                p.CHID = chid == "" ? 0 : int.Parse(chid);
                p.Jiancha = jiancha.Trim();
                p.CLJG = cljg.Trim();
                p.YHR = yhr.Trim();
                p.Beizhu = beizhu.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_gsp_hpyhmxservice.UpdateEntity(p);
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
            gsp_hpyhmx ob_gsp_hpyhmx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_gsp_hpyhmx = ob_gsp_hpyhmxservice.GetEntityById(gsp_hpyhmx => gsp_hpyhmx.ID == id && gsp_hpyhmx.IsDelete == false);
                    ob_gsp_hpyhmx.IsDelete = true;
                    ob_gsp_hpyhmxservice.UpdateEntity(ob_gsp_hpyhmx);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult AddMaintenceDetail()
        {
            int userid = (int)Session["user_id"];
            var _mid = Request["mid"] ?? "";
            var _hzid = Request["hzid"] ?? "";
            if(string.IsNullOrEmpty(_mid) || string.IsNullOrEmpty(_hzid))
            {
                _mid = "0";
                _hzid = "0";
            }

            string pagetag = "gsp_hpyhmx_addmaintencedetail";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //rukudanbh
            string rukudanbh = Request["rukudanbh"] ?? "";
            string rukudanbhequal = Request["rukudanbhequal"] ?? "";
            string rukudanbhand = Request["rukudanbhand"] ?? "";
            //kuwei
            string kuwei = Request["kuwei"] ?? "";
            string kuweiequal = Request["kuweiequal"] ?? "";
            string kuweiand = Request["kuweiand"] ?? "";
            //shangpindm
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpindmequal = Request["shangpindmequal"] ?? "";
            string shangpindmand = Request["shangpindmand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            //shixiaorq
            string shixiaorq = Request["shixiaorq"] ?? "";
            string shixiaorqequal = Request["shixiaorqequal"] ?? "";
            string shixiaorqand = Request["shixiaorqand"] ?? "";
            //入库日期
            string rukurq = Request["rukurq"] ?? "";
            string rukurqequal = Request["rukurqequal"] ?? "";
            string rukurqand = Request["rukurqand"] ?? "";
            //chanpinxian
            string chanpinxian = Request["chanpinxian"] ?? "";
            string chanpinxianequal = Request["chanpinxianequal"] ?? "";
            string chanpinxianand = Request["chanpinxianand"] ?? "";

            PageMenu.Set("CurrentStorage", "wms_cunhuo", "仓库操作");

            Expression<Func<wms_inventory_v, bool>> where = PredicateExtensionses.True<wms_inventory_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
               //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //shixiaorq
                if (!string.IsNullOrEmpty(shixiaorq))
                {
                    if (shixiaorqequal.Equals("="))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                        else
                            where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals(">"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals("<"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                }
                if (!string.IsNullOrEmpty(shixiaorq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", shixiaorq, shixiaorqequal, shixiaorqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", "", shixiaorqequal, shixiaorqand);
                //rukurq
                if (!string.IsNullOrEmpty(rukurq))
                {
                    if (rukurqequal.Equals("="))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals(">"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals("<"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                    }
                }
                if (!string.IsNullOrEmpty(rukurq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", rukurq, rukurqequal, rukurqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", "", rukurqequal, rukurqand);
                //chanpinxian
                if (!string.IsNullOrEmpty(chanpinxian))
                {
                    if (chanpinxianequal.Equals("="))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng == chanpinxian);
                        else
                            where = where.Or(p => p.cpxmingcheng == chanpinxian);
                    }
                    if (chanpinxianequal.Equals("like"))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                        else
                            where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                    }
                }
                if (!string.IsNullOrEmpty(chanpinxian))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", chanpinxian, chanpinxianequal, chanpinxianand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", "", chanpinxianequal, chanpinxianand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //shixiaorq
                if (!string.IsNullOrEmpty(shixiaorq))
                {
                    if (shixiaorqequal.Equals("="))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                        else
                            where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals(">"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                    if (shixiaorqequal.Equals("<"))
                    {
                        if (shixiaorqand.Equals("and"))
                            where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                        else
                            where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                    }
                }
                if (!string.IsNullOrEmpty(shixiaorq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", shixiaorq, shixiaorqequal, shixiaorqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shixiaorq", "", shixiaorqequal, shixiaorqand);
                //rukurq
                if (!string.IsNullOrEmpty(rukurq))
                {
                    if (rukurqequal.Equals("="))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals(">"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                    }
                    if (rukurqequal.Equals("<"))
                    {
                        if (rukurqand.Equals("and"))
                            where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                        else
                            where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                    }
                }
                if (!string.IsNullOrEmpty(rukurq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", rukurq, rukurqequal, rukurqand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukurq", "", rukurqequal, rukurqand);
                //chanpinxian
                if (!string.IsNullOrEmpty(chanpinxian))
                {
                    if (chanpinxianequal.Equals("="))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng == chanpinxian);
                        else
                            where = where.Or(p => p.cpxmingcheng == chanpinxian);
                    }
                    if (chanpinxianequal.Equals("like"))
                    {
                        if (chanpinxianand.Equals("and"))
                            where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                        else
                            where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                    }
                }
                if (!string.IsNullOrEmpty(chanpinxian))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", chanpinxian, chanpinxianequal, chanpinxianand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chanpinxian", "", chanpinxianequal, chanpinxianand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            where = where.And(p => p.HuozhuID ==int.Parse(_hzid));
            var tempdt = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile());
            ViewBag.totalproduct = tempdt.Sum(p => p.sshuliang);
            ViewBag.totalbox = tempdt.Sum(p => p.sshuliang / (p.Huansuanlv != 0 ? p.Huansuanlv : 1));
            var tempData = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_inventory_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            ViewBag.mdid = _mid;
            ViewBag.hzid = _hzid;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult AddMaintenceDetail(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "gsp_hpyhmx_addmaintencedetail";
            var _mid = Request["mid"] ?? "";
            var _hzid = Request["hzid"] ?? "";
            if (string.IsNullOrEmpty(_mid) || string.IsNullOrEmpty(_hzid))
            {
                _mid = "0";
                _hzid = "0";
            }

            Expression<Func<wms_inventory_v, bool>> where = PredicateExtensionses.True<wms_inventory_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {                        
                        case "shangpindm":
                            string shangpindm = scld[1];
                            string shangpindmequal = scld[2];
                            string shangpindmand = scld[3];
                            if (!string.IsNullOrEmpty(shangpindm))
                            {
                                if (shangpindmequal.Equals("="))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinDM.Contains(shangpindm));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        case "shixiaorq":
                            string shixiaorq = scld[1];
                            string shixiaorqequal = scld[2];
                            string shixiaorqand = scld[3];
                            if (!string.IsNullOrEmpty(shixiaorq))
                            {
                                if (shixiaorqequal.Equals("="))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));// DateTime.Parse());
                                    else
                                        where = where.Or(p => p.ShixiaoRQ == DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals(">"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ > DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                                if (shixiaorqequal.Equals("<"))
                                {
                                    if (shixiaorqand.Equals("and"))
                                        where = where.And(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                    else
                                        where = where.Or(p => p.ShixiaoRQ < DateTime.Now.AddDays(int.Parse(shixiaorq)));
                                }
                            }
                            break;
                        case "rukurq":
                            string rukurq = scld[1];
                            string rukurqequal = scld[2];
                            string rukurqand = scld[3];
                            if (!string.IsNullOrEmpty(rukurq))
                            {
                                if (rukurqequal.Equals("="))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ == DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals(">"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ > DateTime.Parse(rukurq));
                                }
                                if (rukurqequal.Equals("<"))
                                {
                                    if (rukurqand.Equals("and"))
                                        where = where.And(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                    else
                                        where = where.Or(wms_inventory_v => wms_inventory_v.RukuRQ < DateTime.Parse(rukurq));
                                }
                            }
                            break;
                        case "chanpinxian":
                            string chanpinxian = scld[1];
                            string chanpinxianequal = scld[2];
                            string chanpinxianand = scld[3];
                            if (!string.IsNullOrEmpty(chanpinxian))
                            {
                                if (chanpinxianequal.Equals("="))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng == chanpinxian);
                                    else
                                        where = where.Or(p => p.cpxmingcheng == chanpinxian);
                                }
                                if (chanpinxianequal.Equals("like"))
                                {
                                    if (chanpinxianand.Equals("and"))
                                        where = where.And(p => p.cpxmingcheng.Contains(chanpinxian));
                                    else
                                        where = where.Or(p => p.cpxmingcheng.Contains(chanpinxian));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            where = where.And(p => p.HuozhuID == int.Parse(_hzid));
            var tempdt = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile());
            ViewBag.totalproduct = tempdt.Sum(p => p.sshuliang);
            ViewBag.totalbox = tempdt.Sum(p => p.sshuliang / (p.Huansuanlv != 0 ? p.Huansuanlv : 1));
            ViewBag.mdid = _mid;
            ViewBag.hzid = _hzid;
            var tempData = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            return View(tempData);
        }
        public JsonResult AddCargos()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            var _chs = Request["hps"] ?? "";
            var _mid = Request["mid"] ?? "";

            if (string.IsNullOrEmpty(_chs) || string.IsNullOrEmpty(_mid))
                return Json(-1);

            var _cargos = _chs.Split(',');
            foreach(var cargo in _cargos)
            {
                if (cargo.Length < 1)
                    continue;
                gsp_hpyhmx _mx = new gsp_hpyhmx();
                _mx.CHID = int.Parse(cargo);
                _mx.CLJG = "";
                _mx.YHID = int.Parse(_mid);
                _mx.Jiancha = "";
                _mx.YHR = "";
                _mx.MakeDate = DateTime.Now;
                _mx.MakeMan = _userid;
                _mx=ob_gsp_hpyhmxservice.AddEntity(_mx);
            }
            return Json(1);
        }
        public ActionResult GetBill(string page)
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            var _mid = Request["mid"] ?? "";
            if (string.IsNullOrEmpty(_mid))
                _mid = "0";
            if (string.IsNullOrEmpty(page))
                page = "1";
            //var _yh = ServiceFactory.gsp_hpyhservice.GetEntityById(p => p.ID == int.Parse(_mid) && p.IsDelete == false);
            //if (_yh == null)
            //    return View();
            var _bill = ob_gsp_hpyhmxservice.GetBill(int.Parse(_mid)).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"])); 
            ViewBag.mbill = _bill;
            ViewBag.username = _username;
            return View(_bill);
        }
        public JsonResult AddCheck()
        {
            int _userid = (int)Session["user_id"];
            var _yhr = Request["yhr"] ?? "";
            var _yhjc = Request["yhjc"] ?? "";
            var _yhjg = Request["yhjg"] ?? "";
            var _yhbz = Request["yhbz"] ?? "";
            var _yhid = Request["yhid"] ?? "";
            if (string.IsNullOrEmpty(_yhid) || string.IsNullOrEmpty(_yhjg))
                return Json(-1);
            var _yhmx = ob_gsp_hpyhmxservice.GetEntityById(p => p.ID == int.Parse(_yhid) && p.IsDelete == false);
            if (_yhmx == null)
                return Json(-1);
            _yhmx.Jiancha = _yhjc;
            _yhmx.Beizhu = _yhbz;
            _yhmx.CLJG = _yhjg;
            _yhmx.YHR = _yhr;
            ob_gsp_hpyhmxservice.UpdateEntity(_yhmx);
            return Json(1);
        }
        public JsonResult DeleteRecord()
        {
            var yhid = Request["yhid"] ?? "";
            if (string.IsNullOrEmpty(yhid))
                return Json(-1);
            var _yhmx = ob_gsp_hpyhmxservice.GetEntityById(p => p.ID == int.Parse(yhid) && p.IsDelete == false);
            if (_yhmx == null)
                return Json(-1);
            _yhmx.IsDelete = true;
            ob_gsp_hpyhmxservice.UpdateEntity(_yhmx);
            return Json(1);
        }
    }
}

