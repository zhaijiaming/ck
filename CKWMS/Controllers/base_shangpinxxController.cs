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
    public class base_shangpinxxController : Controller
    {
        private Ibase_shangpinxxService ob_base_shangpinxxservice = ServiceFactory.base_shangpinxxservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinxx_index";
            Expression<Func<base_shangpinxx, bool>> where = PredicateExtensionses.True<base_shangpinxx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
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
                                        where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpinxx => base_shangpinxx.IsDelete == false);

            var tempData = ob_base_shangpinxxservice.LoadSortEntities(where.Compile(), false, base_shangpinxx => base_shangpinxx.ID).ToPagedList<base_shangpinxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinxx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinxx_index";
            string page = "1";
            //货主
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //商品代码
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //商品名称
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";

            Expression<Func<base_shangpinxx, bool>> where = PredicateExtensionses.True<base_shangpinxx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //货主
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //商品代码
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Daima == daima);
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Daima.Contains(daima));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //商品名称
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //货主
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //商品代码
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Daima == daima);
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Daima.Contains(daima));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //商品名称
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinxx => base_shangpinxx.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpinxx => base_shangpinxx.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shangpinxx => base_shangpinxx.IsDelete == false);

            var tempData = ob_base_shangpinxxservice.LoadSortEntities(where.Compile(), false, base_shangpinxx => base_shangpinxx.ID).ToPagedList<base_shangpinxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinxx = tempData;
            return View(tempData);
        }
        /// <summary>
        /// 货主的商品列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCustomerCargos()
        {
            string _sCust = Request["cust"] ?? "";
            if (_sCust.Length == 0)
                return Json("");
            else
            {
                //var _splist = ServiceFactory.base_shangpinxxservice.LoadSortEntities(p => p.HuozhuID == int.Parse(_sCust) && p.IsDelete == false, true, s => s.Mingcheng).ToList<base_shangpinxx>();
                var _splist = ServiceFactory.base_shangpinxxservice.LoadShangpinCust(int.Parse(_sCust)).ToList<base_shangpin_v>();
                if (_splist == null)
                    return Json("");
                else
                    return Json(_splist);
            }
        }
        /// <summary>
        /// 以入库序号查货主经营的商品列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCustomerCargosByEntryID()
        {
            string _rkdid = Request["rkd"] ?? "";
            if (_rkdid.Length == 0)
                return Json("");
            else
            {
                wms_rukudan _rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkdid) && p.IsDelete == false);
                if (_rkd == null)
                    return Json("");
                else
                {
                    //var _splist= ServiceFactory.base_shangpinxxservice.LoadSortEntities(g => g.HuozhuID == _rkd.HuozhuID && g.JingyinSF==true && g.IsDelete == false, true, s => s.Mingcheng).ToList<base_shangpinxx>();
                    var _splist = ServiceFactory.base_shangpinxxservice.LoadShangpinCust((int)_rkd.HuozhuID).Where<base_shangpin_v>(p => p.jingyinsf == true).ToList<base_shangpin_v>().OrderBy(s => s.mingcheng);
                    if (_splist == null)
                        return Json("");
                    else
            return Json(_splist);

                }
            }
        }
        /// <summary>
        /// 货主全部商品列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCustomerCargosAllByEntryID()
        {
            string _rkdid = Request["rkd"] ?? "";
            if (_rkdid.Length == 0)
                return Json("");
            else
            {
                wms_rukudan _rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkdid) && p.IsDelete == false);
                if (_rkd == null)
                    return Json("");
                else
                {
                    //var _splist = ServiceFactory.base_shangpinxxservice.LoadSortEntities(g => g.HuozhuID == _rkd.HuozhuID && g.IsDelete == false, true, s => s.Mingcheng).ToList<base_shangpinxx>();
                    var _splist = ServiceFactory.base_shangpinxxservice.LoadShangpinCust((int)_rkd.HuozhuID).ToList<base_shangpin_v>().OrderBy(s => s.mingcheng);
                    if (_splist == null)
                        return Json("");
                    else
                        return Json(_splist);
                }
            }
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
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //新增
            string shenchasf = Request["shenchasf"] ?? "";
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            try
            {
                base_shangpinxx ob_base_shangpinxx = new base_shangpinxx();
                ob_base_shangpinxx.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_base_shangpinxx.HuozhuSQID = huozhusqid == "" ? 0 : int.Parse(huozhusqid);
                ob_base_shangpinxx.Daima = daima.Trim();
                ob_base_shangpinxx.Mingcheng = mingcheng.Trim();
                ob_base_shangpinxx.ZhucezhengID = zhucezhengid == "" ? 0 : int.Parse(zhucezhengid);
                ob_base_shangpinxx.ZhucezhengBH = zhucezhengbh.Trim();
                ob_base_shangpinxx.Guige = guige.Trim();
                ob_base_shangpinxx.Xinghao = xinghao.Trim();
                ob_base_shangpinxx.Danwei = danwei.Trim();
                ob_base_shangpinxx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_base_shangpinxx.Volchang = volchang == "" ? 0 : float.Parse(volchang);
                ob_base_shangpinxx.Volkuan = volkuan == "" ? 0 : float.Parse(volkuan);
                ob_base_shangpinxx.Volgao = volgao == "" ? 0 : float.Parse(volgao);
                ob_base_shangpinxx.Chanpinxian = chanpinxian == "" ? 0 : int.Parse(chanpinxian);
                ob_base_shangpinxx.Muluxuhao = muluxuhao == "" ? 0 : int.Parse(muluxuhao);
                ob_base_shangpinxx.Guanlifenlei = guanlifenlei == "" ? 0 : int.Parse(guanlifenlei);
                ob_base_shangpinxx.Baozhuangyaoqiu = baozhuangyaoqiu.Trim();
                ob_base_shangpinxx.Cunchutiaojian = cunchutiaojian.Trim();
                ob_base_shangpinxx.QiyeID = qiyeid == "" ? 0 : int.Parse(qiyeid);
                ob_base_shangpinxx.Qiyemingcheng = qiyemingcheng.Trim();
                ob_base_shangpinxx.GongyingID = gongyingid == "" ? 0 : int.Parse(gongyingid);
                ob_base_shangpinxx.GongyingSQID = gongyingsqid == "" ? 0 : int.Parse(gongyingsqid);
                ob_base_shangpinxx.GongyingXSID = gongyingxsid == "" ? 0 : int.Parse(gongyingxsid);
                ob_base_shangpinxx.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_shangpinxx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shangpinxx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                ob_base_shangpinxx.ShenchaSF = shenchasf == "true" ? true : false;
                ob_base_shangpinxx.JingyinSF = jingyinsf == "true" ? true : false;
                ob_base_shangpinxx.BaozhuangDW = baozhuangdw.Trim();
                ob_base_shangpinxx.ShangpinTM = shangpintm.Trim();

                ob_base_shangpinxx = ob_base_shangpinxxservice.AddEntity(ob_base_shangpinxx);
                ViewBag.base_shangpinxx = ob_base_shangpinxx;
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
            base_shangpinxx tempData = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == id && base_shangpinxx.IsDelete == false);
            ViewBag.base_shangpinxx = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shangpinxxViewModel base_shangpinxxviewmodel = new base_shangpinxxViewModel();
                base_shangpinxxviewmodel.ID = tempData.ID;
                base_shangpinxxviewmodel.HuozhuID = tempData.HuozhuID;
                base_shangpinxxviewmodel.HuozhuSQID = tempData.HuozhuSQID;
                base_shangpinxxviewmodel.Daima = tempData.Daima;
                base_shangpinxxviewmodel.Mingcheng = tempData.Mingcheng;
                base_shangpinxxviewmodel.ZhucezhengID = tempData.ZhucezhengID;
                base_shangpinxxviewmodel.ZhucezhengBH = tempData.ZhucezhengBH;
                base_shangpinxxviewmodel.Guige = tempData.Guige;
                base_shangpinxxviewmodel.Xinghao = tempData.Xinghao;
                base_shangpinxxviewmodel.Danwei = tempData.Danwei;
                base_shangpinxxviewmodel.Huansuanlv = tempData.Huansuanlv;
                base_shangpinxxviewmodel.Volchang = tempData.Volchang;
                base_shangpinxxviewmodel.Volkuan = tempData.Volkuan;
                base_shangpinxxviewmodel.Volgao = tempData.Volgao;
                base_shangpinxxviewmodel.Chanpinxian = tempData.Chanpinxian;
                base_shangpinxxviewmodel.Muluxuhao = tempData.Muluxuhao;
                base_shangpinxxviewmodel.Guanlifenlei = tempData.Guanlifenlei;
                base_shangpinxxviewmodel.Baozhuangyaoqiu = tempData.Baozhuangyaoqiu;
                base_shangpinxxviewmodel.Cunchutiaojian = tempData.Cunchutiaojian;
                base_shangpinxxviewmodel.QiyeID = tempData.QiyeID;
                base_shangpinxxviewmodel.Qiyemingcheng = tempData.Qiyemingcheng;
                base_shangpinxxviewmodel.GongyingID = tempData.GongyingID;
                base_shangpinxxviewmodel.GongyingSQID = tempData.GongyingSQID;
                base_shangpinxxviewmodel.GongyingXSID = tempData.GongyingXSID;
                base_shangpinxxviewmodel.Shouying = tempData.Shouying;
                base_shangpinxxviewmodel.MakeDate = tempData.MakeDate;
                base_shangpinxxviewmodel.MakeMan = tempData.MakeMan;
                base_shangpinxxviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_shangpinxxviewmodel.JingyinSF = tempData.JingyinSF;
                base_shangpinxxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                base_shangpinxxviewmodel.ShangpinTM = tempData.ShangpinTM;
                return View(base_shangpinxxviewmodel);
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
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //新增
            string shenchasf = Request["shenchasf"] ?? "";
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shangpinxx p = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == uid);
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
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                p.ShenchaSF = shenchasf == "true" ? true : false;
                p.JingyinSF = jingyinsf == "true" ? true : false;
                p.BaozhuangDW = baozhuangdw.Trim();
                p.ShangpinTM = shangpintm.Trim();

                ob_base_shangpinxxservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_shangpinxx ob_base_shangpinxx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shangpinxx = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == id && base_shangpinxx.IsDelete == false);
                    ob_base_shangpinxx.IsDelete = true;
                    ob_base_shangpinxxservice.UpdateEntity(ob_base_shangpinxx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

