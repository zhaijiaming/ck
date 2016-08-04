using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class auth_jueseViewModel
    {
        [Display(Name = "序号")]
        public int ID { get; set; }
        [Display(Name = "名称")]
        public string RoleName { get; set; }
        [Display(Name = "描述")]
        public string RoleDescripe { get; set; }
        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "关闭")]
        public bool? IsClose { get; set; }
    }
}

