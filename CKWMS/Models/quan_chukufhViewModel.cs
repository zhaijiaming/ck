using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_chukufhViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "明细序号")]
        public int? MingxiID { get; set; }
        [Display(Name = "复核数量")]
        public float? FuheSL { get; set; }
        [Display(Name = "复核")]
        public int? Fuhe { get; set; }
        [Display(Name = "复核人")]
        public string Fuheren { get; set; }
        [Display(Name = "复核说明")]
        public string FuheSM { get; set; }
        [Display(Name = "复核状态")]
        public int? FuheZT { get; set; }
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

