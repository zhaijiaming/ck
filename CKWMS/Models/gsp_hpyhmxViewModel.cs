using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class gsp_hpyhmxViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "养护序号")]
        public int? YHID { get; set; }
        [Display(Name = "存货序号")]
        public int? CHID { get; set; }
        [Display(Name = "检查")]
        public string Jiancha { get; set; }
        [Display(Name = "处理结果")]
        public string CLJG { get; set; }
        [Display(Name = "养护人")]
        public string YHR { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

