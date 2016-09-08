using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_jianhuoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "明细序号")]
        public int? CKMXID { get; set; }
        [Display(Name = "库位")]
        public string Kuwei { get; set; }
        [Display(Name = "库位序号")]
        public int? KuweiID { get; set; }
        [Display(Name = "待拣数量")]
        public float? DaijianSL { get; set; }
        [Display(Name = "重量")]
        public float? Zhongliang { get; set; }
        [Display(Name = "净重")]
        public float? Jingzhong { get; set; }
        [Display(Name = "体积")]
        public float? Tiji { get; set; }
        [Display(Name = "计费吨")]
        public float? Jifeidun { get; set; }
        [Display(Name = "实拣数量")]
        public float? ShijianSL { get; set; }
        [Display(Name = "拣货说明")]
        public string JianhuoSM { get; set; }
        [Display(Name = "拣货人")]
        public int? Jianhuoren { get; set; }
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

