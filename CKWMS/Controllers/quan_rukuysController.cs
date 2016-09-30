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
    public class quan_rukuysController : Controller
    {
        private Iquan_rukuysService ob_quan_rukuysservice = ServiceFactory.quan_rukuysservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_rukuys_index";
            Expression<Func<quan_inrec_v, bool>> where = PredicateExtensionses.True<quan_inrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mingxiid":
                            string mingxiid = scld[1];
                            string mingxiidequal = scld[2];
                            string mingxiidand = scld[3];
                            if (!string.IsNullOrEmpty(mingxiid))
                            {
                                if (mingxiidequal.Equals("="))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals(">"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals("<"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_quan_rukuysservice.GetInrec(where.Compile()).ToPagedList<quan_inrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_inrec = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_rukuys_index";
            string page = "1";
            string mingxiid = Request["mingxiid"] ?? "";
            string mingxiidequal = Request["mingxiidequal"] ?? "";
            string mingxiidand = Request["mingxiidand"] ?? "";
            Expression<Func<quan_inrec_v, bool>> where = PredicateExtensionses.True<quan_inrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(mingxiid))
                {
                    if (mingxiidequal.Equals("="))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals(">"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals("<"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                    }
                }
                if (!string.IsNullOrEmpty(mingxiid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", mingxiid, mingxiidequal, mingxiidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", "", mingxiidequal, mingxiidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(mingxiid))
                {
                    if (mingxiidequal.Equals("="))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID == int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals(">"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID > int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals("<"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                        else
                            where = where.Or(quan_inrec_v => quan_inrec_v.ID < int.Parse(mingxiid));
                    }
                }
                if (!string.IsNullOrEmpty(mingxiid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", mingxiid, mingxiidequal, mingxiidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", "", mingxiidequal, mingxiidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            var tempData = ob_quan_rukuysservice.GetInrec(where.Compile()).ToPagedList<quan_inrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_inrec = tempData;
            return View(tempData);
        }
        [OutputCache(Duration =30)]
        public ActionResult EntryCheckList(int id)
        {
            var _username = Session["user_name"];
            var _userid = Session["user_id"];
            var tempData = ob_quan_rukuysservice.GetEntrycheckByRK(id);
            ViewBag.userid = _userid;
            ViewBag.username = _username;
            ViewBag.quan_entrycheck = tempData;
            return View();
        }
        public JsonResult AddCheckPart()
        {
            int _userid = (int)Session["user_id"];
            string _shmxid = Request["shmx"] ?? "";
            string _ysslok = Request["oknum"] ?? "";
            string _ysslng = Request["ngnum"] ?? "";
            string _ysresult = Request["ys"] ?? "";
            string _ysren = Request["ysr"] ?? "";
            string _yssm = Request["yssm"] ?? "";
            try
            {
                if (int.Parse(_shmxid) == 0 || _shmxid.Length == 0)
                    return Json(-1);
                wms_shouhuomx _orishmx = ServiceFactory.wms_shouhuomxservice.GetEntityById(p => p.ID ==int.Parse(_shmxid));
                if (_orishmx == null)
                    return Json(-1);
                wms_shouhuomx _newshmx = new wms_shouhuomx();
                _newshmx.BaozhuangDW = _orishmx.BaozhuangDW;
                _newshmx.Beizhu = _orishmx.Beizhu;
                _newshmx.Chandi = _orishmx.Chandi;
                _newshmx.Changjia = _orishmx.Changjia;
                _newshmx.Guige = _orishmx.Guige;
                _newshmx.Huansuanlv = _orishmx.Huansuanlv;
                _newshmx.HuopinZT = _orishmx.HuopinZT;
                _newshmx.JibenDW = _orishmx.JibenDW;
                _newshmx.Jifeidun = _orishmx.Jifeidun;
                _newshmx.Jingzhong = _orishmx.Jingzhong;
                _newshmx.MakeDate = _orishmx.MakeDate;
                _newshmx.MakeMan = _orishmx.MakeMan;
                _newshmx.Pihao= _orishmx.Pihao;
                _newshmx.Pihao1 = _orishmx.Pihao1;
                _newshmx.RKMXID = _orishmx.RKMXID;
                _newshmx.RukuID = _orishmx.RukuID;
                _newshmx.ShangpinDM = _orishmx.ShangpinDM;
                _newshmx.ShangpinID = _orishmx.ShangpinID;
                _newshmx.ShangpinMC = _orishmx.ShangpinMC;
                _newshmx.ShangpinTM = _orishmx.ShangpinTM;
                _newshmx.ShengchanRQ = _orishmx.ShengchanRQ;
                _newshmx.ShixiaoRQ = _orishmx.ShixiaoRQ;
                _newshmx.Shuliang = _orishmx.Shuliang;
                _newshmx.Tiji = _orishmx.Tiji;
                _newshmx.Xuliema = _orishmx.Xuliema;
                _newshmx.Yanshou= _orishmx.Yanshou;
                _newshmx.Zhongliang = _orishmx.Zhongliang;
                _newshmx.Zhucezheng = _orishmx.Zhucezheng;
                _newshmx.IsDelete = _orishmx.IsDelete;
                _newshmx=ServiceFactory.wms_shouhuomxservice.AddEntity(_newshmx);

                quan_rukuys ob_quan_rukuys = new quan_rukuys();
                ob_quan_rukuys.MingxiID = _orishmx.ID;
                ob_quan_rukuys.YanshouSL = _ysslok == "" ? 0 : float.Parse(_ysslok);
                ob_quan_rukuys.Yanshou = 1;
                ob_quan_rukuys.Yanshouren = _ysren.Trim();
                ob_quan_rukuys.YanshouSM = _yssm.Trim();
                ob_quan_rukuys.YanshouZT = 3;
                ob_quan_rukuys.MakeDate = DateTime.Now;
                ob_quan_rukuys.MakeMan = _userid;
                ob_quan_rukuys = ob_quan_rukuysservice.AddEntity(ob_quan_rukuys);

                _orishmx.Shuliang= _ysslok == "" ? 0 : float.Parse(_ysslok);
                ServiceFactory.wms_shouhuomxservice.UpdateEntity(_orishmx);

                quan_rukuys ob_quan_rukuys1 = new quan_rukuys();
                ob_quan_rukuys1.MingxiID = _newshmx.ID;
                ob_quan_rukuys1.YanshouSL = _ysslng == "" ? 0 : float.Parse(_ysslng);
                ob_quan_rukuys1.Yanshou = 3;
                ob_quan_rukuys1.Yanshouren = _ysren.Trim();
                ob_quan_rukuys1.YanshouSM = _yssm.Trim();
                ob_quan_rukuys1.YanshouZT = 3;
                ob_quan_rukuys1.MakeDate = DateTime.Now;
                ob_quan_rukuys1.MakeMan = _userid;
                ob_quan_rukuys1 = ob_quan_rukuysservice.AddEntity(ob_quan_rukuys1);

                _newshmx.Shuliang= _ysslng == "" ? 0 : float.Parse(_ysslng);
                ServiceFactory.wms_shouhuomxservice.UpdateEntity(_newshmx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(-1);
            }
            return Json(1);
        }
        public JsonResult AddCheck()
        {
            int _userid = (int)Session["user_id"];
            string _shmxid = Request["shmx"] ?? "";
            string _yssl = Request["sl"] ?? "";
            string _ysresult = Request["ys"] ?? "";
            string _ysren = Request["ysr"] ?? "";
            string _yssm = Request["yssm"] ?? "";
            try
            {
                quan_rukuys ob_quan_rukuys = new quan_rukuys();
                ob_quan_rukuys.MingxiID = _shmxid == "" ? 0 : int.Parse(_shmxid);
                ob_quan_rukuys.YanshouSL = _yssl == "" ? 0 : float.Parse(_yssl);
                ob_quan_rukuys.Yanshou = _ysresult == "" ? 0 : int.Parse(_ysresult);
                ob_quan_rukuys.Yanshouren = _ysren.Trim();
                ob_quan_rukuys.YanshouSM = _yssm.Trim();
                ob_quan_rukuys.YanshouZT = 3;
                ob_quan_rukuys.MakeDate = DateTime.Now;
                ob_quan_rukuys.MakeMan =_userid;
                ob_quan_rukuys = ob_quan_rukuysservice.AddEntity(ob_quan_rukuys);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(-1);
            }
            return Json(1);
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
            string mingxiid = Request["mingxiid"] ?? "";
            string yanshousl = Request["yanshousl"] ?? "";
            string yanshou = Request["yanshou"] ?? "";
            string yanshouren = Request["yanshouren"] ?? "";
            string yanshousm = Request["yanshousm"] ?? "";
            string yanshouzt = Request["yanshouzt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                quan_rukuys ob_quan_rukuys = new quan_rukuys();
                ob_quan_rukuys.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                ob_quan_rukuys.YanshouSL = yanshousl == "" ? 0 : float.Parse(yanshousl);
                ob_quan_rukuys.Yanshou = yanshou == "" ? 0 : int.Parse(yanshou);
                ob_quan_rukuys.Yanshouren = yanshouren.Trim();
                ob_quan_rukuys.YanshouSM = yanshousm.Trim();
                ob_quan_rukuys.YanshouZT = yanshouzt == "" ? 0 : int.Parse(yanshouzt);
                ob_quan_rukuys.Col1 = col1.Trim();
                ob_quan_rukuys.Col2 = col2.Trim();
                ob_quan_rukuys.Col3 = col3.Trim();
                ob_quan_rukuys.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_rukuys.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_rukuys = ob_quan_rukuysservice.AddEntity(ob_quan_rukuys);
                ViewBag.quan_rukuys = ob_quan_rukuys;
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
            quan_rukuys tempData = ob_quan_rukuysservice.GetEntityById(quan_rukuys => quan_rukuys.ID == id && quan_rukuys.IsDelete == false);
            ViewBag.quan_rukuys = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_rukuysViewModel quan_rukuysviewmodel = new quan_rukuysViewModel();
                quan_rukuysviewmodel.ID = tempData.ID;
                quan_rukuysviewmodel.MingxiID = tempData.MingxiID;
                quan_rukuysviewmodel.YanshouSL = tempData.YanshouSL;
                quan_rukuysviewmodel.Yanshou = tempData.Yanshou;
                quan_rukuysviewmodel.Yanshouren = tempData.Yanshouren;
                quan_rukuysviewmodel.YanshouSM = tempData.YanshouSM;
                quan_rukuysviewmodel.YanshouZT = tempData.YanshouZT;
                quan_rukuysviewmodel.Col1 = tempData.Col1;
                quan_rukuysviewmodel.Col2 = tempData.Col2;
                quan_rukuysviewmodel.Col3 = tempData.Col3;
                quan_rukuysviewmodel.MakeDate = tempData.MakeDate;
                quan_rukuysviewmodel.MakeMan = tempData.MakeMan;
                return View(quan_rukuysviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string mingxiid = Request["mingxiid"] ?? "";
            string yanshousl = Request["yanshousl"] ?? "";
            string yanshou = Request["yanshou"] ?? "";
            string yanshouren = Request["yanshouren"] ?? "";
            string yanshousm = Request["yanshousm"] ?? "";
            string yanshouzt = Request["yanshouzt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_rukuys p = ob_quan_rukuysservice.GetEntityById(quan_rukuys => quan_rukuys.ID == uid);
                p.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                p.YanshouSL = yanshousl == "" ? 0 : float.Parse(yanshousl);
                p.Yanshou = yanshou == "" ? 0 : int.Parse(yanshou);
                p.Yanshouren = yanshouren.Trim();
                p.YanshouSM = yanshousm.Trim();
                p.YanshouZT = yanshouzt == "" ? 0 : int.Parse(yanshouzt);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_rukuysservice.UpdateEntity(p);
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
            quan_rukuys ob_quan_rukuys;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_rukuys = ob_quan_rukuysservice.GetEntityById(quan_rukuys => quan_rukuys.ID == id && quan_rukuys.IsDelete == false);
                    ob_quan_rukuys.IsDelete = true;
                    ob_quan_rukuysservice.UpdateEntity(ob_quan_rukuys);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

