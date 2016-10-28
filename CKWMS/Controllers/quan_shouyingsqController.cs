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
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
namespace CKWMS.Controllers
{
    public class quan_shouyingsqController : Controller
    {
        private Iquan_shouyingsqService ob_quan_shouyingsqservice = ServiceFactory.quan_shouyingsqservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_shouyingsq_index";
            Expression<Func<quan_shouyingsq, bool>> where = PredicateExtensionses.True<quan_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "leibie":
                            string leibie = scld[1];
                            string leibieequal = scld[2];
                            string leibieand = scld[3];
                            if (!string.IsNullOrEmpty(leibie))
                            {
                                if (leibieequal.Equals("="))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                                }
                                if (leibieequal.Equals(">"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                                }
                                if (leibieequal.Equals("<"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_shouyingsq => quan_shouyingsq.IsDelete == false && quan_shouyingsq.Zhuangtai > 0 && quan_shouyingsq.Zhuangtai < 5);

            var tempData = ob_quan_shouyingsqservice.LoadSortEntities(where.Compile(), false, quan_shouyingsq => quan_shouyingsq.ID).ToPagedList<quan_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_shouyingsq = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_shouyingsq_index";
            string page = "1";
            string leibie = Request["leibie"] ?? "";
            string leibieequal = Request["leibieequal"] ?? "";
            string leibieand = Request["leibieand"] ?? "";
            Expression<Func<quan_shouyingsq, bool>> where = PredicateExtensionses.True<quan_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", "", leibieequal, leibieand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", "", leibieequal, leibieand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_shouyingsq => quan_shouyingsq.IsDelete == false && quan_shouyingsq.Zhuangtai > 0 && quan_shouyingsq.Zhuangtai < 5);

            var tempData = ob_quan_shouyingsqservice.LoadSortEntities(where.Compile(), false, quan_shouyingsq => quan_shouyingsq.ID).ToPagedList<quan_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_shouyingsq = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult RecIndex(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_shouyingsq_recindex";
            Expression<Func<quan_shouyingsq, bool>> where = PredicateExtensionses.True<quan_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "leibie":
                            string leibie = scld[1];
                            string leibieequal = scld[2];
                            string leibieand = scld[3];
                            if (!string.IsNullOrEmpty(leibie))
                            {
                                if (leibieequal.Equals("="))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                                }
                                if (leibieequal.Equals(">"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                                }
                                if (leibieequal.Equals("<"))
                                {
                                    if (leibieand.Equals("and"))
                                        where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                                    else
                                        where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            where = where.And(quan_shouyingsq => quan_shouyingsq.IsDelete == false);
            var tempData = ob_quan_shouyingsqservice.LoadSortEntities(where.Compile(), false, quan_shouyingsq => quan_shouyingsq.ID).ToPagedList<quan_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_shouyingsq = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult RecIndex()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_shouyingsq_recindex";
            string page = "1";
            string leibie = Request["leibie"] ?? "";
            string leibieequal = Request["leibieequal"] ?? "";
            string leibieand = Request["leibieand"] ?? "";
            Expression<Func<quan_shouyingsq, bool>> where = PredicateExtensionses.True<quan_shouyingsq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", "", leibieequal, leibieand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(leibie))
                {
                    if (leibieequal.Equals("="))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie == int.Parse(leibie));
                    }
                    if (leibieequal.Equals(">"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie > int.Parse(leibie));
                    }
                    if (leibieequal.Equals("<"))
                    {
                        if (leibieand.Equals("and"))
                            where = where.And(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                        else
                            where = where.Or(quan_shouyingsq => quan_shouyingsq.Leibie < int.Parse(leibie));
                    }
                }
                if (!string.IsNullOrEmpty(leibie))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", "", leibieequal, leibieand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_shouyingsq => quan_shouyingsq.IsDelete == false);
            var tempData = ob_quan_shouyingsqservice.LoadSortEntities(where.Compile(), false, quan_shouyingsq => quan_shouyingsq.ID).ToPagedList<quan_shouyingsq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_shouyingsq = tempData;
            return View(tempData);
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        [HttpPost]
        public string CheckAdd()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _checkval = Request["checkval"] ?? "";
            string _checktype = Request["checktype"] ?? "";
            if (_checktype.Length == 0)
                _checktype = "0";
            if (_checkval.Length > 0)
            {
                string[] _checkvals = _checkval.Split(',');
                foreach (var _cv in _checkvals)
                {
                    if (_cv.Length > 0)
                    {
                        quan_shouyingsq _sq = ob_quan_shouyingsqservice.GetEntityById(p => p.Leibie == int.Parse(_checktype) && p.ShenqingID == int.Parse(_cv) && p.Zhuangtai != -1 && p.IsDelete == false);
                        if (_sq == null)
                        {
                            _sq = new quan_shouyingsq();
                            _sq.MakeMan = _userid;
                            _sq.MakeDate = DateTime.Now;
                            _sq.Leibie = int.Parse(_checktype);
                            _sq.Zhuangtai = 1;
                            _sq.ShenqingID = int.Parse(_cv);
                            _sq.SQren = _username;
                            _sq.SQshijian = DateTime.Now;
                            _sq.Neirong = GetShenqingContent(int.Parse(_cv), int.Parse(_checktype));
                            _sq = ob_quan_shouyingsqservice.AddEntity(_sq);
                        }
                    }
                }
            }
            return "ok";// Json("ok");
        }
        /// <summary>
        /// 质量部审核
        /// </summary>
        /// <returns>1 成功，0 不正常操作，-1 报错</returns>
        [HttpPost]
        public int AuthorCheck()
        {
            try
            {
                int _userid = (int)Session["user_id"];
                string _username = (string)Session["user_name"];
                string _authorval = Request["authorval"] ?? "";
                int _sq = 0;
                string _shwords = "";
                bool _ok = false;
                if (_authorval.Length < 1)
                {
                    return 0;
                }
                var _av = JArray.Parse(_authorval);
                //JObject _av = JObject.Parse(_authorval);
                foreach (JObject jsq in _av)
                {
                    if (jsq["sq"].ToString().Length > 0)
                    {
                        _sq = int.Parse(jsq["sq"].ToString());
                        _shwords = jsq["msg"].ToString();
                        _ok = bool.Parse(jsq["ok"].ToString());
                        quan_shouyingsq _shouyingsq = ob_quan_shouyingsqservice.GetEntityById(p => p.ID == _sq && p.IsDelete == false);
                        if (_shouyingsq == null)
                            return -1;
                        else
                        {
                            if (_shouyingsq.Zhuangtai < 3)
                            {
                                _shouyingsq.Shenheshuoming = _shwords;
                                _shouyingsq.Shenheren = _username;
                                if (_ok)
                                    _shouyingsq.Zhuangtai = 3;
                                else
                                    _shouyingsq.Zhuangtai = -1;
                                ob_quan_shouyingsqservice.UpdateEntity(_shouyingsq);
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 质量部审核
        /// </summary>
        /// <returns>1 成功，0 不正常操作，-1 报错</returns>
        [HttpPost]
        public int AuthorTopCheck()
        {
            try
            {
                bool allow = false;
                int _userid = (int)Session["user_id"];
                string _username = (string)Session["user_name"];
                string _authorval = Request["authorval"] ?? "";
                var _allow = Request["allow"] ?? "";
                if (_allow == "1")
                    allow = true;
                int _sq = 0;
                string _shwords = "";
                bool _ok = false;
                if (_authorval.Length < 1)
                {
                    return 0;
                }
                var _av = JArray.Parse(_authorval);
                //JObject _av = JObject.Parse(_authorval);
                foreach (JObject jsq in _av)
                {
                    if (jsq["sq"].ToString().Length > 0)
                    {
                        _sq = int.Parse(jsq["sq"].ToString());
                        _shwords = jsq["msg"].ToString();
                        _ok = bool.Parse(jsq["ok"].ToString());
                        quan_shouyingsq _shouyingsq = ob_quan_shouyingsqservice.GetEntityById(p => p.ID == _sq && p.IsDelete == false);
                        if (_shouyingsq == null)
                            return -1;
                        else
                        {
                            if (_shouyingsq.Zhuangtai < 3)
                            {
                                _shouyingsq.Shenheshuoming = _shwords;
                                _shouyingsq.Shenheren = _username;
                                if (_ok)
                                {
                                    if (allow)
                                        _shouyingsq.Zhuangtai = 5;
                                    else
                                        _shouyingsq.Zhuangtai = 3;
                                }
                                else
                                    _shouyingsq.Zhuangtai = -1;
                                ob_quan_shouyingsqservice.UpdateEntity(_shouyingsq);
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 质量负责人审核
        /// </summary>
        /// <returns>1 成功，-1 出错，0 不正常操作</returns>
        [HttpPost]
        public int TopCheck()
        {
            try
            {
                int _userid = (int)Session["user_id"];
                string _username = (string)Session["user_name"];
                string _topval = Request["topval"] ?? "";
                int _sq = 0;
                string _shwords = "";
                bool _ok = false;
                if (_topval.Length < 1)
                {
                    return 0;
                }
                var _av = JArray.Parse(_topval);
                //JObject _av = JObject.Parse(_topval);
                foreach (JObject jsq in _av)
                {
                    if (jsq["sq"].ToString().Length > 0)
                    {
                        _sq = int.Parse(jsq["sq"].ToString());
                        _shwords = jsq["msg"].ToString();
                        _ok = bool.Parse(jsq["ok"].ToString());
                        quan_shouyingsq _shouyingsq = ob_quan_shouyingsqservice.GetEntityById(p => p.ID == _sq && p.IsDelete == false);
                        if (_shouyingsq == null)
                            return -1;
                        else
                        {
                            if (_shouyingsq.Zhuangtai < 4)
                            {
                                _shouyingsq.FuzerenSM = _shwords;
                                _shouyingsq.Fuzeren = _username;
                                if (_ok)
                                    _shouyingsq.Zhuangtai = 5;
                                else
                                    _shouyingsq.Zhuangtai = -1;
                                ob_quan_shouyingsqservice.UpdateEntity(_shouyingsq);
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 质量负责人审核
        /// </summary>
        /// <returns>1 成功，-1 出错，0 不正常操作</returns>
        [HttpPost]
        public int TopLowCheck()
        {
            try
            {
                bool allow = false;
                int _userid = (int)Session["user_id"];
                string _username = (string)Session["user_name"];
                string _topval = Request["topval"] ?? "";
                var _allow = Request["allow"] ?? "";
                if (_allow == "1")
                    allow = true;
                int _sq = 0;
                string _shwords = "";
                bool _ok = false;
                if (_topval.Length < 1)
                {
                    return 0;
                }
                var _av = JArray.Parse(_topval);
                //JObject _av = JObject.Parse(_topval);
                foreach (JObject jsq in _av)
                {
                    if (jsq["sq"].ToString().Length > 0)
                    {
                        _sq = int.Parse(jsq["sq"].ToString());
                        _shwords = jsq["msg"].ToString();
                        _ok = bool.Parse(jsq["ok"].ToString());
                        quan_shouyingsq _shouyingsq = ob_quan_shouyingsqservice.GetEntityById(p => p.ID == _sq && p.IsDelete == false);
                        if (_shouyingsq == null)
                            return -1;
                        else
                        {
                            if (_shouyingsq.Zhuangtai < 4)
                            {
                                _shouyingsq.FuzerenSM = _shwords;
                                _shouyingsq.Fuzeren = _username;
                                if (_ok)
                                {
                                    if (allow)
                                        _shouyingsq.Zhuangtai = 4;
                                    else
                                        _shouyingsq.Zhuangtai = 5;
                                }
                                else
                                    _shouyingsq.Zhuangtai = -1;
                                ob_quan_shouyingsqservice.UpdateEntity(_shouyingsq);
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        private void AuthorRecord()
        {

        }
        private string GetShenqingContent(int valid, int valtype)
        {
            //ShouYingType.Add(1, "商品");
            //ShouYingType.Add(2, "客户");
            //ShouYingType.Add(3, "供应商");
            //ShouYingType.Add(4, "收货方");
            //ShouYingType.Add(5, "工厂");
            //ShouYingType.Add(6, "销售");
            //ShouYingType.Add(7, "发货方");
            //ShouYingType.Add(8, "运输单位");
            string _sqcontent = "";
            switch (valtype)
            {
                case 1:
                    base_shangpinxx _spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == valid);
                    if (_spxx != null)
                        _sqcontent = string.Format("商品首营申请：名称，{0}；注册证编号：{1}；厂家：{2}；", _spxx.Mingcheng, _spxx.ZhucezhengBH, _spxx.Qiyemingcheng);
                    break;
                case 2:
                    Ibase_weituokehuService _wtservice = ServiceFactory.base_weituokehuservice;
                    base_weituokehu _wtkh = _wtservice.GetEntityById(p => p.ID == valid);
                    if (_wtkh != null)
                        _sqcontent = string.Format("货主首营申请：名称，{0}；营业执照：{1}；经营许可：{2}；", _wtkh.Kehumingcheng, _wtkh.YingyezhizhaoBH, _wtkh.JingyingxukeBH);
                    break;
                case 3:
                    base_gongyingshang _gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == valid);
                    if (_gys != null)
                        _sqcontent = string.Format("供应商首营申请：名称，{0}；营业执照：{1}；经营许可：{2}；", _gys.Mingcheng, _gys.YingyezhizhaoBH, _gys.JingyingxukeBH);
                    break;
                case 4:
                    base_shouhuodanwei _shdw = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == valid);
                    if (_shdw != null)
                        _sqcontent = string.Format("货主首营申请：名称，{0}；营业执照：{1}；经营许可：{2}；", _shdw.Mingcheng, _shdw.YingyezhizhaoBH, _shdw.JingyingxukeBH);
                    break;
                case 5:
                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.ID == valid);
                    if (_scqy != null)
                        _sqcontent = string.Format("货主首营申请：名称，{0}；营业执照：{1}；经营许可：{2}；", _scqy.Qiyemingcheng, _scqy.YingyezhizhaoBH, _scqy.ShengchanxukeBH);
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                default:
                    break;
            }
            return _sqcontent;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string neirong = Request["neirong"] ?? "";
            string sqshijian = Request["sqshijian"] ?? "";
            string sqren = Request["sqren"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string shenheren = Request["shenheren"] ?? "";
            string shenheshuoming = Request["shenheshuoming"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenqingid = Request["shenqingid"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string fuzerensm = Request["fuzerensm"] ?? "";
            try
            {
                quan_shouyingsq ob_quan_shouyingsq = new quan_shouyingsq();
                ob_quan_shouyingsq.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                ob_quan_shouyingsq.Neirong = neirong.Trim();
                ob_quan_shouyingsq.SQshijian = sqshijian == "" ? DateTime.Now : DateTime.Parse(sqshijian);
                ob_quan_shouyingsq.SQren = sqren.Trim();
                ob_quan_shouyingsq.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_quan_shouyingsq.Shenheren = shenheren.Trim();
                ob_quan_shouyingsq.Shenheshuoming = shenheshuoming.Trim();
                ob_quan_shouyingsq.Col1 = col1.Trim();
                ob_quan_shouyingsq.Col2 = col2.Trim();
                ob_quan_shouyingsq.Col3 = col3.Trim();
                ob_quan_shouyingsq.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_shouyingsq.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_shouyingsq.ShenqingID = shenqingid == "" ? 0 : int.Parse(shenqingid);
                ob_quan_shouyingsq.Fuzeren = fuzeren.Trim();
                ob_quan_shouyingsq.FuzerenSM = fuzerensm.Trim();
                ob_quan_shouyingsq = ob_quan_shouyingsqservice.AddEntity(ob_quan_shouyingsq);
                ViewBag.quan_shouyingsq = ob_quan_shouyingsq;
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
            quan_shouyingsq tempData = ob_quan_shouyingsqservice.GetEntityById(quan_shouyingsq => quan_shouyingsq.ID == id && quan_shouyingsq.IsDelete == false);
            ViewBag.quan_shouyingsq = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_shouyingsqViewModel quan_shouyingsqviewmodel = new quan_shouyingsqViewModel();
                quan_shouyingsqviewmodel.ID = tempData.ID;
                quan_shouyingsqviewmodel.Leibie = tempData.Leibie;
                quan_shouyingsqviewmodel.Neirong = tempData.Neirong;
                quan_shouyingsqviewmodel.SQshijian = tempData.SQshijian;
                quan_shouyingsqviewmodel.SQren = tempData.SQren;
                quan_shouyingsqviewmodel.Zhuangtai = tempData.Zhuangtai;
                quan_shouyingsqviewmodel.Shenheren = tempData.Shenheren;
                quan_shouyingsqviewmodel.Shenheshuoming = tempData.Shenheshuoming;
                quan_shouyingsqviewmodel.Col1 = tempData.Col1;
                quan_shouyingsqviewmodel.Col2 = tempData.Col2;
                quan_shouyingsqviewmodel.Col3 = tempData.Col3;
                quan_shouyingsqviewmodel.MakeDate = tempData.MakeDate;
                quan_shouyingsqviewmodel.MakeMan = tempData.MakeMan;
                quan_shouyingsqviewmodel.ShenqingID = tempData.ShenqingID;
                quan_shouyingsqviewmodel.Fuzeren = tempData.Fuzeren;
                quan_shouyingsqviewmodel.FuzerenSM = tempData.FuzerenSM;
                return View(quan_shouyingsqviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string neirong = Request["neirong"] ?? "";
            string sqshijian = Request["sqshijian"] ?? "";
            string sqren = Request["sqren"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string shenheren = Request["shenheren"] ?? "";
            string shenheshuoming = Request["shenheshuoming"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenqingid = Request["shenqingid"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string fuzerensm = Request["fuzerensm"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_shouyingsq p = ob_quan_shouyingsqservice.GetEntityById(quan_shouyingsq => quan_shouyingsq.ID == uid);
                p.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                p.Neirong = neirong.Trim();
                p.SQshijian = sqshijian == "" ? DateTime.Now : DateTime.Parse(sqshijian);
                p.SQren = sqren.Trim();
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.Shenheren = shenheren.Trim();
                p.Shenheshuoming = shenheshuoming.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShenqingID = shenqingid == "" ? 0 : int.Parse(shenqingid);
                p.Fuzeren = fuzeren.Trim();
                p.FuzerenSM = fuzerensm.Trim();
                ob_quan_shouyingsqservice.UpdateEntity(p);
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
            quan_shouyingsq ob_quan_shouyingsq;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_shouyingsq = ob_quan_shouyingsqservice.GetEntityById(quan_shouyingsq => quan_shouyingsq.ID == id && quan_shouyingsq.IsDelete == false);
                    ob_quan_shouyingsq.IsDelete = true;
                    ob_quan_shouyingsqservice.UpdateEntity(ob_quan_shouyingsq);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

