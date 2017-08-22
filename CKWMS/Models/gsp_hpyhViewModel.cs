using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class gsp_hpyhViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "货主序号")]
        public int? HZID { get; set; }
        [Display(Name = "养护日期")]
        [DataType(DataType.Date)]
        public DateTime? YanghuoRQ { get; set; }
        [Display(Name = "结果")]
        public bool? Jiegu { get; set; }
        [Display(Name = "贮存条件")]
        public string ZCTJ { get; set; }
        [Display(Name = "作业流程")]
        public string ZYLC { get; set; }
        [Display(Name = "货品标识")]
        public string HPBS { get; set; }
        [Display(Name = "防护措施")]
        public string FHCS { get; set; }
        [Display(Name = "卫生环境")]
        public string WSHJ { get; set; }
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

