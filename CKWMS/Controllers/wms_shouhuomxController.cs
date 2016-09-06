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
    public class wms_shouhuomxController : Controller
    {
        private Iwms_shouhuomxService ob_wms_shouhuomxservice = ServiceFactory.wms_shouhuomxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_shouhuomx_index";
            Expression<Func<wms_shouhuomx, bool>> where = PredicateExtensionses.True<wms_shouhuomx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rkmxid":
                            string rkmxid = scld[1];
                            string rkmxidequal = scld[2];
                            string rkmxidand = scld[3];
                            if (!string.IsNullOrEmpty(rkmxid))
                            {
                                if (rkmxidequal.Equals("="))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals(">"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                                }
                                if (rkmxidequal.Equals("<"))
                                {
                                    if (rkmxidand.Equals("and"))
                                        where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                                    else
                                        where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_shouhuomx => wms_shouhuomx.IsDelete == false);

            var tempData = ob_wms_shouhuomxservice.LoadSortEntities(where.Compile(), false, wms_shouhuomx => wms_shouhuomx.ID).ToPagedList<wms_shouhuomx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_shouhuomx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_shouhuomx_index";
            string page = "1";
            string rkmxid = Request["rkmxid"] ?? "";
            string rkmxidequal = Request["rkmxidequal"] ?? "";
            string rkmxidand = Request["rkmxidand"] ?? "";
            Expression<Func<wms_shouhuomx, bool>> where = PredicateExtensionses.True<wms_shouhuomx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rkmxid))
                {
                    if (rkmxidequal.Equals("="))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID == int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals(">"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID > int.Parse(rkmxid));
                    }
                    if (rkmxidequal.Equals("<"))
                    {
                        if (rkmxidand.Equals("and"))
                            where = where.And(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                        else
                            where = where.Or(wms_shouhuomx => wms_shouhuomx.RKMXID < int.Parse(rkmxid));
                    }
                }
                if (!string.IsNullOrEmpty(rkmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", rkmxid, rkmxidequal, rkmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rkmxid", "", rkmxidequal, rkmxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_shouhuomx => wms_shouhuomx.IsDelete == false);

            var tempData = ob_wms_shouhuomxservice.LoadSortEntities(where.Compile(), false, wms_shouhuomx => wms_shouhuomx.ID).ToPagedList<wms_shouhuomx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_shouhuomx = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult Recieving(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string rkdid = Request["rkd"] ?? "";
            if (rkdid.Length == 0)
                rkdid = "0";
            //var tempData = ServiceFactory.wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(rkdid) && p.IsDelete==false && (p.DaohuoSL-p.YishouSL>0), true, s => s.ShangpinMC).ToPagedList<wms_rukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            var tempData = ServiceFactory.wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(rkdid) && p.IsDelete == false && (p.DaohuoSL - p.YishouSL > 0), true, s => s.ShangpinMC);
            ViewBag.wms_rukumx = tempData;
            ViewBag.rkdid = rkdid;
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
            string rkmxid = Request["rkmxid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
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
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            try
            {
                wms_shouhuomx ob_wms_shouhuomx = new wms_shouhuomx();
                ob_wms_shouhuomx.RKMXID = rkmxid == "" ? 0 : int.Parse(rkmxid);
                ob_wms_shouhuomx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_wms_shouhuomx.ShangpinMC = shangpinmc.Trim();
                ob_wms_shouhuomx.Zhucezheng = zhucezheng.Trim();
                ob_wms_shouhuomx.Guige = guige.Trim();
                ob_wms_shouhuomx.Pihao = pihao.Trim();
                ob_wms_shouhuomx.Pihao1 = pihao1.Trim();
                ob_wms_shouhuomx.Xuliema = xuliema.Trim();
                ob_wms_shouhuomx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_wms_shouhuomx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_wms_shouhuomx.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                ob_wms_shouhuomx.JibenDW = jibendw.Trim();
                ob_wms_shouhuomx.BaozhuangDW = baozhuangdw.Trim();
                ob_wms_shouhuomx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_wms_shouhuomx.Changjia = changjia.Trim();
                ob_wms_shouhuomx.Chandi = chandi.Trim();
                ob_wms_shouhuomx.ShangpinTM = shangpintm.Trim();
                ob_wms_shouhuomx.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_shouhuomx.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_shouhuomx.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_shouhuomx.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_shouhuomx.Col1 = col1.Trim();
                ob_wms_shouhuomx.Col2 = col2.Trim();
                ob_wms_shouhuomx.Col3 = col3.Trim();
                ob_wms_shouhuomx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_shouhuomx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_shouhuomx.Beizhu = beizhu.Trim();
                ob_wms_shouhuomx = ob_wms_shouhuomxservice.AddEntity(ob_wms_shouhuomx);
                ViewBag.wms_shouhuomx = ob_wms_shouhuomx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetShouhuoList()
        {
            var _rkdid = Request["rkd"] ?? "";
            if (_rkdid.Length < 1)
                return Json("");
            var _shouhuo = ServiceFactory.wms_shouhuomxservice.LoadSortEntities(p => p.IsDelete == false && p.RukuID == int.Parse(_rkdid), false, s => s.MakeDate);
            if (_shouhuo == null)
                return Json("");
            return Json(_shouhuo.ToList<wms_shouhuomx>());
        }
        public JsonResult AddByRukuMX()
        {
            var _rkmxid = Request["rk"] ?? "";
            var _user = (int)Session["user_id"];
            var _ph1 = Request["ph1"] ?? "";
            var _ph = Request["ph"] ?? "";
            var _xlm = Request["xlm"] ?? "";
            var _scrq = Request["scrq"] ?? "";
            var _sxrq = Request["sxrq"] ?? "";
            var _sl = Request["sl"] ?? "";
            var _zl = Request["zl"] ?? "";
            var _jz = Request["jz"] ?? "";
            var _tj = Request["tj"] ?? "";
            var _jfd = Request["jfd"] ?? "";
            var _bz = Request["bz"] ?? "";
            if (_sl.Length == 0)
                _sl = "0";
            if (_zl.Length == 0)
                _zl = "0";
            if (_jz.Length == 0)
                _jz = "0";
            if (_tj.Length == 0)
                _tj = "0";
            if (_jfd.Length == 0)
                _jfd = "0";
            try
            {
                wms_rukumx _rkmx = ServiceFactory.wms_rukumxservice.GetEntityById(p => p.ID == int.Parse(_rkmxid));
                if (_rkmx != null)
                {
                    wms_shouhuomx _shmx = new wms_shouhuomx();
                    _shmx.RKMXID = _rkmx.ID;
                    _shmx.RukuID = _rkmx.RukuID;
                    _shmx.ShangpinID = _rkmx.ShangpinID;
                    _shmx.ShangpinMC = _rkmx.ShangpinMC;
                    _shmx.ShangpinTM = _rkmx.ShangpinTM;
                    _shmx.Zhucezheng = _rkmx.Zhucezheng;
                    _shmx.JibenDW = _rkmx.JibenDW;
                    _shmx.BaozhuangDW = _rkmx.BaozhuangDW;
                    _shmx.Chandi = _rkmx.Chandi;
                    _shmx.Changjia = _rkmx.Changjia;
                    _shmx.Guige = _rkmx.Guige;
                    _shmx.Huansuanlv = _rkmx.Huansuanlv;

                    _shmx.Pihao = _ph;
                    _shmx.Pihao1 = _ph1;
                    _shmx.Xuliema = _xlm;
                    _shmx.ShengchanRQ = DateTime.Parse(_scrq);
                    _shmx.ShixiaoRQ = DateTime.Parse(_sxrq);
                    _shmx.Shuliang = float.Parse(_sl);
                    _shmx.Zhongliang = float.Parse(_zl);
                    _shmx.Jingzhong = float.Parse(_jz);
                    _shmx.Tiji = float.Parse(_tj);
                    _shmx.Jifeidun = float.Parse(_jfd);
                    _shmx.Beizhu = _bz;

                    _shmx.MakeMan = _user;
                    _shmx = ServiceFactory.wms_shouhuomxservice.AddEntity(_shmx);
                }
            }
            catch
            {
                return Json(-1);
            }
            return Json(0);
        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_shouhuomx tempData = ob_wms_shouhuomxservice.GetEntityById(wms_shouhuomx => wms_shouhuomx.ID == id && wms_shouhuomx.IsDelete == false);
            ViewBag.wms_shouhuomx = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_shouhuomxViewModel wms_shouhuomxviewmodel = new wms_shouhuomxViewModel();
                wms_shouhuomxviewmodel.ID = tempData.ID;
                wms_shouhuomxviewmodel.RKMXID = tempData.RKMXID;
                wms_shouhuomxviewmodel.ShangpinID = tempData.ShangpinID;
                wms_shouhuomxviewmodel.ShangpinMC = tempData.ShangpinMC;
                wms_shouhuomxviewmodel.Zhucezheng = tempData.Zhucezheng;
                wms_shouhuomxviewmodel.Guige = tempData.Guige;
                wms_shouhuomxviewmodel.Pihao = tempData.Pihao;
                wms_shouhuomxviewmodel.Pihao1 = tempData.Pihao1;
                wms_shouhuomxviewmodel.Xuliema = tempData.Xuliema;
                wms_shouhuomxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                wms_shouhuomxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                wms_shouhuomxviewmodel.Shuliang = tempData.Shuliang;
                wms_shouhuomxviewmodel.JibenDW = tempData.JibenDW;
                wms_shouhuomxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                wms_shouhuomxviewmodel.Huansuanlv = tempData.Huansuanlv;
                wms_shouhuomxviewmodel.Changjia = tempData.Changjia;
                wms_shouhuomxviewmodel.Chandi = tempData.Chandi;
                wms_shouhuomxviewmodel.ShangpinTM = tempData.ShangpinTM;
                wms_shouhuomxviewmodel.Zhongliang = tempData.Zhongliang;
                wms_shouhuomxviewmodel.Jingzhong = tempData.Jingzhong;
                wms_shouhuomxviewmodel.Tiji = tempData.Tiji;
                wms_shouhuomxviewmodel.Jifeidun = tempData.Jifeidun;
                wms_shouhuomxviewmodel.Col1 = tempData.Col1;
                wms_shouhuomxviewmodel.Col2 = tempData.Col2;
                wms_shouhuomxviewmodel.Col3 = tempData.Col3;
                wms_shouhuomxviewmodel.MakeDate = tempData.MakeDate;
                wms_shouhuomxviewmodel.MakeMan = tempData.MakeMan;
                wms_shouhuomxviewmodel.Beizhu = tempData.Beizhu;
                return View(wms_shouhuomxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rkmxid = Request["rkmxid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
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
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_shouhuomx p = ob_wms_shouhuomxservice.GetEntityById(wms_shouhuomx => wms_shouhuomx.ID == uid);
                p.RKMXID = rkmxid == "" ? 0 : int.Parse(rkmxid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
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
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Beizhu = beizhu.Trim();
                ob_wms_shouhuomxservice.UpdateEntity(p);
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
            wms_shouhuomx ob_wms_shouhuomx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_shouhuomx = ob_wms_shouhuomxservice.GetEntityById(wms_shouhuomx => wms_shouhuomx.ID == id && wms_shouhuomx.IsDelete == false);
                    ob_wms_shouhuomx.IsDelete = true;
                    ob_wms_shouhuomxservice.UpdateEntity(ob_wms_shouhuomx);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult DelByID()
        {
            string sdel = Request["del"] ?? "";
            int id;
            try
            {
                wms_shouhuomx ob_wms_shouhuomx;
                foreach (string sD in sdel.Split(','))
                {
                    if (sD.Length > 0)
                    {
                        id = int.Parse(sD);
                        ob_wms_shouhuomx = ob_wms_shouhuomxservice.GetEntityById(wms_shouhuomx => wms_shouhuomx.ID == id && wms_shouhuomx.IsDelete == false);
                        ob_wms_shouhuomx.IsDelete = true;
                        ob_wms_shouhuomxservice.UpdateEntity(ob_wms_shouhuomx);
                    }
                }
            }
            catch
            {
                return Json(-1);
            }
            return Json(1);
        }
    }
}

