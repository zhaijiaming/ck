using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_shouyingsqViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "类别")]
        public int Leibie { get; set; }
        [Display(Name = "内容")]
        public string Neirong { get; set; }
        [Display(Name = "申请时间")]
        [DataType(DataType.Date)]
        public DateTime? SQshijian { get; set; }
        [Display(Name = "申请人")]
        public string SQren { get; set; }
        [Display(Name = "状态")]
        public int? Zhuangtai { get; set; }
        [Display(Name = "审核人")]
        public string Shenheren { get; set; }
        [Display(Name = "审核说明")]
        public string Shenheshuoming { get; set; }
        [Display(Name = "机动1")]
        public string Col1 { get; set; }
        [Display(Name = "机动2")]
        public string Col2 { get; set; }
        [Display(Name = "机动3")]
        public string Col3 { get; set; }
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "申请序号")]
        public int? ShenqingID { get; set; }
        [Display(Name = "负责人")]
        public string Fuzeren { get; set; }
        [Display(Name = "负责人说明")]
        public string FuzerenSM { get; set; }
    }
}

