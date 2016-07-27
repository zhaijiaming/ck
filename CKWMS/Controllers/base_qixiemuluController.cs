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
    public class base_qixiemuluController : Controller
    {
        private Ibase_qixiemuluService ob_base_qixiemuluservice = ServiceFactory.base_qixiemuluservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_qixiemulu_index";
            Expression<Func<base_qixiemulu, bool>> where = PredicateExtensionses.True<base_qixiemulu>();
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
                                        where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_qixiemulu => base_qixiemulu.IsDelete == false);

            var tempData = ob_base_qixiemuluservice.LoadSortEntities(where.Compile(), false, base_qixiemulu => base_qixiemulu.ID).ToPagedList<base_qixiemulu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_qixiemulu = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_qixiemulu_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            Expression<Func<base_qixiemulu, bool>> where = PredicateExtensionses.True<base_qixiemulu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
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
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_qixiemulu => base_qixiemulu.IsDelete == false);

            var tempData = ob_base_qixiemuluservice.LoadSortEntities(where.Compile(), false, base_qixiemulu => base_qixiemulu.ID).ToPagedList<base_qixiemulu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_qixiemulu = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_qixiemulu ob_base_qixiemulu = new base_qixiemulu();
                ob_base_qixiemulu.Bianhao = bianhao.Trim();
                ob_base_qixiemulu.Mingcheng = mingcheng.Trim();
                ob_base_qixiemulu.Miaoshu = miaoshu.Trim();
                ob_base_qixiemulu.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_qixiemulu.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_qixiemulu = ob_base_qixiemuluservice.AddEntity(ob_base_qixiemulu);
                ViewBag.base_qixiemulu = ob_base_qixiemulu;
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
            base_qixiemulu tempData = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == id && base_qixiemulu.IsDelete == false);
            ViewBag.base_qixiemulu = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_qixiemuluViewModel base_qixiemuluviewmodel = new base_qixiemuluViewModel();
                base_qixiemuluviewmodel.ID = tempData.ID;
                base_qixiemuluviewmodel.Bianhao = tempData.Bianhao;
                base_qixiemuluviewmodel.Mingcheng = tempData.Mingcheng;
                base_qixiemuluviewmodel.Miaoshu = tempData.Miaoshu;
                base_qixiemuluviewmodel.MakeDate = tempData.MakeDate;
                base_qixiemuluviewmodel.MakeMan = tempData.MakeMan;
                return View(base_qixiemuluviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_qixiemulu p = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_qixiemuluservice.UpdateEntity(p);
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
            base_qixiemulu ob_base_qixiemulu;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_qixiemulu = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == id && base_qixiemulu.IsDelete == false);
                    ob_base_qixiemulu.IsDelete = true;
                    ob_base_qixiemuluservice.UpdateEntity(ob_base_qixiemulu);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

