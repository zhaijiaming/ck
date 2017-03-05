using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_zhiliangwhViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "存货序号")]
        public int? CHID { get; set; }
        [Display(Name = "质量状态")]
        public bool ZhiliangZT { get; set; }
        [Display(Name = "修改原因")]
        public string XiugaiYY { get; set; }
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

