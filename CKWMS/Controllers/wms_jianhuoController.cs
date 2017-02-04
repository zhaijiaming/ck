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
    public class wms_jianhuoController : Controller
    {
        private Iwms_jianhuoService ob_wms_jianhuoservice = ServiceFactory.wms_jianhuoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_jianhuo_index";
            Expression<Func<wms_jianhuo, bool>> where = PredicateExtensionses.True<wms_jianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ckmxid":
                            string ckmxid = scld[1];
                            string ckmxidequal = scld[2];
                            string ckmxidand = scld[3];
                            if (!string.IsNullOrEmpty(ckmxid))
                            {
                                if (ckmxidequal.Equals("="))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                                }
                                if (ckmxidequal.Equals(">"))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                                }
                                if (ckmxidequal.Equals("<"))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_jianhuo => wms_jianhuo.IsDelete == false);

            var tempData = ob_wms_jianhuoservice.LoadSortEntities(where.Compile(), false, wms_jianhuo => wms_jianhuo.ID).ToPagedList<wms_jianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_jianhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_jianhuo_index";
            string page = "1";
            string ckmxid = Request["ckmxid"] ?? "";
            string ckmxidequal = Request["ckmxidequal"] ?? "";
            string ckmxidand = Request["ckmxidand"] ?? "";
            Expression<Func<wms_jianhuo, bool>> where = PredicateExtensionses.True<wms_jianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(ckmxid))
                {
                    if (ckmxidequal.Equals("="))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals(">"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals("<"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                    }
                }
                if (!string.IsNullOrEmpty(ckmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", ckmxid, ckmxidequal, ckmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", "", ckmxidequal, ckmxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(ckmxid))
                {
                    if (ckmxidequal.Equals("="))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals(">"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals("<"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                    }
                }
                if (!string.IsNullOrEmpty(ckmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", ckmxid, ckmxidequal, ckmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", "", ckmxidequal, ckmxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_jianhuo => wms_jianhuo.IsDelete == false);

            var tempData = ob_wms_jianhuoservice.LoadSortEntities(where.Compile(), false, wms_jianhuo => wms_jianhuo.ID).ToPagedList<wms_jianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_jianhuo = tempData;
            return View(tempData);
        }
        public ActionResult PrintJianhuodan()
        {
            var id = Request["id"] ?? "";
            if (id.Length < 1)
                id = "0";
            var _ckmxs = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == int.Parse(id)).ToList();
            var _cksl = _ckmxs.Sum(p => p.ChukuSL);
            var _jhsl = _ckmxs.Sum(p => p.JianhuoSL);
            if (_cksl != _jhsl)
                ViewBag.equalvalue = 0;
            else
                ViewBag.equalvalue = 1;
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintCKfuhejianyan()
        {
            var ckjy_id = Request["id"] ?? "";
            ViewBag.id = ckjy_id;
            return View();
        }
        public ActionResult GetPickDetailByOut()
        {
            int _userid = (int)Session["user_id"];
            var _outid = Request["out"] ?? "";
            if (_outid.Length < 1)
                _outid = "0";
            var _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid));
            ViewBag.chukubh = _ckd.ChukudanBH;

            var _outdetail = ServiceFactory.wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, true, s => s.Guige).ToList<wms_chukumx>();
            ViewBag.outdetail = _outdetail;
            ViewBag.chukudan = _outid;
            //总数和总计
            ViewBag.linecount = _outdetail.Count;
            ViewBag.totalproduct = _outdetail.Sum(p => p.ChukuSL);
            ViewBag.JianhuoSLs = _outdetail.Sum(p => p.JianhuoSL);

            return View();
        }
        public JsonResult GetPickByOut()
        {
            int _userid = (int)Session["user_id"];
            var _outid = Request["out"] ?? "";
            if (string.IsNullOrEmpty(_outid))
                _outid = "0";
            var _pickdetail = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_outid), p => p.DaijianSL > 0).OrderByDescending(s => s.JianhuoRQ);
            if (_pickdetail == null)
                return Json(-1);
            return Json(_pickdetail.ToList<wms_pick_v>());
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult GetStoreCargo()
        {
            int _userid = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            int _custid = 0;
            if (_mxid == "")
                return Json(-1);
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();

            wms_chukumx _mx = ServiceFactory.wms_chukumxservice.GetEntityById(p => p.ID == int.Parse(_mxid));
            if (_mx == null)
                return Json(-1);
            wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == _mx.ChukuID);
            _custid = (int)_ckd.HuozhuID;
            if (_mx.ShangpinID != null)
                where = where.And(p => p.ShangpinID == _mx.ShangpinID);
            //if (_mx.ShangpinDM != null)
            //    where = where.And(p => p.ShangpinDM == _mx.ShangpinDM);
            //if (_mx.ShangpinMC != null)
            //    where = where.And(p => p.ShangpinMC == _mx.ShangpinMC);
            //if (_mx.ShangpinTM != null)
            //    where = where.And(p => p.ShangpinTM == _mx.ShangpinTM);
            //if (_mx.Guige != null)
            //    where = where.And(p => p.Guige == _mx.Guige);
            //if (_mx.Zhucezheng != null)
            //    where = where.And(p => p.Zhucezheng == _mx.Zhucezheng);
            if (_mx.Pihao != null)
                where = where.And(p => p.Pihao.Contains(_mx.Pihao));
            if (_mx.Pihao1 != null)
                where = where.And(p => p.Pihao1.Contains(_mx.Pihao1));
            //if (_mx.ShengchanRQ != null)
            //    where = where.And(p => p.ShengchanRQ == _mx.ShengchanRQ);
            //if (_mx.ShixiaoRQ != null)
            //    where = where.And(p => p.ShixiaoRQ == _mx.ShixiaoRQ);
            if (_mx.Xuliema != null)
                where = where.And(p => p.Xuliema.Contains(_mx.Xuliema));
            if (_mx.HuopinZT != null)
                where = where.And(p => p.CunhuoZT == _mx.HuopinZT);
            where = where.And(p => p.sshuliang > 0);
            var tempData = ServiceFactory.wms_cunhuoservice.GetStorageList(_custid, where.Compile()).ToList<wms_storage_v>();//.OrderBy(s=>s.ShixiaoRQ).ThenBy(s=>s.RukuRQ);
            if (tempData == null)
                return Json(-1);
            return Json(tempData);
        }
        public JsonResult AddPickGoods()
        {
            int _userid = (int)Session["user_id"];
            var _pickgoods = Request["pickgd"] ?? "";
            if (_pickgoods == "")
                return Json(-1);
            foreach (var _pg in _pickgoods.Split(';'))
            {
                if (_pg.Length > 3)
                {
                    string[] _rec = _pg.Split(',');
                    int _ckmx = int.Parse(_rec[0]);
                    float _pknum = float.Parse(_rec[1]);
                    string _pkmemo = _rec[2];
                    int _chmx = int.Parse(_rec[3]);

                    wms_cunhuo _ch = ServiceFactory.wms_cunhuoservice.GetEntityById(p => p.ID == _chmx);
                    if (_ch != null)
                    {
                        if (_pknum > _ch.Shuliang - _ch.DaijianSL)
                            return Json(-2);
                        var _rt = _pknum / _ch.Shuliang;
                        wms_jianhuo _jh = new wms_jianhuo();
                        _jh.CKMXID = _ckmx;
                        _jh.DaijianSL = _pknum;
                        _jh.JianhuoRQ = DateTime.Now;
                        _jh.JianhuoSM = _pkmemo;
                        _jh.KCID = _chmx;
                        _jh.Fuhe = _ch.HegeSF;
                        _jh.Kuwei = _ch.Kuwei;
                        _jh.KuweiID = _ch.KuweiID;
                        _jh.Zhongliang = (float)Math.Round((double)(_rt * _ch.Zhongliang), 3);
                        _jh.Jingzhong = (float)Math.Round((double)(_rt * _ch.Jingzhong), 3);
                        _jh.Tiji = (float)Math.Round((double)(_rt * _ch.Tiji), 3);
                        _jh.Jifeidun = (float)Math.Round((double)(_rt * _ch.Jifeidun), 3);
                        _jh.MakeMan = _userid;
                        //扫描拣货后去除
                        _jh.Jianhuoren = _userid;
                        //_jh.ShijianSL = _pknum;
                        _jh = ob_wms_jianhuoservice.AddEntity(_jh);
                        if (_jh != null)
                        {
                            wms_chukumx _ck = ServiceFactory.wms_chukumxservice.GetEntityById(p => p.ID == _ckmx);
                            if (_ck != null)
                            {
                                _ck.Jianhuo = true;
                                if (_ck.JianhuoSL == null)
                                    _ck.JianhuoSL = 0;
                                _ck.JianhuoSL = _ck.JianhuoSL + _pknum;
                                ServiceFactory.wms_chukumxservice.UpdateEntity(_ck);
                            }
                        }
                    }
                }
            }
            return Json(1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string ckmxid = Request["ckmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string shijiansl = Request["shijiansl"] ?? "";
            string jianhuosm = Request["jianhuosm"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            try
            {
                wms_jianhuo ob_wms_jianhuo = new wms_jianhuo();
                ob_wms_jianhuo.CKMXID = ckmxid == "" ? 0 : int.Parse(ckmxid);
                ob_wms_jianhuo.Kuwei = kuwei.Trim();
                ob_wms_jianhuo.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                ob_wms_jianhuo.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                ob_wms_jianhuo.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_jianhuo.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_jianhuo.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_jianhuo.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_jianhuo.ShijianSL = shijiansl == "" ? 0 : float.Parse(shijiansl);
                ob_wms_jianhuo.JianhuoSM = jianhuosm.Trim();
                ob_wms_jianhuo.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                ob_wms_jianhuo.Col1 = col1.Trim();
                ob_wms_jianhuo.Col2 = col2.Trim();
                ob_wms_jianhuo.Col3 = col3.Trim();
                ob_wms_jianhuo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_jianhuo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_jianhuo.Fuhe = fuhe == "" ? true : Boolean.Parse(fuhe);
                ob_wms_jianhuo = ob_wms_jianhuoservice.AddEntity(ob_wms_jianhuo);
                ViewBag.wms_jianhuo = ob_wms_jianhuo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetDetailByMX()
        {
            int _userid = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            if (_mxid == "")
                return Json(-1);
            var tempData = ob_wms_jianhuoservice.GetPickDetailByMX(int.Parse(_mxid), p => p.DaijianSL > 0);
            if (tempData == null)
                return Json(-1);
            return Json(tempData.ToList<wms_pick_v>());
        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_jianhuo tempData = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == id && wms_jianhuo.IsDelete == false);
            ViewBag.wms_jianhuo = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_jianhuoViewModel wms_jianhuoviewmodel = new wms_jianhuoViewModel();
                wms_jianhuoviewmodel.ID = tempData.ID;
                wms_jianhuoviewmodel.CKMXID = tempData.CKMXID;
                wms_jianhuoviewmodel.Kuwei = tempData.Kuwei;
                wms_jianhuoviewmodel.KuweiID = tempData.KuweiID;
                wms_jianhuoviewmodel.DaijianSL = tempData.DaijianSL;
                wms_jianhuoviewmodel.Zhongliang = tempData.Zhongliang;
                wms_jianhuoviewmodel.Jingzhong = tempData.Jingzhong;
                wms_jianhuoviewmodel.Tiji = tempData.Tiji;
                wms_jianhuoviewmodel.Jifeidun = tempData.Jifeidun;
                wms_jianhuoviewmodel.ShijianSL = tempData.ShijianSL;
                wms_jianhuoviewmodel.JianhuoSM = tempData.JianhuoSM;
                wms_jianhuoviewmodel.Jianhuoren = tempData.Jianhuoren;
                wms_jianhuoviewmodel.Col1 = tempData.Col1;
                wms_jianhuoviewmodel.Col2 = tempData.Col2;
                wms_jianhuoviewmodel.Col3 = tempData.Col3;
                wms_jianhuoviewmodel.MakeDate = tempData.MakeDate;
                wms_jianhuoviewmodel.MakeMan = tempData.MakeMan;
                wms_jianhuoviewmodel.Fuhe = tempData.Fuhe;
                return View(wms_jianhuoviewmodel);
            }
        }
        public JsonResult DelPick()
        {
            int _userid = (int)Session["user_id"];
            var _delid = Request["del"] ?? "";
            var _ckid = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_delid) || string.IsNullOrEmpty(_ckid))
                return Json(-1);
            wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            if (_ckd == null)
                return Json(-1);
            if (_ckd.JihuaZT > 2)
                return Json(-2);
            wms_jianhuo _jh = ob_wms_jianhuoservice.GetEntityById(p => p.ID == int.Parse(_delid) && p.IsDelete==false);
            if (_jh == null)
                return Json(-1);
            var b = ob_wms_jianhuoservice.DeleteEntity(_jh);
            if (!b)
                return Json(-1);
            return Json(1);
        }
        public JsonResult NGChange()
        {
            int _userid = (int)Session["user_id"];
            var _ngv = Request["ch"] ?? "";
            if (_ngv == "" || _ngv.Length < 2)
                return Json(-1);
            foreach (var ng in _ngv.Split(';'))
            {
                if (ng.Length > 2)
                {
                    string[] _np = ng.Split(',');
                    if (int.Parse(_np[0]) > 0 && int.Parse(_np[1]) > 0)
                    {
                        wms_jianhuo _jh = ob_wms_jianhuoservice.GetEntityById(p => p.ID == int.Parse(_np[0]));
                        if (_jh != null)
                        {
                            wms_jianhuo _njh = ob_wms_jianhuoservice.AddNGPick(_jh, int.Parse(_np[1]));
                            if (_njh != null)
                            {
                                _njh.Fuhe = false;
                                ob_wms_jianhuoservice.UpdateEntity(_njh);
                            }
                        }
                    }
                }
            }
            return Json(1);
        }
        public JsonResult NGPick()
        {
            int _userid = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            if (_mxid == "")
                return Json(-1);
            var _pklist = ob_wms_jianhuoservice.GetPickDetailByMX(int.Parse(_mxid), p => p.DaijianSL > 0);
            if (_pklist != null)
            {
                foreach (var _pk in _pklist.ToList<wms_pick_v>())
                {
                    var _pid = _pk.pickid;
                    wms_jianhuo _jh = ob_wms_jianhuoservice.GetEntityById(g => g.ID ==_pid && g.IsDelete==false);
                    if (_jh != null)
                    {
                        _jh.Fuhe = false;
                        ob_wms_jianhuoservice.UpdateEntity(_jh);
                    }
                }
            }
            return Json(1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ckmxid = Request["ckmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string shijiansl = Request["shijiansl"] ?? "";
            string jianhuosm = Request["jianhuosm"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_jianhuo p = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == uid);
                p.CKMXID = ckmxid == "" ? 0 : int.Parse(ckmxid);
                p.Kuwei = kuwei.Trim();
                p.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                p.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                p.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                p.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                p.ShijianSL = shijiansl == "" ? 0 : float.Parse(shijiansl);
                p.JianhuoSM = jianhuosm.Trim();
                p.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Fuhe = fuhe == "" ? false : Boolean.Parse(fuhe);
                ob_wms_jianhuoservice.UpdateEntity(p);
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
            wms_jianhuo ob_wms_jianhuo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_jianhuo = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == id && wms_jianhuo.IsDelete == false);
                    ob_wms_jianhuo.IsDelete = true;
                    ob_wms_jianhuoservice.UpdateEntity(ob_wms_jianhuo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

