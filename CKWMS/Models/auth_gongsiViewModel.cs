using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class auth_gongsiViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "代码")]
        public string Daima { get; set; }
        [Display(Name = "简称")]
        public string Jiancheng { get; set; }
        [Display(Name = "名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "标识")]
        public string Biaozhi { get; set; }
        [Display(Name = "合同")]
        public string HetongBH { get; set; }
        [Display(Name = "有效期")]
        [DataType(DataType.Date)]
        public DateTime? YXQ { get; set; }
        [Display(Name = "类型")]
        public string Leixing { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string LianxiDH { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
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

