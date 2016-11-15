using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_scanpickViewModel
    {
        [Display(Name = "库位")]
        public string KuWei { get; set; }
        [Display(Name = "待拣数量")]
        public float DaijianSL { get; set; }
        [Display(Name = "拣货数量")]
        public float JianhuoSL { get; set; }
    }
}