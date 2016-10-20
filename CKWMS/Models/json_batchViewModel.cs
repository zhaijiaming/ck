using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class json_batchViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "OBD号码")]
        public string DELIVERY_NUMBER { get; set; }
        [Display(Name = "包装箱号")]
        public string PACKAGE_NUMBER { get; set; }
        [Display(Name = "客户采购单号")]
        public string PURCHASE_ORDER { get; set; }
        [Display(Name = "产品型号代码")]
        public string MATERIAL_NUMBER { get; set; }
        [Display(Name = "产品批号")]
        public string BATCH_NUMBER { get; set; }
        [Display(Name = "发货数量")]
        public int? DELIVERY_QTY { get; set; }
        [Display(Name = "发货包装单位")]
        public string UOM { get; set; }
        [Display(Name = "有效期")]
        [DataType(DataType.Date)]
        public DateTime? EXP_DATE { get; set; }
        [Display(Name = "序列号管理标志")]
        public int? SN_FLAG { get; set; }
        [Display(Name = "生产日期")]
        [DataType(DataType.Date)]
        public DateTime? DOM_DATE { get; set; }
        [Display(Name = "产品中文描述")]
        public string DESCRIPTION_SC { get; set; }
        [Display(Name = "制造厂家名称")]
        public string MANUFACTURER_NAME { get; set; }
        [Display(Name = "产品中文名称")]
        public string PRODUCT_LABEL_NAME { get; set; }
        [Display(Name = "注册证号")]
        public string LICENSE_NO { get; set; }
        [Display(Name = "保存条件")]
        public string OTHER_CONDITION { get; set; }
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

