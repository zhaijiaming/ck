using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class cfda_outhistoryViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "原序号")]
        public string XH { get; set; }
        [Display(Name = "委托方企业名称")]
        public string WTFQYMC { get; set; }
        [Display(Name = "出库日期")]
        public string CKRQ { get; set; }
        [Display(Name = "出库类型")]
        public string CKLX { get; set; }
        [Display(Name = "货品名称")]
        public string HPMC { get; set; }
        [Display(Name = "规格(型号)")]
        public string GGXH { get; set; }
        [Display(Name = "生产企业")]
        public string SCQY { get; set; }
        [Display(Name = "产品注册证号")]
        public string CPZCZ { get; set; }
        [Display(Name = "生产批号")]
        public string SCPH { get; set; }
        [Display(Name = "储运条件")]
        public string CYTJ { get; set; }
        [Display(Name = "有效期")]
        public string YXQ { get; set; }
        [Display(Name = "单位")]
        public string DW { get; set; }
        [Display(Name = "数量")]
        public string SL { get; set; }
        [Display(Name = "收货客户名称")]
        public string SHKHMC { get; set; }
        [Display(Name = "收货地址")]
        public string SHDZ { get; set; }
        [Display(Name = "联系人")]
        public string LXR { get; set; }
        [Display(Name = "联系方式")]
        public string LXFS { get; set; }
        [Display(Name = "备注")]
        public string BZ { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

