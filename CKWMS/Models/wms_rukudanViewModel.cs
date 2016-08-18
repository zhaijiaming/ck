using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_rukudanViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "货主序号")]
        public int? HuozhuID { get; set; }
        [Display(Name = "计划序号")]
        public int? JihuaID { get; set; }
        [Display(Name = "信息来源")]
        public int? XinxiLY { get; set; }
        [Display(Name = "供应商序号")]
        public int? GongyingshangID { get; set; }
        [Display(Name = "发货地址")]
        public string Fahuodizhi { get; set; }
        [Display(Name = "运送地址")]
        public string Yunsongdizhi { get; set; }
        [Display(Name = "入库日期")]
        [DataType(DataType.Date)]
        public DateTime? RukuRQ { get; set; }
        [Display(Name = "业务类型")]
        public int? YewuLX { get; set; }
        [Display(Name = "是否保税")]
        public bool? BaoshuiSF { get; set; }
        [Display(Name = "是否监管")]
        public bool? JianguanSF { get; set; }
        [Display(Name = "是否验收")]
        public bool? YanshouSF { get; set; }
        [Display(Name = "储运要求")]
        public string ChunyunYQ { get; set; }
        [Display(Name = "客户单号")]
        public string KehuDH { get; set; }
        [Display(Name = "客服序号")]
        public int? KefuID { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "入库条码")]
        public string RukuTM { get; set; }
        [Display(Name = "入库状态")]
        public int? RukuZT { get; set; }
        [Display(Name = "对方区域")]
        public string DuifangQY { get; set; }
        [Display(Name = "收货人")]
        public int? Shouhuoren { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool? IsDelete { get; set; }
        [Display(Name = "入库单编号")]
        public string RukudanBH { get; set; }
    }
}

