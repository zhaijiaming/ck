﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.Models;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.EFModels;
using System.Text;
using Newtonsoft.Json;
namespace CKWMS.App_Code
{
    public static class HtmlHelperExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #region 下拉列表
        /// <summary>
        /// 用户信息下拉列表框,选中指定的项
        /// </summary>
        /// <param name="html"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectList_userinfo(this HtmlHelper html, long selectedValue)
        {
            IuserinfoService _userinfo = ServiceFactory.userinfoservice;
            StringBuilder sb = new StringBuilder();
            //sb.Append("<select name'userinfo' id='userinfo' class='width-80 ' data-placeholder='Choose a person...' >");
            sb.Append("<select name=\"userinfo\" id=\"userinfo\" class=\"width-100\" data-placeholder=\"Choose a person...\" >");
            foreach (var i in _userinfo.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.Account))
            {
                if (i.ID == selectedValue && selectedValue != 0)
                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Account);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Account);
            }
            sb.Append("</select>");
            return MvcHtmlString.Create(sb.ToString());
        }
        /// <summary>
        /// 用户信息下拉列表框
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectList_userinfo(this HtmlHelper html)
        {
            return SelectList_userinfo(html, 0);
        }
        /// <summary>
        /// 客户选择项
        /// </summary>
        /// <param name="selectedvalue">选定值</param>
        /// <param name="classname">类型名称</param>
        /// <param name="itemname">选择项目</param>
        /// <param name="showname">显示信息</param>
        /// <returns></returns>
        public static string SelectItem_Auto(string showname, string classname, string itemname, long selectedvalue)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<select name=\"{0}\" id=\"{1}\" class=\"width-100\" >", showname, showname);
            if (selectedvalue == 0)
                sb.AppendFormat("<option value=\"\" selected=\"selected\"></option>");
            else
                sb.AppendFormat("<option value=\"\"></option>");
            switch (classname)
            {
                case "货主": //"base_weituokehu":
                    Ibase_weituokehuService wtkh = ServiceFactory.base_weituokehuservice;
                    var tmpwtkh = wtkh.LoadSortEntities(base_weituokehu => base_weituokehu.IsDelete == false, true, base_weituokehu => base_weituokehu.Daima);
                    foreach (var i in tmpwtkh)
                    {
                        switch (itemname)
                        {
                            case "代码":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Daima);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Daima);
                                break;
                            case "名称":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Kehumingcheng);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Kehumingcheng);
                                break;
                            case "简称":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Jiancheng);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Jiancheng);
                                break;
                            case "都有":
                            default:
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}_{2}</option>", i.ID, i.Daima, i.Kehumingcheng);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}_{2}</option>", i.ID, i.Daima, i.Kehumingcheng);
                                break;
                        }
                    }
                    break;
                case "医疗器械目录":
                    var _tmpqxml = ServiceFactory.base_qixiemuluservice.LoadSortEntities(p => p.IsDelete == false, true, s => s.Mingcheng);
                    foreach(var i in _tmpqxml)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Mingcheng);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Mingcheng);
                    }
                    break;
                case "注册证":
                    var tmpzcz = ServiceFactory.base_shangpinzczservice.LoadSortEntities(p => p.IsDelete == false && p.ShixiaoSF == false, true, s => s.Bianhao);
                    foreach (var i in tmpzcz)
                    {
                        switch (itemname)
                        {
                            case "编号":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Bianhao);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Bianhao);
                                break;
                            case "名称":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Mingcheng);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Mingcheng);
                                break;
                            case "都有":
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1_2}</option>", i.ID, i.Bianhao, i.Mingcheng);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1_2}</option>", i.ID, i.Bianhao, i.Mingcheng);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "收货单位":
                    var tmpshdw = ServiceFactory.base_shouhuodanweiservice.LoadSortEntities(p => p.IsDelete == false && p.HezuoSF == true, true, s => s.Mingcheng);
                    foreach (var i in tmpshdw)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Mingcheng);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Mingcheng);
                    }
                    break;
                case "供应商": //"base_gongyingshang":
                    Ibase_gongyingshangService gys = ServiceFactory.base_gongyingshangservice;
                    var tmpgys = gys.LoadSortEntities(base_gongyingshang => base_gongyingshang.IsDelete == false, true, base_gongyingshang => base_gongyingshang.Daima);
                    foreach (var i in tmpgys)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}_{2}</option>", i.ID, i.Daima, i.Mingcheng);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}_{2}</option>", i.ID, i.Daima, i.Mingcheng);
                    }
                    break;
                case "厂家": //"base_shengchanqiye":
                    Ibase_shengchanqiyeService scqy = ServiceFactory.base_shengchanqiyeservice;
                    var tmpscqy = scqy.LoadSortEntities(base_shengchanqiye => base_shengchanqiye.IsDelete == false, true, base_shengchanqiye => base_shengchanqiye.Daima);
                    foreach (var i in tmpscqy)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}_{2}</option>", i.ID, i.Daima, i.Qiyemingcheng);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}_{2}</option>", i.ID, i.Daima, i.Qiyemingcheng);
                    }
                    break;
                case "角色"://auth_juese
                    Iauth_jueseService jueseservice = ServiceFactory.auth_jueseservice;
                    var tmpjs = jueseservice.LoadSortEntities(auth_juese => auth_juese.IsDelete == false, true, auth_juese => auth_juese.RoleName);
                    foreach (var i in tmpjs)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.RoleName);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.RoleName);
                    }
                    break;
                case "功能":
                    Iauth_gongnengService gongnengservice = ServiceFactory.auth_gongnengservice;
                    var tmpgn = gongnengservice.LoadSortEntities(auth_gongneng => auth_gongneng.IsDelete == false, true, auth_gongneng => auth_gongneng.Name);
                    foreach (var i in tmpgn)
                    {
                        if (i.ID == selectedvalue && selectedvalue != 0)
                            sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}_{2}</option>", i.ID, i.Name, i.Module);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}_{2}</option>", i.ID, i.Name, i.Module);
                    }
                    break;
                case "userinfo"://用户
                    IuserinfoService uis = ServiceFactory.userinfoservice;
                    var tmpus = uis.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.Account);
                    switch (itemname)
                    {
                        case "account":
                            foreach (var i in tmpus)
                            {
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.Account);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.Account);
                            }
                            break;
                        case "fullname":
                            foreach (var i in uis.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.FullName))
                            {
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.FullName);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.FullName);
                            }
                            break;
                        case "bothname":
                            foreach (var i in uis.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.Account))
                            {
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}_{2}</option>", i.ID, i.Account, i.FullName);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}_{2}</option>", i.ID, i.Account, i.FullName);
                            }
                            break;
                        default:
                            foreach (var i in uis.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.FullName))
                            {
                                if (i.ID == selectedvalue && selectedvalue != 0)
                                    sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.ID, i.FullName);
                                else
                                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ID, i.FullName);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            sb.Append("</select>");
            return sb.ToString();
        }
        /// <summary>
        /// 客户信息、供应商、厂家、用户下拉列表框,选中指定的项
        /// </summary>
        /// <param name="html"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectList_Auto(this HtmlHelper html, string showName, string className, string itemName, long selectedValue)
        {
            return MvcHtmlString.Create(SelectItem_Auto(showName, className, itemName, selectedValue));
        }
        /// <summary>
        /// 客户信息下拉列表框
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectList_Auto(this HtmlHelper html, string showName, string className, string itemName)
        {
            return SelectList_Auto(html, showName, className, itemName, 0);
        }

        /// <summary>
        /// 常量下拉框
        /// </summary>
        /// <param name="html">htmlhepler</param>
        /// <param name="showName">项目显示名称</param>
        /// <param name="itemName">项目名称</param>
        /// <param name="selectedValue">选择值</param>
        /// <returns>页面选择框</returns>
        public static MvcHtmlString SelectList_Common(this HtmlHelper html, string showName, string itemName, long selectedValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<select name=\"{0}\" id=\"{1}\" class=\"width-100\" >", showName, showName);
            //if (selectedValue == 0)
            //    sb.AppendFormat("<option value=\"\" selected=\"selected\"></option>");
            //else
            //    sb.AppendFormat("<option value=\"\"></option>");
            switch (itemName)
            {
                case "首营状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.ShouYingZhuangTai));
                    break;
                case "教育程度":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.Education));
                    break;
                case "性别":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.Sex));
                    break;
                case "是否":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.YesOrNo));
                    break;
                case "医疗器械管理类别":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.ManageType));
                    break;
                case "首营种类":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.ShouYingType));
                    break;
                case "储运要求":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.TranCondition));
                    break;
                case "入库类型":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.EntryType));
                    break;
                case "出库类型":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.OutgoingType));
                    break;
                case "入库计划状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.EntryPlanState));
                    break;
                case "出库计划状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.OutPlanState));
                    break;
                case "入库状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.EntryState));
                    break;
                case "出库状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.OutState));
                    break;
                case "验收状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.CheckState));
                    break;
                case "验收结果":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.CheckResult));
                    break;
                case "验收不符合项说明":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.CheckMemo));
                    break;
                case "验收标准":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.CheckStandard));
                    break;
                case "存货状态":
                    sb.Append(GetCommonSelect(selectedValue, MvcApplication.CargoState));
                    break;
                //case "":
                //    sb.Append(GetCommonSelect(selectedValue, MvcApplication));
                //    break;
                //case "":
                //    sb.Append(GetCommonSelect(selectedValue, MvcApplication));
                //    break;
                //case "":
                //    sb.Append(GetCommonSelect(selectedValue, MvcApplication));
                //    break;
                default:
                    break;
            }
            sb.Append("</select>");
            return MvcHtmlString.Create(sb.ToString());
        }
        private static string GetCommonSelect(long selectedvalue, Dictionary<int, string> commonselect)
        {
            string _comsel = "";
            foreach (var i in commonselect)
            {
                if (i.Key == selectedvalue && selectedvalue != 0)
                    _comsel = _comsel + string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", i.Key, i.Value);
                else
                    _comsel = _comsel + string.Format("<option value=\"{0}\">{1}</option>", i.Key, i.Value);
            }
            return _comsel;
        }
        /// <summary>
        /// 常量下拉框
        /// </summary>
        /// <param name="html">htmlhepler</param>
        /// <param name="showName">项目显示名称</param>
        /// <param name="itemName">项目名称</param>
        /// <returns>页面选择框</returns>
        public static MvcHtmlString SelectList_Common(this HtmlHelper html, string showName, string itemName)
        {
            return SelectList_Common(html, showName, itemName, 0);
        }

        public static MvcHtmlString Search_UserRights(this HtmlHelper html, int userid)
        {
            string curmodule = "";
            StringBuilder sb = new StringBuilder();
            Iauth_quanxianService qxservice = ServiceFactory.auth_quanxianservice;
            var rps = qxservice.GetPersonRightsFirst(userid).DistinctBy(auth_personrights => auth_personrights.ID);//.GroupBy(p=>p.ID).Select(g=>g.First());
            if (rps != null)
            {
                foreach (auth_personrights rp in rps)
                {
                    if (rp.ryid == userid)
                    {

                        if (curmodule.Equals(rp.module.Trim()))
                        {
                            sb.AppendLine("<li><i class=\"icon-double-angle-right\"></i><a href=\"/" + rp.controller.Trim() + "/" + rp.function.Trim() + "\">" + rp.name + "</a></li>");
                        }
                        else
                        {
                            if (curmodule.Length > 1)
                            {
                                sb.AppendLine("</ul>");
                                sb.AppendLine("</li>");
                            }
                            sb.AppendLine("<li>");
                            sb.AppendLine("<a href=\"#\" class=\"dropdown-toggle\">");
                            sb.AppendLine("<i class=\"" + getIcon(rp.module.Trim()) + "\"></i>");
                            sb.AppendLine("<span class=\"menu-text\">" + rp.module.Trim() + "</span>");
                            sb.AppendLine("<b class=\"arrow icon-angle-down\"></b>");
                            sb.AppendLine("</a>");
                            sb.AppendLine("<ul class=\"submenu\">");
                            curmodule = rp.module.Trim();
                            sb.AppendLine("<li><i class=\"icon-double-angle-right\"></i><a href=\"/" + rp.controller.Trim() + "/" + rp.function.Trim() + "\">" + rp.name + "</a></li>");
                        }
                    }
                    else
                    {
                        Console.WriteLine(rp.ryid);
                    }
                }
                sb.AppendLine("</ul>");
                sb.AppendLine("</li>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        private static string getIcon(string modulename)
        {
            string icon = "";
            switch (modulename)
            {
                case "首营管理":
                    icon = "icon-check";
                    break;
                case "基础数据":
                    icon = "icon-bookmark";
                    break;
                case "仓库操作":
                    icon = "icon-move";
                    break;
                case "仓库定义":
                    icon = "icon-gear";
                    break;
                case "质量管理":
                    icon = "icon-check-minus";
                    break;
                case "费用结算":
                    icon = "icon-money";
                    break;
                case "权限管理":
                    icon = "icon-ban-circle";
                    break;
                case "药监查核":
                    icon = "icon-fighter-jet";
                    break;
                case "客户服务":
                    icon = "icon-bullseye";
                    break;
                case "帐表查询":
                    icon = "icon-book";
                    break;
                case "系统管理":
                    icon = "icon-magic";
                    break;
                default:
                    icon = "icon-anchor";
                    break;
            }
            return icon;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString Search_userinfo(this HtmlHelper html)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"modal fade\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\">");
            sb.AppendLine("<div class=\"modal-dialog\">");
            sb.AppendLine("<div class=\"modal-content\">");
            sb.AppendLine("<div class=\"modal-header\">");
            sb.AppendLine("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">");
            sb.AppendLine("&times;");
            sb.AppendLine("</button>");
            sb.AppendLine("<h3 class=\"modal-title center\" id=\"myModalLabel\">");
            sb.AppendLine("用户信息查询");
            sb.AppendLine("</h3>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"modal-body\">");
            sb.AppendLine("<form role=\"form\" action=\"/userinfo/pagelist\" method=\"post\">");
            sb.AppendLine("<ul class=\"list-unstyled\">");
            sb.AppendLine("<li>");
            sb.AppendLine("<ul class=\"list-inline\">");
            sb.AppendLine("<li class=\"width-30\"><label for=\"Account\">账号</label></li>");
            sb.AppendLine("<li class=\"width-20\"><select name=\"AccountEqual\" class=\"width-100\"><option value=\"=\" selected=\"selected\">=</option><option value=\"like\">包含</option></select></li>");
            sb.AppendLine("<li class=\"width-30\"><input type=\"text\" class=\"form-control\" name=\"Account\" id=\"Account\" placeholder=\"请输入账号\"></li>");
            sb.AppendLine("<li class=\"width-10\"><select name=\"AccountAnd\" class=\"width-100\"><option value=\"and\" selected=\"selected\">与</option><option value=\"or\">或</option></select></li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("<div class=\"center\">");
            sb.AppendLine("<button type=\"button\" class=\"btn btn - default\">");
            sb.AppendLine("重置");
            sb.AppendLine("</button>");
            sb.AppendLine("<button type=\"submit\" class=\"btn btn - default\">提交</button>");
            sb.AppendLine("</div>");
            sb.AppendLine("</form>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }
        /// <summary>
        /// 查询操作符构造
        /// </summary>
        /// <param name="conditiontype">操作符</param>
        /// <returns></returns>
        public static string OperatorString(string conditiontype)
        {
            string os = "";
            switch (conditiontype)
            {
                case "System.String":
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"like\">包含</option>";
                    break;
                case "System.Int32":
                case "System.Int":
                case "System.Int16":
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\">\">></option><option value=\"<\"><</option>";
                    break;
                case "System.Decimal":
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\">\">></option><option value=\"<\"><</option>";
                    break;
                case "System.Double":
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\">\">></option><option value=\"<\"><</option>";
                    break;
                case "System.Boolean":
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"<>\">></option>";
                    break;
                default:
                    os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"like\">包含</option>";
                    break;
            }

            return os;
        }
        /// <summary>
        /// 操作符转换
        /// </summary>
        /// <param name="conditiontype">类型</param>
        /// <param name="operatevalue">操作符</param>
        /// <returns></returns>
        public static string OperatorString(string conditiontype, string operatevalue)
        {
            string os = "";
            try
            {
                switch (conditiontype)
                {
                    case "System.String":
                        if (operatevalue.Equals("="))
                            os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"like\">包含</option>";
                        else
                            os = "<option value=\"=\">=</option><option value=\"like\" selected=\"selected\">包含</option>";
                        break;
                    case "System.Int32":
                    case "System.Int":
                    case "System.Int16":
                    case "System.Decimal":
                    case "System.Double":
                        switch (operatevalue)
                        {
                            case ">":
                                os = "<option value=\"=\">=</option><option value=\">\" selected=\"selected\">></option><option value=\"<\"><</option>";
                                break;
                            case "<":
                                os = "<option value=\"=\">=</option><option value=\">\">></option><option value=\"<\" selected=\"selected\"><</option>";
                                break;
                            case "=":
                            default:
                                os = "<option value=\"=\" selected=\"selected\">=</option><option value=\">\">></option><option value=\"<\"><</option>";
                                break;
                        }
                        break;
                    case "System.Boolean":
                        if (operatevalue.Equals("="))
                            os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"<>\">></option>";
                        else
                            os = "<option value=\"=\">=</option><option value=\"<>\" selected=\"selected\">></option>";
                        break;
                    default:
                        os = "<option value=\"=\" selected=\"selected\">=</option><option value=\"like\">包含</option>";
                        break;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("AppLog").Error(string.Format("search condition operator get fail,{0}", ex.Message));
            }
            return os;
        }
        /// <summary>
        /// 查询元素分解构造
        /// </summary>
        /// <param name="sc">查询元素</param>
        /// <returns></returns>
        public static string SearchValueString(SearchConditionModel sc)
        {
            var svs = "";
            try
            {
                if (sc.ItemType.Contains("System."))
                {
                    switch (sc.ItemType)
                    {
                        case "System.String":
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                        case "System.Double":
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                        case "System.Int16":
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                        case "System.Decimal":
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                        case "System.DateTime":
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                        case "System.Boolean":
                            if (sc.ItemValue == null)
                                svs = string.Format("<select class=\"width-100\" name=\"{0}\"><option value=\"\" selected=\"selected\"></option><option value=\"yes\">是</option><option value=\"no\">否</option></select>", sc.ItemCode);
                            else
                            {
                                if (sc.ItemValue.Equals("True"))
                                    svs = string.Format("<select class=\"width-100\" name=\"{0}\"><option value=\"\"></option><option value=\"yes\" selected=\"selected\">是</option><option value=\"no\">否</option></select>", sc.ItemCode);
                                else
                                    svs = string.Format("<select class=\"width-100\" name=\"{0}\"><option value=\"\"></option><option value=\"yes\">是</option><option value=\"no\" selected=\"selected\">否</option></select>", sc.ItemCode);
                            }
                            break;
                        default:
                            svs = string.Format("<input type =\"text\" class=\"form-control\" name=\"{0}\" id=\"{1}\" placeholder=\"请输入{2}\" value=\"{3}\">", sc.ItemCode, sc.ItemCode, sc.ItemTitle, sc.ItemValue);
                            break;
                    }
                }
                else
                {
                    string myclassname = sc.ItemType.Split('.')[0];
                    string myclassitem = sc.ItemType.Split('.')[1];
                    switch (myclassname)
                    {
                        case "货主":
                        case "供应商":
                        case "厂家":
                            if (sc.ItemValue == null)
                                svs = SelectItem_Auto(sc.ItemCode, myclassname, myclassitem, 0);
                            else
                                svs = SelectItem_Auto(sc.ItemCode, myclassname, myclassitem, long.Parse(sc.ItemValue));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("AppLog").Error(string.Format("search condition init fail,{0}", ex.Message));
            }
            return svs;
        }
        /// <summary>
        /// 组合条件查询
        /// </summary>
        /// <param name="html">对象</param>
        /// <param name="searchuser">用户id</param>
        /// <param name="searchtitle">查询标题</param>
        /// <param name="searchurl">检索的url</param>
        /// <param name="pagebrief">页面的系统代码</param>
        /// <param name="sclist">查询页面构成元素</param>
        /// <returns></returns>
        public static MvcHtmlString Search_Condition(this HtmlHelper html, int searchuser, string searchtitle, string searchurl, string pagebrief, List<SearchConditionModel> sclist)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"modal fade\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\">");
            sb.AppendLine("<div class=\"modal-dialog\">");
            sb.AppendLine("<div class=\"modal-content\">");
            sb.AppendLine("<div class=\"modal-header\">");
            sb.AppendLine("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">");
            sb.AppendLine("&times;");
            sb.AppendLine("</button>");
            sb.AppendLine("<h3 class=\"modal-title center\" id=\"myModalLabel\"><i class=\"icon-code-fork\"></i><span class=\"large\">");
            sb.AppendFormat("{0}", searchtitle);
            sb.AppendLine("</span></h3>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"modal-body\">");
            sb.AppendFormat("<form role=\"form\" action=\"{0}\" method=\"post\">", searchurl);
            sb.AppendLine("<ul class=\"list-unstyled\">");
            foreach (SearchConditionModel scm in sclist)
            {
                if (scm.ItemValue == null)
                {
                    sb.AppendLine("<li>");
                    sb.AppendLine("<ul class=\"list-inline\">");
                    sb.AppendFormat("<li class=\"width-20\"><label for=\"{0}\">{1}</label></li>", scm.ItemCode, scm.ItemTitle);
                    sb.AppendFormat("<li class=\"width-20\"><select name=\"{0}Equal\" class=\"width-100\">{1}</select></li>", scm.ItemCode, OperatorString(scm.ItemType));
                    sb.AppendFormat("<li class=\"width-40\">{0}</li>", SearchValueString(scm));
                    sb.AppendFormat("<li class=\"width-10\"><select name=\"{0}And\" class=\"width-100\"><option value=\"and\" selected=\"selected\">与</option><option value=\"or\">或</option></select></li>", scm.ItemCode);
                    sb.AppendLine("</ul>");
                    sb.AppendLine("</li>");
                }
                else
                {
                    sb.AppendLine("<li>");
                    sb.AppendLine("<ul class=\"list-inline\">");
                    sb.AppendFormat("<li class=\"width-20\"><label for=\"{0}\">{1}</label></li>", scm.ItemCode, scm.ItemTitle);
                    sb.AppendFormat("<li class=\"width-20\"><select name=\"{0}Equal\" class=\"width-100\">{1}</select></li>", scm.ItemCode, OperatorString(scm.ItemType, scm.ItemOpValue));
                    sb.AppendFormat("<li class=\"width-40\">{0}</li>", SearchValueString(scm));
                    if (scm.ItemJion.Equals("and"))
                        sb.AppendFormat("<li class=\"width-10\"><select name=\"{0}And\" class=\"width-100\"><option value=\"and\" selected=\"selected\">与</option><option value=\"or\">或</option></select></li>", scm.ItemCode);
                    else
                        sb.AppendFormat("<li class=\"width-10\"><select name=\"{0}And\" class=\"width-100\"><option value=\"and\">与</option><option value=\"or\" selected=\"selected\">或</option></select></li>", scm.ItemCode);
                    sb.AppendLine("</ul>");
                    sb.AppendLine("</li>");
                }
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("<div class=\"center\">");
            sb.AppendLine("<button type=\"submit\" class=\"btn btn - default\"><i class=\"icon-upload\"></i>提交查询</button>");
            sb.AppendLine("</div>");
            sb.AppendLine("</form>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion

        #region 检索值
        public static MvcHtmlString GetTreeData(this HtmlHelper html, string treedata)
        {
            StringBuilder sb = new StringBuilder();
            string modulestr = "";
            string funstr = "";
            string funstr0 = "";
            StringBuilder sb1 = new StringBuilder();
            //module:fun1,fun2;
            if (treedata.Length > 0)
            {
                modulestr = "var tree_data11={";
                string[] mods = treedata.Split(';');
                foreach (string ms in mods)
                {
                    if (ms.Length > 0)
                    {
                        string modname = ms.Substring(0, ms.IndexOf(':'));
                        string modfuns = ms.Substring(ms.IndexOf(':') + 1);
                        if (modfuns.Length > 0 && modfuns != ";")
                        {
                            modulestr = modulestr + "'" + modname + "':{ name:'" + modname + "',type:'folder'},";
                            funstr = "tree_data11['" + modname + "']['additionalParameters']={'children':{";
                            string[] funs = modfuns.Split(',');
                            foreach (string funname in funs)
                            {
                                if (funname.Length > 0)
                                {
                                    funstr = funstr + "'" + funname + "':{name:'" + funname + "',type:'item'},";
                                }
                            }
                            funstr = funstr.Substring(0, funstr.Length - 1);
                            funstr = funstr + "}}";
                        }
                    }
                    sb1.AppendLine(funstr);
                    //funstr0 = funstr0 + funstr;
                }
                modulestr = modulestr.Substring(0, modulestr.Length - 1);
                modulestr = modulestr + "}";
            }
            sb.AppendLine(modulestr);
            sb.AppendLine(sb1.ToString());
            return MvcHtmlString.Create(sb.ToString());
        }
        /// <summary>
        /// 通过id获取信息
        /// </summary>
        /// <param name="html">htmlhelper</param>
        /// <param name="className">数据类名</param>
        /// <param name="itemName">获取项目</param>
        /// <param name="dataValue">id值</param>
        /// <returns>项目值</returns>
        public static MvcHtmlString GetDataValue_ID(this HtmlHelper html, string className, string itemName, int dataValue)
        {
            string returnvalue = "";
            switch (className)
            {
                case "功能":
                    Iauth_gongnengService gnservice = ServiceFactory.auth_gongnengservice;
                    auth_gongneng gongneng = gnservice.GetEntityById(auth_gongneng => auth_gongneng.ID == dataValue);
                    if (gongneng == null)
                        returnvalue = "";
                    else
                    {
                        if (itemName == "名称")
                            returnvalue = gongneng.Name;
                        if (itemName == "控制")
                            returnvalue = gongneng.Controller;
                        if (itemName == "函数")
                            returnvalue = gongneng.Function;
                    }
                    break;
                case "角色":
                    Iauth_jueseService jss = ServiceFactory.auth_jueseservice;
                    auth_juese juese = jss.GetEntityById(auth_juese => auth_juese.ID == dataValue);
                    if (juese == null)
                        returnvalue = "";
                    else
                    {
                        if (itemName == "名称")
                            returnvalue = juese.RoleName;
                        if (itemName == "描述")
                            returnvalue = juese.RoleName;
                    }
                    break;
                case "userinfo":
                    IuserinfoService us = ServiceFactory.userinfoservice;
                    userinfo ui = us.GetEntityById(userinfo => userinfo.ID == dataValue);
                    if (ui == null)
                        returnvalue = "";
                    else
                    {
                        if (itemName == "账号")
                        {
                            returnvalue = ui.Account;
                        }
                        if (itemName == "全名")
                        {
                            returnvalue = ui.FullName;
                        }
                    }
                    break;
                case "货主":
                    Ibase_weituokehuService wts = ServiceFactory.base_weituokehuservice;
                    base_weituokehu wt = wts.GetEntityById(base_weituokehu => base_weituokehu.ID == dataValue);
                    if (wt == null)
                        returnvalue = "";
                    else
                    {
                        if (itemName == "代码")
                            returnvalue = wt.Daima;
                        if (itemName == "名称")
                            returnvalue = wt.Kehumingcheng;
                        if (itemName == "简称")
                            returnvalue = wt.Jiancheng;
                    }
                    break;
                case "生产企业":
                    Ibase_shengchanqiyeService _scqyservice = ServiceFactory.base_shengchanqiyeservice;
                    base_shengchanqiye _scqy = _scqyservice.GetEntityById(p => p.ID == dataValue);
                    if (_scqy == null)
                        returnvalue = "";
                    else
                    {
                        if (itemName == "代码")
                            returnvalue = _scqy.Daima;
                        if (itemName == "名称")
                            returnvalue = _scqy.Qiyemingcheng;
                        if (itemName == "企业地址")
                            returnvalue = _scqy.Qiyedizhi;

                    }
                    break;
                case "收货单位":
                    base_shouhuodanwei _shdw = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == dataValue);
                    if (_shdw == null)
                        returnvalue = "";
                    else
                        returnvalue = _shdw.Mingcheng;
                    break;
                case "供应商":
                    base_gongyingshang _gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == dataValue);
                    if (_gys == null)
                        returnvalue = "";
                    else
                        returnvalue = _gys.Mingcheng;
                    break;
                case "分类目录":
                    base_qixiemulu _qxml = ServiceFactory.base_qixiemuluservice.GetEntityById(p => p.ID == dataValue);
                    if (_qxml == null)
                        returnvalue = "";
                    else
                        returnvalue = _qxml.Mingcheng;
                    break;
                case "产品线":
                    base_chanpinxian _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == dataValue);
                    if (_cpx == null)
                        returnvalue = "";
                    else
                        returnvalue = _cpx.Mingcheng;
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(returnvalue);
        }
        /// <summary>
        /// 文件名转浏览方法
        /// </summary>
        /// <param name="html"></param>
        /// <param name="filename">文件名</param>
        /// <param name="showname">显示名</param>
        /// <param name="filetype">文件类型：1，证照；2，业务；3，其他</param>
        /// <returns></returns>
        public static MvcHtmlString GetCommonURL(this HtmlHelper html, string filename, string showname, int filetype)
        {
            string returnvalue = "";
            if (filename.Length < 1)
                return MvcHtmlString.Create(returnvalue);
            switch (filetype)
            {
                case 1:
                    returnvalue = "<a href='/files/zhengzhao/" + filename + "'  target='_blank'>" + showname + "</a>";
                    break;
                case 2:
                    returnvalue = "<a href='/files/yewu/" + filename + "'  target='_blank'>" + showname + "</a>";
                    break;
                case 3:
                    returnvalue = "<a href='/files/other/" + filename + "'  target='_blank'>" + showname + "</a>";
                    break;
                default:
                    returnvalue = filename;
                    break;
            }
            return MvcHtmlString.Create(returnvalue);
        }
        public static MvcHtmlString GetCommonValue_ID(this HtmlHelper html, string itemName, int dataValue)
        {
            string returnvalue = "";
            switch (itemName)
            {
                case "首营状态":
                    if (MvcApplication.ShouYingZhuangTai.ContainsKey(dataValue))
                        returnvalue = MvcApplication.ShouYingZhuangTai[dataValue];
                    break;
                case "首营类型":
                    if (MvcApplication.ShouYingType.ContainsKey(dataValue))
                        returnvalue = MvcApplication.ShouYingType[dataValue];
                    break;
                case "性别":
                    if (MvcApplication.Sex.ContainsKey(dataValue))
                        returnvalue = MvcApplication.Sex[dataValue];
                    break;
                case "教育程度":
                    if (MvcApplication.Education.ContainsKey(dataValue))
                        returnvalue = MvcApplication.Education[dataValue];
                    break;
                case "是否":
                    if (MvcApplication.YesOrNo.ContainsKey(dataValue))
                        returnvalue = MvcApplication.YesOrNo[dataValue];
                    break;
                case "医疗器械管理类别":
                    if (MvcApplication.ManageType.ContainsKey(dataValue))
                        returnvalue = MvcApplication.ManageType[dataValue];
                    break;
                case "储运要求":
                    if (MvcApplication.TranCondition.ContainsKey(dataValue))
                        returnvalue = MvcApplication.TranCondition[dataValue];
                    break;
                case "入库类型":
                    if (MvcApplication.EntryType.ContainsKey(dataValue))
                        returnvalue = MvcApplication.EntryType[dataValue];
                    break;
                case "出库类型":
                    if (MvcApplication.OutgoingType.ContainsKey(dataValue))
                        returnvalue = MvcApplication.OutgoingType[dataValue];
                    break;
                case "入库计划状态":
                    if (MvcApplication.EntryPlanState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.EntryPlanState[dataValue];
                    break;
                case "出库计划状态":
                    if (MvcApplication.OutPlanState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.OutPlanState[dataValue];
                    break;
                case "入库状态":
                    if (MvcApplication.OutPlanState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.EntryState[dataValue];
                    break;
                case "出库状态":
                    if (MvcApplication.OutPlanState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.OutState[dataValue];
                    break;
                case "验收状态":
                    if (MvcApplication.CheckState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.CheckState[dataValue];
                    break;
                case "验收结果":
                    if (MvcApplication.CheckResult.ContainsKey(dataValue))
                        returnvalue = MvcApplication.CheckResult[dataValue];
                    break;
                case "验收不符合项":
                    if (MvcApplication.CheckMemo.ContainsKey(dataValue))
                        returnvalue = MvcApplication.CheckMemo[dataValue];
                    break;
                case "验收标准":
                    if (MvcApplication.CheckMemo.ContainsKey(dataValue))
                        returnvalue = MvcApplication.CheckStandard[dataValue];
                    break;
                case "存货状态":
                    if (MvcApplication.CargoState.ContainsKey(dataValue))
                        returnvalue = MvcApplication.CargoState[dataValue];
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(returnvalue);
        }
        #endregion

        #region 文件上传
        /// <summary>
        /// 从页面上传文件
        /// </summary>
        /// <param name="html"></param>
        /// <param name="picName">浏览图片对象</param>
        /// <param name="refName">浏览地址</param>
        /// <param name="loadType">上传类别</param>
        /// <param name="loadClass">分类</param>
        /// <param name="selTime">页面上传数量</param>
        /// <param name="itemValue">值id</param>
        /// <param name="opStr">页面上传字典</param>
        /// <returns></returns>
        public static MvcHtmlString GetFileUpload(this HtmlHelper html, string picName, string refName, string loadType, string loadClass, int selTime, int itemValue, Dictionary<string, string> opStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("function doUpload(){");
            sb.AppendLine("var formData=new FormData($(\"#uploadForm\")[0]);");
            sb.AppendLine("var strBack=\"\";");
            sb.AppendLine("var strWrite=\"\";");
            sb.AppendLine("var strSel=$(\"#btnSelect\").val();");
            sb.AppendLine("$.ajax({");
            sb.AppendLine("url: '" + System.Web.Configuration.WebConfigurationManager.AppSettings["WebRootString"] + "/upload/UploadOK',");
            sb.AppendLine("type: 'POST',");
            sb.AppendLine("data: formData,");
            sb.AppendLine("async: false,");
            sb.AppendLine("cache: false,");
            sb.AppendLine("contentType: false,");
            sb.AppendLine("processData: false,");
            sb.AppendLine("success: function (returndata) {");
            sb.AppendLine("strBack=returndata;");
            sb.AppendLine("},");
            sb.AppendLine("error: function (returndata) {");
            sb.AppendLine("strBack=\"fail\";");
            sb.AppendLine("}");
            sb.AppendLine("});");
            sb.AppendLine("switch (strBack) {");
            sb.AppendLine("case \"0\":");
            sb.AppendLine("strWrite=\"无传送值，上传失败！\";");
            sb.AppendLine("break;");
            sb.AppendLine("case \"1\":");
            sb.AppendLine("strWrite=\"空文件，上传失败！\";");
            sb.AppendLine("break;");
            sb.AppendLine("case \"2\":");
            sb.AppendLine("strWrite=\"错误，上传失败！\";");
            sb.AppendLine("break;");
            sb.AppendLine("case \"3\":");
            sb.AppendLine("strWrite=\"文件尺寸超过4M，上传失败！\";");
            sb.AppendLine("break;");
            sb.AppendLine("case \"fail\":");
            sb.AppendLine("strWrite=\"文件太大，上传失败！\";");
            sb.AppendLine("break;");
            sb.AppendLine("default:");
            sb.AppendLine("strWrite=\"文件上传成功！\";");
            sb.AppendLine("break;");
            sb.AppendLine("}");
            sb.AppendLine("switch (strSel) {");
            int i = 1;
            foreach (var pp in opStr.Keys)
            {
                sb.AppendFormat("case \"picsel{0}\":", i);
                sb.AppendFormat("$(\"#{0}\").val(strBack);", opStr[pp]);
                sb.AppendFormat("$(\"#{0}\").html(\"<a href='/files/zhengzhao/\" + strBack + \"'  target='_blank'>浏览</a>\");", pp);
                sb.AppendLine("break;");
                i++;
            }
            sb.AppendLine("case \"\":");
            sb.AppendLine("default:");
            sb.AppendFormat("$(\"#{0}\").val(strBack);", picName);
            sb.AppendFormat("$(\"#{0}\").html(\"<a href='/files/zhengzhao/\" + strBack + \"'  target='_blank'>浏览</a>\");", refName);
            sb.AppendLine("break;");
            sb.AppendLine("}");
            //sb.AppendLine("alert(strWrite);");
            sb.AppendLine("}");
            for (int j = 1; j <= selTime; j++)
            {
                sb.AppendLine("function btn" + j + "(){");
                sb.AppendFormat("$(\"#btnSelect\").val(\"picsel{0}\");", j);
                sb.AppendLine("}");
            }
            sb.AppendLine("</script>");
            sb.AppendLine("<div class=\"modal fade\" id=\"myModalUpload\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\">");
            sb.AppendLine("<div class=\"modal-dialog\">");
            sb.AppendLine("<div class=\"modal-content\">");
            sb.AppendLine("<div class=\"modal-header\">");
            sb.AppendLine("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">");
            sb.AppendLine("&times;");
            sb.AppendLine("</button>");
            sb.AppendLine("<h3 class=\"modal-title center\" id=\"myModalLabel\">");
            sb.AppendLine("<i class=\"icon-file blue\"></i><span class=\"large\">");
            sb.AppendLine("文件上传(<4M)");
            sb.AppendLine("</span>");
            sb.AppendLine("</h3>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"modal-body center\">");
            sb.AppendLine("<form id=\"uploadForm\">");
            sb.AppendFormat("<input type=\"hidden\" name=\"uploadtype\" id=\"uploadtype\" value=\"{0}\" />", loadType);
            sb.AppendFormat("<input type=\"hidden\" name=\"uploaditemName\" id=\"uploaditemName\" value=\"{0}_{1}\" />", loadClass, itemValue);
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<input type=\"file\" name=\"file\" />");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<input type=\"button\" class=\"btn btn-default\" value=\"确 定\" onclick=\"doUpload()\" />");
            sb.AppendLine("</div>");
            sb.AppendLine("</form>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion
    }

}