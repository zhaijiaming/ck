using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class json_deliveryViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "OBD号码")]
        public string DELIVERY_NUMBER { get; set; }
        [Display(Name = "发货日期")]
        [DataType(DataType.Date)]
        public DateTime? POSTING_DATE { get; set; }
        [Display(Name = "仓库编号")]
        public string WH_NUMBER { get; set; }
        [Display(Name = "发货总数量")]
        public int? DELIVERY_QTY { get; set; }
        [Display(Name = "收货方名称")]
        public string SHIP_TO_NAME { get; set; }
        [Display(Name = "收货方代码")]
        public string SHIP_TO_NUMBER { get; set; }
        [Display(Name = "邮政编码")]
        public string POSTAL_CODE { get; set; }
        [Display(Name = "付款方代码")]
        public string SOLD_TO_NUMBER { get; set; }
        [Display(Name = "付款方名称")]
        public string SOLD_TO_NAME { get; set; }
        [Display(Name = "上传时间戳")]
        [DataType(DataType.Date)]
        public DateTime? UPLOAD_DATETIME { get; set; }
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

