﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.Models;
using CKWMS.EFModels;
using CKWMS.BSL;
namespace CKWMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int _userid = (int)Session["user_id"];

            List<rmd_remindlistViewModel> _reminds = new List<rmd_remindlistViewModel>();
            var _myremind = ServiceFactory.rmd_myreminderservice.LoadEntities(p =>p.YonghuID == _userid &&p.YemianXS==true &&  p.IsDelete == false).ToList<rmd_myreminder>();
            if (_myremind.Count > 0)
            {
                foreach (var rmd in _myremind)
                {
                    rmd_remindlistViewModel _rmd = GetReminder((int)rmd.TixingID, (int)rmd.TixingZQ);
                    if (_rmd != null)
                        _reminds.Add(_rmd);
                }
            }

            ViewBag.RemindList = _reminds;
            return View();
            //return Content(html, "text/xml");
        }
        protected rmd_remindlistViewModel GetReminder(int remindobject, int remindperiod)
        {
            rmd_remindlistViewModel _rmd = new rmd_remindlistViewModel();
            switch (remindobject)
            {
                case 1:
                    var _Inv0 = ServiceFactory.wms_cunhuoservice.GetStorageList(p => p.ShixiaoRQ <= DateTime.Now.AddDays(remindperiod)).ToList<wms_storage_v>();
                    var _InvSum = (from s in _Inv0
                                   select s.sshuliang)
                                  .Sum();
                    if (remindperiod == -1)
                        _rmd.Info = "库内货品已经超期共有：" + _InvSum.ToString() + "件！";
                    else
                        _rmd.Info = "库内货品近效期 " + remindperiod.ToString() + "天，共有：" + _InvSum.ToString() + "件！";
                    _rmd.Ref = "/wms_cunhuo/remindoverdue?period=" + remindperiod.ToString();
                    break;
                case 2:
                    var _Zcz = ServiceFactory.base_shangpinzczservice.LoadEntities(p => p.IsDelete == false && p.ShixiaoSF == false && p.ZhucezhengYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_shangpinzcz>();
                    var _ZczCount = (from s in _Zcz
                                     select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "已经超期的注册证有 " + _ZczCount.ToString() + "件";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的注册证有 " + _ZczCount.ToString() + "件";
                    _rmd.Ref = "/base_shangpinzcz/overdue?period=" + remindperiod.ToString();
                    break;
                case 3:
                    var _HZAuthor = ServiceFactory.base_huozhushouquanservice.LoadEntities(p => p.IsDelete == false && p.ShouquanshuYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_huozhushouquan>();
                    var _HZACount = (from s in _HZAuthor select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "已经超期的授权有 " + _HZACount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的授权,有 " + _HZACount.ToString() + "条";
                    _rmd.Ref = "/base_huozhushouquan/overdue?period=" + remindperiod.ToString();
                    break;
                case 4:
                    var _WTjy = ServiceFactory.base_weituokehuservice.LoadEntities(p => p.HezuoSF == true && p.IsDelete == false && p.YingyezhizhaoYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_weituokehu>();
                    var _wtjyCount = (from s in _WTjy select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "货主营业执照已经超期的有 " + _wtjyCount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的营业执照,有 " + _wtjyCount.ToString() + "条";
                    _rmd.Ref = "/base_weituokehu/yyzzoverdue?period=" + remindperiod.ToString();
                    break;
                case 5:
                    var _WTxk = ServiceFactory.base_weituokehuservice.LoadEntities(p => p.HezuoSF == true && p.IsDelete == false && p.JingyingxukeYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_weituokehu>();
                    var _wtxkCount = (from s in _WTxk select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "货主经营许可已经超期的有 " + _wtxkCount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的经营许可,有 " + _wtxkCount.ToString() + "条";
                    _rmd.Ref = "/base_weituokehu/jyxkoverdue?period=" + remindperiod.ToString();
                    break;
                case 12:
                    var _rkd = ServiceFactory.wms_rukudanservice.LoadEntities(p => p.IsDelete == false && p.RukuZT < 5).ToList();
                    var _rkdCount = (from s in _rkd select s.ID).Count();
                    _rmd.Info = "系统中现有 " + _rkdCount.ToString() + "笔入库作业正在执行！";
                    _rmd.Ref = "/wms_rukudan/operatelist";
                    break;
                case 13:
                    var _ckd = ServiceFactory.wms_chukudanservice.LoadEntities(p => p.IsDelete == false && p.JihuaZT < 5).ToList();
                    var _ckdCount = (from s in _ckd select s.ID).Count();
                    _rmd.Info = "系统中现有 " + _ckdCount.ToString() + "笔出库作业正在执行！";
                    _rmd.Ref = "/wms_chukudan/outoperate";
                    break;
                case 0:
                default:
                    _rmd = null;
                    break;
            }
            return _rmd;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}