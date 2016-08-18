using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_rukuysViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "明细序号")]
        public int? MingxiID { get; set; }
        [Display(Name = "验收数量")]
        public float? YanshouSL { get; set; }
        [Display(Name = "验收")]
        public int Yanshou { get; set; }
        [Display(Name = "验收人")]
        public string Yanshouren { get; set; }
        [Display(Name = "验收说明")]
        public string YanshouSM { get; set; }
        [Display(Name = "验收状态")]
        public int YanshouZT { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

