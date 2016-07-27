using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class base_weituokehuViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "客户代码")]
        [Required]
        public string Daima { get; set; }
        [Display(Name = "客户简称")]
        [Required]
        public string Jiancheng { get; set; }
        [Display(Name = "客户名称")]
        [Required]
        public string Kehumingcheng { get; set; }
        [Display(Name = "合同编号")]
        public string Hetongbianhao { get; set; }
        [Display(Name = "营业执照编号")]
        public string YingyezhizhaoBH { get; set; }
        [Display(Name = "营业执照有效期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? YingyezhizhaoYXQ { get; set; }
        [Display(Name = "营业执照照片")]
        public string YingyezhizhaoTP { get; set; }
        [Display(Name = "组织机构编号")]
        public string ZuzhijigouBH { get; set; }
        [Display(Name = "组织机构有效期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ZuzhijigouYXQ { get; set; }
        [Display(Name = "组织机构照片")]
        public string ZuzhijigouTP { get; set; }
        [Display(Name = "税务登记编号")]
        public string ShuiwudengjiBH { get; set; }
        [Display(Name = "税务登记有效期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ShuiwudengjiYXQ { get; set; }
        [Display(Name = "税务登记照片")]
        public string ShuiwudengjiTP { get; set; }
        [Display(Name = "经营许可编号")]
        public string JingyingxukeBH { get; set; }
        [Display(Name = "经营许可有效期")]
        [DataType(DataType.Date)]
        public DateTime? JingyingxukeYXQ { get; set; }
        [Display(Name = "经营许可照片")]
        public string JingyingxukeTP { get; set; }
        [Display(Name = "经营范围")]
        public string Jingyingfanwei { get; set; }
        [Display(Name = "经营范围简码")]
        public string JingyingfanweiDM { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string Lianxidianhua { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "首营状态")]
        public int? Shouying { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "机动4")]
        public string Col4 { get; set; }
        [Display(Name = "机动5")]
        public string Col5 { get; set; }
        [Display(Name = "机动6")]
        public string Col6 { get; set; }
        [Display(Name = "录入日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "录入人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
    }
}

