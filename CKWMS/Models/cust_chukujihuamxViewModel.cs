using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class cust_chukujihuamxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "计划序号")]
        public int? JihuaID { get; set; }
        [Display(Name = "商品序号")]
        public int? ShangpinID { get; set; }
        [Display(Name = "商品名称")]
        public string ShangpinMC { get; set; }
        [Display(Name = "注册证")]
        public string Zhucezheng { get; set; }
        [Display(Name = "规格")]
        public string Guige { get; set; }
        [Display(Name = "批号")]
        public string Pihao { get; set; }
        [Display(Name = "次批号")]
        public string Pihao1 { get; set; }
        [Display(Name = "序列码")]
        public string Xuliema { get; set; }
        [Display(Name = "生产日期")]
        [DataType(DataType.Date)]
        public DateTime? ShengchanRQ { get; set; }
        [Display(Name = "失效日期")]
        [DataType(DataType.Date)]
        public DateTime? ShiXiaoRQ { get; set; }
        [Display(Name = "计划数量")]
        public float? JihuaSL { get; set; }
        [Display(Name = "出库数量")]
        public float? ChukuSL { get; set; }
        [Display(Name = "基本单位")]
        public string JibenDW { get; set; }
        [Display(Name = "包装单位")]
        public string BaozhuangDW { get; set; }
        [Display(Name = "换算率")]
        public float? Huansuanlv { get; set; }
        [Display(Name = "厂家")]
        public string Changjia { get; set; }
        [Display(Name = "产地")]
        public string Chandi { get; set; }
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
        [Display(Name = "商品代码")]
        public string ShangpinDM { get; set; }
    }
}

