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
    public class base_huozhushouquanController : Controller
    {
        private Ibase_huozhushouquanService ob_base_huozhushouquanservice = ServiceFactory.base_huozhushouquanservice;
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";

            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";

            Expression<Func<base_huozhushouquan, bool>> where = PredicateExtensionses.True<base_huozhushouquan>();
            if (!string.IsNullOrEmpty(huozhuid))
            {
                if (huozhuidequal.Equals("="))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                    else
                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                }
                if (huozhuidequal.Equals(">"))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                    else
                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                }
                if (huozhuidequal.Equals("<"))
                {
                    if (huozhuidand.Equals("and"))
                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                    else
                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                }
            }

            where = where.And(base_huozhushouquan => base_huozhushouquan.IsDelete == false);

            var tempData = ob_base_huozhushouquanservice.LoadSortEntities(where.Compile(), false, base_huozhushouquan => base_huozhushouquan.ID).ToPagedList<base_huozhushouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huozhushouquan = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_huozhushouquan_id"] ?? "";
            string huozhuid = Request["ob_base_huozhushouquan_huozhuid"] ?? "";
            string leibie = Request["ob_base_huozhushouquan_leibie"] ?? "";
            string shouquanid = Request["ob_base_huozhushouquan_shouquanid"] ?? "";
            string shouquanmingcheng = Request["ob_base_huozhushouquan_shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["ob_base_huozhushouquan_shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["ob_base_huozhushouquan_shouquanshutp"] ?? "";
            string makedate = Request["ob_base_huozhushouquan_makedate"] ?? "";
            string makeman = Request["ob_base_huozhushouquan_makeman"] ?? "";
            try
            {
                base_huozhushouquan ob_base_huozhushouquan = new base_huozhushouquan();
                ob_base_huozhushouquan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_base_huozhushouquan.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                ob_base_huozhushouquan.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                ob_base_huozhushouquan.Shouquanmingcheng = shouquanmingcheng.Trim();
                ob_base_huozhushouquan.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                ob_base_huozhushouquan.ShouquanshuTP = shouquanshutp.Trim();
                ob_base_huozhushouquan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_huozhushouquan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huozhushouquan = ob_base_huozhushouquanservice.AddEntity(ob_base_huozhushouquan);
                ViewBag.base_huozhushouquan = ob_base_huozhushouquan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_huozhushouquan tempData = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == id && base_huozhushouquan.IsDelete == false);
            ViewBag.base_huozhushouquan = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_huozhushouquan_id"] ?? "";
            string huozhuid = Request["ob_base_huozhushouquan_huozhuid"] ?? "";
            string leibie = Request["ob_base_huozhushouquan_leibie"] ?? "";
            string shouquanid = Request["ob_base_huozhushouquan_shouquanid"] ?? "";
            string shouquanmingcheng = Request["ob_base_huozhushouquan_shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["ob_base_huozhushouquan_shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["ob_base_huozhushouquan_shouquanshutp"] ?? "";
            string makedate = Request["ob_base_huozhushouquan_makedate"] ?? "";
            string makeman = Request["ob_base_huozhushouquan_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_huozhushouquan p = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                p.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                p.Shouquanmingcheng = shouquanmingcheng.Trim();
                p.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                p.ShouquanshuTP = shouquanshutp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huozhushouquanservice.UpdateEntity(p);
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
            base_huozhushouquan ob_base_huozhushouquan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_huozhushouquan = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == id && base_huozhushouquan.IsDelete == false);
                    ob_base_huozhushouquan.IsDelete = true;
                    ob_base_huozhushouquanservice.UpdateEntity(ob_base_huozhushouquan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

