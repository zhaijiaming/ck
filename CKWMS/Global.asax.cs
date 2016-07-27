using System;
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
            YesOrNo.Add(1, "是");
            YesOrNo.Add(2, "否");
            //性别
            Sex = new Dictionary<int, string>();
            Sex.Add(1, "男");
            Sex.Add(2, "女");
            Sex.Add(3, "无");
            //教育程度
            Education = new Dictionary<int, string>();
            Education.Add(1, "小学");
            Education.Add(2, "初中");
            Education.Add(3, "高中");
            Education.Add(4, "大学");
            Education.Add(5, "硕士");
            Education.Add(6, "博士");

            //医疗器械管理类别
            ManageType = new Dictionary<int, string>();
            ManageType.Add(1, "管理类别 Ⅰ");
            ManageType.Add(2, "管理类别 Ⅱ");
            ManageType.Add(3, "管理类别 Ⅲ");

            //首营状态
            ShouYingZhuangTai = new Dictionary<int, string>();
            ShouYingZhuangTai.Add(1, "新建");
            ShouYingZhuangTai.Add(2, "待审核");
            ShouYingZhuangTai.Add(3, "审核中");
            ShouYingZhuangTai.Add(4, "已通过");
            ShouYingZhuangTai.Add(5, "未通过");

            //储运要求
            TranCondition = new Dictionary<int, string>();
            TranCondition.Add(1, "常温");
            TranCondition.Add(2, "冷藏");
            TranCondition.Add(3, "冷冻");

            EntryType = new Dictionary<int, string>();
            EntryType.Add(1, "进货");
            EntryType.Add(2, "移库");
            EntryType.Add(3, "销退");
            EntryType.Add(4, "赠品");
            EntryType.Add(5, "换货");
            EntryType.Add(6, "其它");

            EntryPlanState = new Dictionary<int, string>();
            EntryPlanState.Add(1, "新建");
            EntryPlanState.Add(2, "执行中");
            EntryPlanState.Add(3, "部分入库");
            EntryPlanState.Add(4, "入库完成");
            EntryPlanState.Add(5, "历史入库");

            OutgoingType = new Dictionary<int, string>();
            OutgoingType.Add(1, "销售");
            OutgoingType.Add(2, "移库");
            OutgoingType.Add(3, "采退");
            OutgoingType.Add(4, "赠品");
            OutgoingType.Add(5, "换货");
            OutgoingType.Add(6, "其它");

            OutPlanState = new Dictionary<int, string>();
            OutPlanState.Add(1, "新建");
            OutPlanState.Add(2, "执行中");
            OutPlanState.Add(3, "部分出库");
            OutPlanState.Add(4, "出库完成");
            OutPlanState.Add(5, "历史出库");
        }
    }
}
