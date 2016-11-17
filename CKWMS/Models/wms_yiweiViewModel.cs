using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_yiweiViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "收货序号")]
        public int? SHID { get; set; }
        [Display(Name = "库存序号")]
        public int? KCID { get; set; }
        [Display(Name = "库位编号")]
        public string KWBH { get; set; }
        [Display(Name = "库位序号")]
        public int? KWID { get; set; }
        [Display(Name = "移出数量")]
        public int? Shuliang { get; set; }
        [Display(Name = "新库存序号")]
        public int? XKCID { get; set; }
        [Display(Name = "新库位编号")]
        public string XKWBH { get; set; }
        [Display(Name = "新库位序号")]
        public int? XKWID { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
    }
}

