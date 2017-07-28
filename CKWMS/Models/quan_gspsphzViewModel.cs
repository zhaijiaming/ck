using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_gspsphzViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "商品序号")]
        public int? SPID { get; set; }
        [Display(Name = "商品代码")]
        public string SPDM { get; set; }
        [Display(Name = "注册证序号")]
        public int? ZCZID { get; set; }
        [Display(Name = "注册证编号")]
        public string ZCZDM { get; set; }
        [Display(Name = "类型")]
        public bool? LX { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool Isdelete { get; set; }
    }
}

