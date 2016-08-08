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
    public class base_gongyingshouquanController : Controller
    {
        private Ibase_gongyingshouquanService ob_base_gongyingshouquanservice = ServiceFactory.base_gongyingshouquanservice;
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";

            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string gongyingshangidequal = Request["gongyingshangidequal"] ?? "";
            string gongyingshangidand = Request["gongyingshangidand"] ?? "";

            Expression<Func<base_gongyingshouquan, bool>> where = PredicateExtensionses.True<base_gongyingshouquan>();
            if (!string.IsNullOrEmpty(gongyingshangid))
            {
                if (gongyingshangidequal.Equals("="))
                {
                    if (gongyingshangidand.Equals("and"))
                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                    else
                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                }
                if (gongyingshangidequal.Equals(">"))
                {
                    if (gongyingshangidand.Equals("and"))
                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                    else
                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                }
                if (gongyingshangidequal.Equals("<"))
                {
                    if (gongyingshangidand.Equals("and"))
                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                    else
                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                }
            }

            where = where.And(base_gongyingshouquan => base_gongyingshouquan.IsDelete == false);

            var tempData = ob_base_gongyingshouquanservice.LoadSortEntities(where.Compile(), false, base_gongyingshouquan => base_gongyingshouquan.ID).ToPagedList<base_gongyingshouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshouquan = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_gongyingshouquan_id"] ?? "";
            string gongyingshangid = Request["ob_base_gongyingshouquan_gongyingshangid"] ?? "";
            string shouquanid = Request["ob_base_gongyingshouquan_shouquanid"] ?? "";
            string shouquanmingcheng = Request["ob_base_gongyingshouquan_shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["ob_base_gongyingshouquan_shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["ob_base_gongyingshouquan_shouquanshutp"] ?? "";
            string makedate = Request["ob_base_gongyingshouquan_makedate"] ?? "";
            string makeman = Request["ob_base_gongyingshouquan_makeman"] ?? "";
            try
            {
                base_gongyingshouquan ob_base_gongyingshouquan = new base_gongyingshouquan();
                ob_base_gongyingshouquan.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                ob_base_gongyingshouquan.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                ob_base_gongyingshouquan.Shouquanmingcheng = shouquanmingcheng.Trim();
                ob_base_gongyingshouquan.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                ob_base_gongyingshouquan.ShouquanshuTP = shouquanshutp.Trim();
                ob_base_gongyingshouquan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_gongyingshouquan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshouquan = ob_base_gongyingshouquanservice.AddEntity(ob_base_gongyingshouquan);
                ViewBag.base_gongyingshouquan = ob_base_gongyingshouquan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_gongyingshouquan tempData = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == id && base_gongyingshouquan.IsDelete == false);
            ViewBag.base_gongyingshouquan = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_gongyingshouquan_id"] ?? "";
            string gongyingshangid = Request["ob_base_gongyingshouquan_gongyingshangid"] ?? "";
            string shouquanid = Request["ob_base_gongyingshouquan_shouquanid"] ?? "";
            string shouquanmingcheng = Request["ob_base_gongyingshouquan_shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["ob_base_gongyingshouquan_shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["ob_base_gongyingshouquan_shouquanshutp"] ?? "";
            string makedate = Request["ob_base_gongyingshouquan_makedate"] ?? "";
            string makeman = Request["ob_base_gongyingshouquan_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_gongyingshouquan p = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == uid);
                p.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                p.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                p.Shouquanmingcheng = shouquanmingcheng.Trim();
                p.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                p.ShouquanshuTP = shouquanshutp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshouquanservice.UpdateEntity(p);
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
            base_gongyingshouquan ob_base_gongyingshouquan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_gongyingshouquan = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == id && base_gongyingshouquan.IsDelete == false);
                    ob_base_gongyingshouquan.IsDelete = true;
                    ob_base_gongyingshouquanservice.UpdateEntity(ob_base_gongyingshouquan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

