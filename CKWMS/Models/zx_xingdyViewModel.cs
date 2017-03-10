using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class zx_xingdyViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "型号")]
        public string Xianghao { get; set; }
        [Display(Name = "箱型")]
        public int? Xiangxing { get; set; }
        [Display(Name = "体积")]
        public float? Tiji { get; set; }
        [Display(Name = "状态")]
        public int? Zhuangtai { get; set; }
        [Display(Name = "开始装箱时间")]
        [DataType(DataType.Date)]
        public DateTime? KaishiSJ { get; set; }
        [Display(Name = "结束装箱时间")]
        [DataType(DataType.Date)]
        public DateTime? JieshuoSJ { get; set; }
        [Display(Name = "是否占用")]
        public bool ZhanyongSF { get; set; }
        [Display(Name = "是否周转")]
        public bool ZhouzhuanSF { get; set; }
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

