using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_cangguanszViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "仓库")]
        [Required]
        public int? CangkuID { get; set; }
        [Display(Name = "人员")]
        [Required]
        public int? RenyuanID { get; set; }
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

