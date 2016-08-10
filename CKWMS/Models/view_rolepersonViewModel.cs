using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class view_rolepersonViewModel
    {
        [Display(Name = "角色序号")]
        public int id { get; set; }
        [Display(Name = "角色名称")]
        public string rolename { get; set; }
        [Display(Name = "功能序号")]
        public int? funid { get; set; }
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
        [Display(Name = "人员序号")]
        public int? ryid { get; set; }
    }
}

