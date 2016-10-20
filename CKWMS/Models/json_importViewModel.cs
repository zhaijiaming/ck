using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class json_importViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "发货单序号")]
        public int? BillID { get; set; }
        [Display(Name = "发货编号")]
        public string BillNum { get; set; }
        [Display(Name = "计划序号")]
        public int? PlanID { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
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

