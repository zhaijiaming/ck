using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class view_roledetailViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "序号")]
        public int id { get; set; }
        [Display(Name = "角色")]
        public string rolename { get; set; }
        [Display(Name = "功能")]
        public string name { get; set; }
        [Display(Name = "模块")]
        public string module { get; set; }
        [Display(Name = "控制")]
        public string controller { get; set; }
        [Display(Name = "函数")]
        public string function { get; set; }
        [Display(Name = "等级")]
        public int? grade { get; set; }
    }
}

