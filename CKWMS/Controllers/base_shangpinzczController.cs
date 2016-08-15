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
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Controllers
{
    public class base_shangpinzczController : Controller
    {
        private Ibase_shangpinzczService ob_base_shangpinzczservice = ServiceFactory.base_shangpinzczservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinzcz_index";
            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "bianhao":
                            string bianhao = scld[1];
                            string bianhaoequal = scld[2];
                            string bianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(bianhao))
                            {
                                if (bianhaoequal.Equals("="))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpinzcz => base_shangpinzcz.IsDelete == false);

            var tempData = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), false, base_shangpinzcz => base_shangpinzcz.ID).ToPagedList<base_shangpinzcz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinzcz = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinzcz_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";

            string shangpingmingcheng = Request["shangpingmingcheng"] ?? "";
            string shangpingmingchengequal = Request["shangpingmingchengequal"] ?? "";
            string shangpingmingchengand = Request["shangpingmingchengand"] ?? "";

            string zhucezhengyxq = Request["zhucezhengyxq"] ?? "";
            string zhucezhengyxqequal = Request["zhucezhengyxqequal"] ?? "";
            string zhucezhengyxqand = Request["zhucezhengyxqand"] ?? "";
            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //biaohao
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                
                //shangpingmingcheng
                if (!string.IsNullOrEmpty(shangpingmingcheng))
                {
                    if (shangpingmingchengequal.Equals("="))
                    {
                        if (shangpingmingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == shangpingmingcheng);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == shangpingmingcheng);
                    }
                    if (shangpingmingchengequal.Equals("like"))
                    {
                        if (shangpingmingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(shangpingmingcheng));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(shangpingmingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(shangpingmingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpingmingcheng", shangpingmingcheng, shangpingmingchengequal, shangpingmingchengand);
                //zhucezhengyxq
                if (!string.IsNullOrEmpty(zhucezhengyxq))
                {
                    if (zhucezhengyxqequal.Equals("="))
                    {
                        if (zhucezhengyxqand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.ZhucezhengYXQ == DateTime.Parse(zhucezhengyxq));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.ZhucezhengYXQ == DateTime.Parse(zhucezhengyxq));
                    }
                    if (zhucezhengyxqequal.Equals("like"))
                    {
                        if (zhucezhengyxqand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.ZhucezhengYXQ == DateTime.Parse(zhucezhengyxq));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.ZhucezhengYXQ == DateTime.Parse(zhucezhengyxq));
                    }
                }
                if (!string.IsNullOrEmpty(zhucezhengyxq))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengyxq", zhucezhengyxq, zhucezhengyxqequal, zhucezhengyxqand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                //shangpingmingcheng
                if (!string.IsNullOrEmpty(shangpingmingcheng))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == shangpingmingcheng);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == shangpingmingcheng);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(shangpingmingcheng));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(shangpingmingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(shangpingmingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpingmingcheng", shangpingmingcheng, shangpingmingchengequal, shangpingmingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shangpinzcz => base_shangpinzcz.IsDelete == false);

            var tempData = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), false, base_shangpinzcz => base_shangpinzcz.ID).ToPagedList<base_shangpinzcz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinzcz = tempData;
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
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string bianzhun = Request["bianzhun"] ?? "";
            string zhucezhengyxq = Request["zhucezhengyxq"] ?? "";
            string zhucezhengtp = Request["zhucezhengtp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //新增
            string shixiaosf = Request["shixiaosf"];
            string bianhaozw = Request["bianhaozw"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shengchanqiyeid = Request["Shengchanqiyeid"] ?? "";
            string shiyongfw = Request["shiyongfw"] ?? "";
            string zhucedl = Request["zhucedl"] ?? "";
            string shouhuojg = Request["shouhuojg"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            try
            {
                base_shangpinzcz ob_base_shangpinzcz = new base_shangpinzcz();
                ob_base_shangpinzcz.Bianhao = bianhao.Trim();
                ob_base_shangpinzcz.Mingcheng = mingcheng.Trim();
                ob_base_shangpinzcz.Guige = guige.Trim();
                ob_base_shangpinzcz.Bianzhun = bianzhun.Trim();
                ob_base_shangpinzcz.ZhucezhengYXQ = zhucezhengyxq == "" ? DateTime.Now : DateTime.Parse(zhucezhengyxq);
                ob_base_shangpinzcz.ZhucezhengTP = zhucezhengtp.Trim();
                ob_base_shangpinzcz.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shangpinzcz.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                ob_base_shangpinzcz.ShixiaoSF = shixiaosf == "true" ? true:false;
                ob_base_shangpinzcz.BianhaoZW = bianhaozw.Trim();
                ob_base_shangpinzcz.Chandi = chandi.Trim();
                ob_base_shangpinzcz.ShengchanqiyeID = shengchanqiyeid == "" ? 0 : int.Parse(shengchanqiyeid);
                ob_base_shangpinzcz.ShiyongFW = shiyongfw.Trim();
                ob_base_shangpinzcz.ZhuceDL = zhucedl.Trim();
                ob_base_shangpinzcz.ShouhuoJG = shouhuojg.Trim();
                ob_base_shangpinzcz.Beizhu = beizhu.Trim();

                ob_base_shangpinzcz = ob_base_shangpinzczservice.AddEntity(ob_base_shangpinzcz);
                ViewBag.base_shangpinzcz = ob_base_shangpinzcz;
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
            base_shangpinzcz tempData = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == id && base_shangpinzcz.IsDelete == false);
            ViewBag.base_shangpinzcz = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shangpinzczViewModel base_shangpinzczviewmodel = new base_shangpinzczViewModel();
                base_shangpinzczviewmodel.ID = tempData.ID;
                base_shangpinzczviewmodel.Bianhao = tempData.Bianhao;
                base_shangpinzczviewmodel.Mingcheng = tempData.Mingcheng;
                base_shangpinzczviewmodel.Guige = tempData.Guige;
                base_shangpinzczviewmodel.Bianzhun = tempData.Bianzhun;
                base_shangpinzczviewmodel.ZhucezhengYXQ = tempData.ZhucezhengYXQ;
                base_shangpinzczviewmodel.ZhucezhengTP = tempData.ZhucezhengTP;
                base_shangpinzczviewmodel.MakeDate = tempData.MakeDate;
                base_shangpinzczviewmodel.MakeMan = tempData.MakeMan;
                //新增
                base_shangpinzczviewmodel.ShixiaoSF = tempData.ShixiaoSF;
                base_shangpinzczviewmodel.BianhaoZW = tempData.BianhaoZW;
                base_shangpinzczviewmodel.Chandi = tempData.Chandi;
                base_shangpinzczviewmodel.ShengchanqiyeID = (int)tempData.ShengchanqiyeID;
                base_shangpinzczviewmodel.ShiyongFW = tempData.ShiyongFW;
                base_shangpinzczviewmodel.ZhuceDL = tempData.ZhuceDL;
                base_shangpinzczviewmodel.ShouhuoJG = tempData.ShouhuoJG;
                base_shangpinzczviewmodel.Beizhu = tempData.Beizhu;
                return View(base_shangpinzczviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string bianzhun = Request["bianzhun"] ?? "";
            string zhucezhengyxq = Request["zhucezhengyxq"] ?? "";
            string zhucezhengtp = Request["zhucezhengtp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //新增
            string shixiaosf = Request["shixiaosf"];
            string bianhaozw = Request["bianhaozw"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shengchanqiyeid = Request["Shengchanqiyeid"] ?? "";
            string shiyongfw = Request["shiyongfw"] ?? "";
            string zhucedl = Request["zhucedl"] ?? "";
            string shouhuojg = Request["shouhuojg"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shangpinzcz p = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Guige = guige.Trim();
                p.Bianzhun = bianzhun.Trim();
                p.ZhucezhengYXQ = zhucezhengyxq == "" ? DateTime.Now : DateTime.Parse(zhucezhengyxq);
                p.ZhucezhengTP = zhucezhengtp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                p.ShixiaoSF = shixiaosf == "true" ? true : false;
                p.BianhaoZW = bianhaozw.Trim();
                p.Chandi = chandi.Trim();
                p.ShengchanqiyeID = shengchanqiyeid == "" ? 0 : int.Parse(shengchanqiyeid);
                p.ShiyongFW = shiyongfw.Trim();
                p.ZhuceDL = zhucedl.Trim();
                p.ShouhuoJG = shouhuojg.Trim();
                p.Beizhu = beizhu.Trim();
                ob_base_shangpinzczservice.UpdateEntity(p);
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
            base_shangpinzcz ob_base_shangpinzcz;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shangpinzcz = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == id && base_shangpinzcz.IsDelete == false);
                    ob_base_shangpinzcz.IsDelete = true;
                    ob_base_shangpinzczservice.UpdateEntity(ob_base_shangpinzcz);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

