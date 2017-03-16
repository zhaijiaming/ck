using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class zx_xuanxiangViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "箱子序号")]
        public int? XiangID { get; set; }
        [Display(Name = "出库序号")]
        public int? ChukuID { get; set; }
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

