using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_cangkuqyViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "仓库")]
        [Required]
        public int? CangkuID { get; set; }
        [Display(Name = "区域名称")]
        [Required]
        public string Mingcheng { get; set; }
        [Display(Name = "区域代码")]
        [Required]
        public string Daima { get; set; }
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
        [Display(Name = "类型")]
        public int? Leixing { get; set; }
        [Display(Name = "功能类型")]
        public int? GongnengLX { get; set; }
    }
}

