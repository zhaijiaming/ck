using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_cangkuViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "简称")]
        [Required]
        public string Jiancheng { get; set; }
        [Display(Name = "名称")]
        [Required]
        public string Mingcheng { get; set; }
        [Display(Name = "注册地址")]
        public string ZhuceDZ { get; set; }
        [Display(Name = "收货地址")]
        public string ShouhuoDZ { get; set; }
        [Display(Name = "负责人序号")]
        public int? FuzerenID { get; set; }
        [Display(Name = "负责人")]
        public string Fuzeren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "发票抬头")]
        public string FapiaoTT { get; set; }
        [Display(Name = "银行账号")]
        public string YinhangZH { get; set; }
        [Display(Name = "税号")]
        public string Shuihao { get; set; }
        [Display(Name = "营业执照")]
        public string YYZZTP { get; set; }
        [Display(Name = "经营许可")]
        public string JYXKTP { get; set; }
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

