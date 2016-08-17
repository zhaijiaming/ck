using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace CKWMS.Controllers
{
    public class base_gongyingshangController : Controller
    {
        private Ibase_gongyingshangService ob_base_gongyingshangservice = ServiceFactory.base_gongyingshangservice;



        //public class GongyingshangContext : DbContext
        //{
        //    public GongyingshangContext() : base("GongyingshangContext")
        //    {
        //    }

        //    public DbSet<base_gongyingshang> Gongyingshangs { get; set; }
            

        //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    }
        //}

        //private GongyingshangContext db = new GongyingshangContext();

        public ActionResult Index(string sortOrder,string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //string mingcheng = Request["mingcheng"] ?? "";
            //string mingchengequal = Request["mingchengequal"] ?? "";
            //string mingchengand = Request["mingchengand"] ?? "";
            //string shouying = Request["shouying"] ?? "";
            //string shouyingequal = Request["shouyingequal"] ?? "";
            //string shouyingand = Request["shouyingand"] ?? "";

            Expression<Func<base_gongyingshang, bool>> where = PredicateExtensionses.True<base_gongyingshang>();
            if (!string.IsNullOrEmpty(daima))
            {
                if (daimaequal.Equals("="))
                {
                    if (daimaand.Equals("and"))
                        where = where.And(base_gongyingshang => base_gongyingshang.Daima == daima);
                    else
                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima == daima);
                }
                if (daimaequal.Equals("like"))
                {
                    if (daimaand.Equals("and"))
                        where = where.And(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                    else
                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                }
            }

            where = where.And(base_gongyingshang => base_gongyingshang.IsDelete == false);

            var tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.ID).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshang = tempData;
            return View(tempData);







            //ViewBag.DaimaSortParm = String.IsNullOrEmpty(sortOrder) ? "daima_desc" : "";

            //var gongyingshangs = from s in tempData
            //                     select s;
            //switch (sortOrder)
            //{
            //    case "daima_desc":
            //        gongyingshangs = gongyingshangs.OrderByDescending(s => s.Daima);
            //        break;

            //    default:
            //        gongyingshangs = gongyingshangs.OrderBy(s => s.Daima);
            //        break;
            //}
            //return View(gongyingshangs.ToList());
            




        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_gongyingshang_id"] ?? "";
            string daima = Request["ob_base_gongyingshang_daima"] ?? "";
            string mingcheng = Request["ob_base_gongyingshang_mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_gongyingshang_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_gongyingshang_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_gongyingshang_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_gongyingshang_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_gongyingshang_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_gongyingshang_jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["ob_base_gongyingshang_jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["ob_base_gongyingshang_jingyingfanweidm"] ?? "";
            string shouying = Request["ob_base_gongyingshang_shouying"] ?? "";
            string makedate = Request["ob_base_gongyingshang_makedate"] ?? "";
            string makeman = Request["ob_base_gongyingshang_makeman"] ?? "";
            try
            {
                base_gongyingshang ob_base_gongyingshang = new base_gongyingshang();
                ob_base_gongyingshang.Daima = daima.Trim();
                ob_base_gongyingshang.Mingcheng = mingcheng.Trim();
                ob_base_gongyingshang.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_gongyingshang.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_gongyingshang.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_gongyingshang.JingyingxukeBH = jingyingxukebh.Trim();
                ob_base_gongyingshang.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_base_gongyingshang.JingyingxukeTP = jingyingxuketp.Trim();
                ob_base_gongyingshang.Jingyingfanwei = jingyingfanwei.Trim();
                ob_base_gongyingshang.JingyingfanweiDM = jingyingfanweidm.Trim();
                ob_base_gongyingshang.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_gongyingshang.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_gongyingshang.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshang = ob_base_gongyingshangservice.AddEntity(ob_base_gongyingshang);
                ViewBag.base_gongyingshang = ob_base_gongyingshang;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_gongyingshang tempData = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == id && base_gongyingshang.IsDelete == false);
            ViewBag.base_gongyingshang = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_gongyingshang_id"] ?? "";
            string daima = Request["ob_base_gongyingshang_daima"] ?? "";
            string mingcheng = Request["ob_base_gongyingshang_mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_gongyingshang_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_gongyingshang_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_gongyingshang_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_gongyingshang_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_gongyingshang_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_gongyingshang_jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["ob_base_gongyingshang_jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["ob_base_gongyingshang_jingyingfanweidm"] ?? "";
            string shouying = Request["ob_base_gongyingshang_shouying"] ?? "";
            string makedate = Request["ob_base_gongyingshang_makedate"] ?? "";
            string makeman = Request["ob_base_gongyingshang_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_gongyingshang p = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == uid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Jingyingfanwei = jingyingfanwei.Trim();
                p.JingyingfanweiDM = jingyingfanweidm.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshangservice.UpdateEntity(p);
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
            base_gongyingshang ob_base_gongyingshang;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_gongyingshang = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == id && base_gongyingshang.IsDelete == false);
                    ob_base_gongyingshang.IsDelete = true;
                    ob_base_gongyingshangservice.UpdateEntity(ob_base_gongyingshang);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

