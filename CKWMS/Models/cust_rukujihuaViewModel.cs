using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class cust_rukujihuaViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "货主")]
        public int? HuozhuID { get; set; }
        [Display(Name = "合同编号")]
        public string HetongBH { get; set; }
        [Display(Name = "供应商")]
        public int? GongyingshangID { get; set; }
        [Display(Name = "发货地址")]
        public string Fahuodizhi { get; set; }
        [Display(Name = "送达地址")]
        public string Yunsongdizhi { get; set; }
        [Display(Name = "入库日期")]
        [DataType(DataType.Date)]
        public DateTime? RukuRQ { get; set; }
        [Display(Name = "业务类型")]
        public int? YewuLX { get; set; }
        [Display(Name = "是否保税")]
        public bool BaoshuiSF { get; set; }
        [Display(Name = "储运要求")]
        public string ChunyunYQ { get; set; }
        [Display(Name = "是否监管")]
        public bool JianguanSF { get; set; }
        [Display(Name = "客户单号")]
        public string KehuDH { get; set; }
        [Display(Name = "客服序号")]
        public int? KefuID { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "计划状态")]
        public int? JihuaZT { get; set; }
        [Display(Name = "入库单数量")]
        public int? RukudanSL { get; set; }
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
        public bool IsDelete { get; set; }
    }
}

