using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;

namespace CKWMS.Controllers
{
    public class base_shouhuodanweiController : Controller
    {
        private Ibase_shouhuodanweiService ob_base_shouhuodanweiservice = ServiceFactory.base_shouhuodanweiservice;
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";

            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";

            Expression<Func<base_shouhuodanwei, bool>> where = PredicateExtensionses.True<base_shouhuodanwei>();
            if (!string.IsNullOrEmpty(huozhuid))
            {
                if (huozhuidequal.Equals("="))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                    else
                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                }
                if (huozhuidequal.Equals(">"))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID > int.Parse(huozhuid));
                    else
                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID > int.Parse(huozhuid));
                }
                if (huozhuidequal.Equals("<"))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID < int.Parse(huozhuid));
                    else
                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID < int.Parse(huozhuid));
                }
            }

            where = where.And(base_shouhuodanwei => base_shouhuodanwei.IsDelete == false);

            var tempData = ob_base_shouhuodanweiservice.LoadSortEntities(where.Compile(), false, base_shouhuodanwei => base_shouhuodanwei.ID).ToPagedList<base_shouhuodanwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouhuodanwei = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_shouhuodanwei_id"] ?? "";
            string huozhuid = Request["ob_base_shouhuodanwei_huozhuid"] ?? "";
            string mingcheng = Request["ob_base_shouhuodanwei_mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_shouhuodanwei_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_shouhuodanwei_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_shouhuodanwei_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_shouhuodanwei_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_shouhuodanwei_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_shouhuodanwei_jingyingxuketp"] ?? "";
            string shouying = Request["ob_base_shouhuodanwei_shouying"] ?? "";
            string makedate = Request["ob_base_shouhuodanwei_makedate"] ?? "";
            string makeman = Request["ob_base_shouhuodanwei_makeman"] ?? "";
            try
            {
                base_shouhuodanwei ob_base_shouhuodanwei = new base_shouhuodanwei();
                ob_base_shouhuodanwei.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_base_shouhuodanwei.Mingcheng = mingcheng.Trim();
                ob_base_shouhuodanwei.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_shouhuodanwei.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_shouhuodanwei.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_shouhuodanwei.JingyingxukeBH = jingyingxukebh.Trim();
                ob_base_shouhuodanwei.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_base_shouhuodanwei.JingyingxukeTP = jingyingxuketp.Trim();
                ob_base_shouhuodanwei.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_shouhuodanwei.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shouhuodanwei.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouhuodanwei = ob_base_shouhuodanweiservice.AddEntity(ob_base_shouhuodanwei);
                ViewBag.base_shouhuodanwei = ob_base_shouhuodanwei;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_shouhuodanwei tempData = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == id && base_shouhuodanwei.IsDelete == false);
            ViewBag.base_shouhuodanwei = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_shouhuodanwei_id"] ?? "";
            string huozhuid = Request["ob_base_shouhuodanwei_huozhuid"] ?? "";
            string mingcheng = Request["ob_base_shouhuodanwei_mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_shouhuodanwei_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_shouhuodanwei_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_shouhuodanwei_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_shouhuodanwei_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_shouhuodanwei_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_shouhuodanwei_jingyingxuketp"] ?? "";
            string shouying = Request["ob_base_shouhuodanwei_shouying"] ?? "";
            string makedate = Request["ob_base_shouhuodanwei_makedate"] ?? "";
            string makeman = Request["ob_base_shouhuodanwei_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shouhuodanwei p = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.Mingcheng = mingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouhuodanweiservice.UpdateEntity(p);
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
            base_shouhuodanwei ob_base_shouhuodanwei;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shouhuodanwei = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == id && base_shouhuodanwei.IsDelete == false);
                    ob_base_shouhuodanwei.IsDelete = true;
                    ob_base_shouhuodanweiservice.UpdateEntity(ob_base_shouhuodanwei);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

