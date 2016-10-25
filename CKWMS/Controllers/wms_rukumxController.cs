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
    public class wms_rukumxController : Controller
    {
        private Iwms_rukumxService ob_wms_rukumxservice = ServiceFactory.wms_rukumxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukumx_index";
            Expression<Func<wms_rukumx, bool>> where = PredicateExtensionses.True<wms_rukumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rukuid":
                            string rukuid = scld[1];
                            string rukuidequal = scld[2];
                            string rukuidand = scld[3];
                            if (!string.IsNullOrEmpty(rukuid))
                            {
                                if (rukuidequal.Equals("="))
                                {
                                    if (rukuidand.Equals("and"))
                                        where = where.And(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                                    else
                                        where = where.Or(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                                }
                                if (rukuidequal.Equals(">"))
                                {
                                    if (rukuidand.Equals("and"))
                                        where = where.And(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                                    else
                                        where = where.Or(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                                }
                                if (rukuidequal.Equals("<"))
                                {
                                    if (rukuidand.Equals("and"))
                                        where = where.And(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                                    else
                                        where = where.Or(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_rukumx => wms_rukumx.IsDelete == false);

            var tempData = ob_wms_rukumxservice.LoadSortEntities(where.Compile(), false, wms_rukumx => wms_rukumx.ID).ToPagedList<wms_rukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukumx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_rukumx_index";
            string page = "1";
            string rukuid = Request["rukuid"] ?? "";
            string rukuidequal = Request["rukuidequal"] ?? "";
            string rukuidand = Request["rukuidand"] ?? "";
            Expression<Func<wms_rukumx, bool>> where = PredicateExtensionses.True<wms_rukumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rukuid))
                {
                    if (rukuidequal.Equals("="))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                    }
                    if (rukuidequal.Equals(">"))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                    }
                    if (rukuidequal.Equals("<"))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                    }
                }
                if (!string.IsNullOrEmpty(rukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukuid", rukuid, rukuidequal, rukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukuid", "", rukuidequal, rukuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rukuid))
                {
                    if (rukuidequal.Equals("="))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID == int.Parse(rukuid));
                    }
                    if (rukuidequal.Equals(">"))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID > int.Parse(rukuid));
                    }
                    if (rukuidequal.Equals("<"))
                    {
                        if (rukuidand.Equals("and"))
                            where = where.And(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                        else
                            where = where.Or(wms_rukumx => wms_rukumx.RukuID < int.Parse(rukuid));
                    }
                }
                if (!string.IsNullOrEmpty(rukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukuid", rukuid, rukuidequal, rukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukuid", "", rukuidequal, rukuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_rukumx => wms_rukumx.IsDelete == false);

            var tempData = ob_wms_rukumxservice.LoadSortEntities(where.Compile(), false, wms_rukumx => wms_rukumx.ID).ToPagedList<wms_rukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_rukumx = tempData;
            return View(tempData);
        }
        public JsonResult GetCargoDetail()
        {
            string _rkuid = Request["rkd"] ?? "";
            if (_rkuid.Length == 0)
                return Json("");
            else
            {
                var _mxtmp = ob_wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(_rkuid) && p.IsDelete == false, true, s => s.ShangpinMC).ToList<wms_rukumx>();
                return Json(_mxtmp);
            }
        }
        public JsonResult DelCargo()
        {
            string _mxid = Request["rkmx"] ?? "";
            if (_mxid.Length < 1)
                return Json(0);
            int id;
            foreach (string sD in _mxid.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    var ob_wms_rukumx = ob_wms_rukumxservice.GetEntityById(wms_rukumx => wms_rukumx.ID == id && wms_rukumx.IsDelete == false);
                    ob_wms_rukumx.IsDelete = true;
                    ob_wms_rukumxservice.UpdateEntity(ob_wms_rukumx);
                }
            }
            return Json(1);
        }
        public ActionResult GetCargos(int id)
        {
            var _mxtmp = ob_wms_rukumxservice.LoadSortEntities(p => p.RukuID == id && p.IsDelete == false, true, s => s.ShangpinMC);
            ViewBag.wms_rukumx = _mxtmp;
            ViewBag.rkid = id;
            return View("CargoIndex", _mxtmp);
        }
        //public ActionResult Index(int id)
        //{
        //    var _mxtmp = ob_wms_rukumxservice.LoadSortEntities(p => p.RukuID == id && p.IsDelete == false, true, s => s.ShangpinMC);
        //    ViewBag.wms_rukumx = _mxtmp;
        //    return View(_mxtmp);
        //}
        //public ActionResult Add()
        //{
        //    ViewBag.userid = (int)Session["user_id"];
        //    return View();
        //}
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            var srkid = Request["rkid"] ?? "";
            int rkid;
            if (srkid.Length == 0)
                rkid = 0;
            else
                rkid = int.Parse(srkid);

            ViewBag.rkdid = rkid;
            return View();
        }
        public ActionResult AddOn()
        {
            ViewBag.userid = (int)Session["user_id"];
            var srkid = Request["rkid"] ?? "";
            int rkid;
            if (srkid.Length == 0)
                rkid = 0;
            else
                rkid = int.Parse(srkid);

            ViewBag.rkdid = rkid;
            return View();
        }
        public JsonResult ImportData()
        {
            int _userid = (int)Session["user_id"];
            var _bh = Request["bh"] ?? "";
            var _rkid = Request["rk"] ?? "";

            wms_rukudan _rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkid) && p.IsDelete == false);
            if (_rkd == null)
                return Json(-1);
            if (_rkd.GongyingshangID != 2)
                return Json(-2);
            json_delivery _del = ServiceFactory.json_deliveryservice.GetEntityById(p => p.DELIVERY_NUMBER == _bh && p.IsDelete == false);
            if (_del == null)
                return Json(-3);
            json_import _imp = ServiceFactory.json_importservice.GetEntityById(p => p.BillNum == _del.DELIVERY_NUMBER && p.PlanID == _rkd.ID && p.IsDelete == false);
            if (_imp != null)
                return Json(-4);
            int _i = ServiceFactory.json_batchservice.ImportBatch(_del.DELIVERY_NUMBER, _rkd.KehuDH, _rkd.ID, _del.ID, _userid);
            return Json(_i);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string rukuid = Request["rukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string daohuosl = Request["daohuosl"] ?? "";
            string yishousl = Request["yishousl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
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
            try
            {
                wms_rukumx ob_wms_rukumx = new wms_rukumx();
                ob_wms_rukumx.RukuID = rukuid == "" ? 0 : int.Parse(rukuid);
                ob_wms_rukumx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_wms_rukumx.ShangpinMC = shangpinmc.Trim();
                ob_wms_rukumx.Zhucezheng = zhucezheng.Trim();
                ob_wms_rukumx.Guige = guige.Trim();
                ob_wms_rukumx.Pihao = pihao.Trim();
                ob_wms_rukumx.Pihao1 = pihao1.Trim();
                ob_wms_rukumx.Xuliema = xuliema.Trim();
                ob_wms_rukumx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_wms_rukumx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_wms_rukumx.DaohuoSL = daohuosl == "" ? 0 : float.Parse(daohuosl);
                ob_wms_rukumx.YishouSL = yishousl == "" ? 0 : float.Parse(yishousl);
                ob_wms_rukumx.JibenDW = jibendw.Trim();
                ob_wms_rukumx.BaozhuangDW = baozhuangdw.Trim();
                ob_wms_rukumx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_wms_rukumx.Changjia = changjia.Trim();
                ob_wms_rukumx.Chandi = chandi.Trim();
                ob_wms_rukumx.ShangpinTM = shangpintm.Trim();
                ob_wms_rukumx.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_rukumx.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_rukumx.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_rukumx.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_rukumx.Beizhu = beizhu.Trim();
                ob_wms_rukumx.Col1 = col1.Trim();
                ob_wms_rukumx.Col2 = col2.Trim();
                ob_wms_rukumx.Col3 = col3.Trim();
                ob_wms_rukumx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_rukumx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_rukumx.ShangpinDM = shangpindm.Trim();
                ob_wms_rukumx = ob_wms_rukumxservice.AddEntity(ob_wms_rukumx);
                ViewBag.wms_rukumx = ob_wms_rukumx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("GetCargos", new { id = rukuid });
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_rukumx tempData = ob_wms_rukumxservice.GetEntityById(wms_rukumx => wms_rukumx.ID == id && wms_rukumx.IsDelete == false);
            ViewBag.wms_rukumx = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_rukumxViewModel wms_rukumxviewmodel = new wms_rukumxViewModel();
                wms_rukumxviewmodel.ID = tempData.ID;
                wms_rukumxviewmodel.RukuID = tempData.RukuID;
                wms_rukumxviewmodel.ShangpinID = tempData.ShangpinID;
                wms_rukumxviewmodel.ShangpinMC = tempData.ShangpinMC;
                wms_rukumxviewmodel.Zhucezheng = tempData.Zhucezheng;
                wms_rukumxviewmodel.Guige = tempData.Guige;
                wms_rukumxviewmodel.Pihao = tempData.Pihao;
                wms_rukumxviewmodel.Pihao1 = tempData.Pihao1;
                wms_rukumxviewmodel.Xuliema = tempData.Xuliema;
                wms_rukumxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                wms_rukumxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                wms_rukumxviewmodel.DaohuoSL = tempData.DaohuoSL;
                wms_rukumxviewmodel.YishouSL = tempData.YishouSL;
                wms_rukumxviewmodel.JibenDW = tempData.JibenDW;
                wms_rukumxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                wms_rukumxviewmodel.Huansuanlv = tempData.Huansuanlv;
                wms_rukumxviewmodel.Changjia = tempData.Changjia;
                wms_rukumxviewmodel.Chandi = tempData.Chandi;
                wms_rukumxviewmodel.ShangpinTM = tempData.ShangpinTM;
                wms_rukumxviewmodel.Zhongliang = tempData.Zhongliang;
                wms_rukumxviewmodel.Jingzhong = tempData.Jingzhong;
                wms_rukumxviewmodel.Tiji = tempData.Tiji;
                wms_rukumxviewmodel.Jifeidun = tempData.Jifeidun;
                wms_rukumxviewmodel.Beizhu = tempData.Beizhu;
                wms_rukumxviewmodel.Col1 = tempData.Col1;
                wms_rukumxviewmodel.Col2 = tempData.Col2;
                wms_rukumxviewmodel.Col3 = tempData.Col3;
                wms_rukumxviewmodel.MakeDate = tempData.MakeDate;
                wms_rukumxviewmodel.MakeMan = tempData.MakeMan;
                wms_rukumxviewmodel.ShangpinDM = tempData.ShangpinDM;
                return View(wms_rukumxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rukuid = Request["rukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string daohuosl = Request["daohuosl"] ?? "";
            string yishousl = Request["yishousl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
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
            int uid = int.Parse(id);
            try
            {
                wms_rukumx p = ob_wms_rukumxservice.GetEntityById(wms_rukumx => wms_rukumx.ID == uid);
                p.RukuID = rukuid == "" ? 0 : int.Parse(rukuid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.DaohuoSL = daohuosl == "" ? 0 : float.Parse(daohuosl);
                p.YishouSL = yishousl == "" ? 0 : float.Parse(yishousl);
                p.JibenDW = jibendw.Trim();
                p.BaozhuangDW = baozhuangdw.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Changjia = changjia.Trim();
                p.Chandi = chandi.Trim();
                p.ShangpinTM = shangpintm.Trim();
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
                ob_wms_rukumxservice.UpdateEntity(p);
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
            wms_rukumx ob_wms_rukumx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_rukumx = ob_wms_rukumxservice.GetEntityById(wms_rukumx => wms_rukumx.ID == id && wms_rukumx.IsDelete == false);
                    if (ob_wms_rukumx.YishouSL == 0)
                    {
                        ob_wms_rukumx.IsDelete = true;
                        ob_wms_rukumxservice.UpdateEntity(ob_wms_rukumx);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PrintRuKuMX()
        {
            var rkmxid = Request["rkmxid"] ?? "";
            ViewBag.rkmxid = rkmxid;

            return View();
        }

    }
}

