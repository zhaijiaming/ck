using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class json_packageViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "OBD号码")]
        public string DELIVERY_NUMBER { get; set; }
        [Display(Name = "包装箱号")]
        public string PACKAGE_NUMBER { get; set; }
        [Display(Name = "产品型号代码")]
        public string MATERIAL_NUMBER { get; set; }
        [Display(Name = "产品批号")]
        public string BATCH_NUMBER { get; set; }
        [Display(Name = "产品唯一号")]
        public string SERIAL_NUMBER { get; set; }
        [Display(Name = "包装盒号")]
        public string BOX_NUMBER { get; set; }
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

