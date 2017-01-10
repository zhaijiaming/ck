using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class u8_lackcargoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "U8单号")]
        public string U8Code { get; set; }
        [Display(Name = "货号")]
        public string CargoCode { get; set; }
        [Display(Name = "数量")]
        public float? RecieveNumber { get; set; }
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

