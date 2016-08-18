﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CKWMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 是或否
        /// </summary>
        public static Dictionary<int, string> YesOrNo;
        /// <summary>
        /// 性别
        /// </summary>
        public static Dictionary<int, string> Sex;
        /// <summary>
        /// 教育程度
        /// </summary>
        public static Dictionary<int, string> Education;
        /// <summary>
        /// 医疗器械管理类别
        /// </summary>
        public static Dictionary<int, string> ManageType;
        /// <summary>
        /// 首营状态
        /// </summary>
        public static Dictionary<int, string> ShouYingZhuangTai;
        /// <summary>
        /// 首营种类
        /// </summary>
        public static Dictionary<int, string> ShouYingType;
        /// <summary>
        /// 储运要求
        /// </summary>
        public static Dictionary<int, string> TranCondition;
        /// <summary>
        /// 入库类型
        /// </summary>
        public static Dictionary<int, string> EntryType;
        /// <summary>
        /// 出库类型
        /// </summary>
        public static Dictionary<int, string> OutgoingType;
        /// <summary>
        /// 入库计划状态
        /// </summary>
        public static Dictionary<int, string> EntryPlanState;
        /// <summary>
        /// 出库计划状态
        /// </summary>
        public static Dictionary<int, string> OutPlanState;
        /// <summary>
        /// 验收状态
        /// </summary>
        public static Dictionary<int, string> CheckState;
        /// <summary>
        /// 验收结果
        /// </summary>
        public static Dictionary<int, string> CheckResult;
        /// <summary>
        /// 验收不符合项说明
        /// </summary>
        public static Dictionary<int, string> CheckMemo;
        /// <summary>
        /// 验收标准
        /// </summary>
        public static Dictionary<int, string> CheckStandard;
        /// <summary>
        /// 存货状态
        /// </summary>
        public static Dictionary<int, string> CargoState;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //davis define
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            InitCommon();
        }

        private void InitCommon()
        {
            
            //是或否
            YesOrNo = new Dictionary<int, string>();
            YesOrNo.Add(0, "");
            YesOrNo.Add(1, "是");
            YesOrNo.Add(2, "否");
            //性别
            Sex = new Dictionary<int, string>();
            Sex.Add(0, "");
            Sex.Add(1, "男");
            Sex.Add(2, "女");
            Sex.Add(3, "无");
            //教育程度
            Education = new Dictionary<int, string>();
            Education.Add(0, "");
            Education.Add(1, "小学");
            Education.Add(2, "初中");
            Education.Add(3, "高中");
            Education.Add(4, "大学");
            Education.Add(5, "硕士");
            Education.Add(6, "博士");

            //医疗器械管理类别
            ManageType = new Dictionary<int, string>();
            ManageType.Add(0, "");
            ManageType.Add(1, "管理类别 Ⅰ");
            ManageType.Add(2, "管理类别 Ⅱ");
            ManageType.Add(3, "管理类别 Ⅲ");

            //首营状态
            ShouYingZhuangTai = new Dictionary<int, string>();
            ShouYingZhuangTai.Add(0, "");
            ShouYingZhuangTai.Add(1, "新建");
            ShouYingZhuangTai.Add(2, "待审核");
            ShouYingZhuangTai.Add(3, "质量部通过");
            ShouYingZhuangTai.Add(4, "负责人通过");
            ShouYingZhuangTai.Add(5, "已通过");
            ShouYingZhuangTai.Add(-1, "未通过");

            //储运要求
            TranCondition = new Dictionary<int, string>();
            TranCondition.Add(0, "");
            TranCondition.Add(1, "常温");
            TranCondition.Add(2, "冷藏");
            TranCondition.Add(3, "冷冻");

            EntryType = new Dictionary<int, string>();
            EntryType.Add(0, "");
            EntryType.Add(1, "进货");
            EntryType.Add(2, "移库");
            EntryType.Add(3, "销退");
            EntryType.Add(4, "赠品");
            EntryType.Add(5, "换货");
            EntryType.Add(6, "其它");

            EntryPlanState = new Dictionary<int, string>();
            EntryPlanState.Add(0, "");
            EntryPlanState.Add(1, "新建");
            EntryPlanState.Add(2, "执行中");
            EntryPlanState.Add(3, "部分入库");
            EntryPlanState.Add(4, "入库完成");
            EntryPlanState.Add(5, "历史入库");

            OutgoingType = new Dictionary<int, string>();
            OutgoingType.Add(0, "");
            OutgoingType.Add(1, "销售");
            OutgoingType.Add(2, "移库");
            OutgoingType.Add(3, "采退");
            OutgoingType.Add(4, "赠品");
            OutgoingType.Add(5, "换货");
            OutgoingType.Add(6, "其它");

            OutPlanState = new Dictionary<int, string>();
            OutPlanState.Add(0, "");
            OutPlanState.Add(1, "新建");
            OutPlanState.Add(2, "执行中");
            OutPlanState.Add(3, "部分出库");
            OutPlanState.Add(4, "出库完成");
            OutPlanState.Add(5, "历史出库");

            CheckState = new Dictionary<int, string>();
            CheckState.Add(0, "");
            CheckState.Add(1, "未检验");//没有检验
            CheckState.Add(2, "待检验");//正在检验
            CheckState.Add(3, "已检验");//完成检验

            CheckResult = new Dictionary<int, string>();
            CheckResult.Add(0, "");
            CheckResult.Add(1, "合格");
            CheckResult.Add(2, "部分合格");
            CheckResult.Add(3, "不合格");

            CheckMemo = new Dictionary<int, string>();
            CheckMemo.Add(0, "");
            CheckMemo.Add(1, "未见异常，检查验收合格");
            CheckMemo.Add(2, "近效期，包装外观未见异常");
            CheckMemo.Add(3, "退货经检查验收，包装外观符合要求，可入库");
            CheckMemo.Add(4, "包装损坏，货品经检验后合格，可入库");
            CheckMemo.Add(5, "包装破损，不合格");
            CheckMemo.Add(6, "货品经检验后判定不合格");

            CargoState = new Dictionary<int, string>();
            CargoState.Add(0, "");
            CargoState.Add(1, "正常");
            CargoState.Add(2, "破损");
            CargoState.Add(3, "污染");
            CargoState.Add(4, "渗漏");
            CargoState.Add(5, "其它");

            ShouYingType = new Dictionary<int, string>();
            ShouYingType.Add(0, "");
            ShouYingType.Add(1, "商品");
            ShouYingType.Add(2, "客户");
            ShouYingType.Add(3, "供应商");
            ShouYingType.Add(4, "收货方");
            ShouYingType.Add(5, "工厂");
            ShouYingType.Add(6, "销售");
            ShouYingType.Add(7, "发货方");
            ShouYingType.Add(8, "运输单位");

            CheckStandard = new Dictionary<int, string>();
            CheckStandard.Add(0, "");
            CheckStandard.Add(1, "包装质量");
            CheckStandard.Add(2, "外观质量");
            CheckStandard.Add(3, "检验报告");
            CheckStandard.Add(4, "检验证标志");
            CheckStandard.Add(5, "合格证标志");
            CheckStandard.Add(6, "注册证");
            CheckStandard.Add(7, "存储质量");
            CheckStandard.Add(8, "运输温度");
        }
    }
}
