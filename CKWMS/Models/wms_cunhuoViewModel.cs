using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_cunhuoViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "明细序号")]
        public int? RKMXID { get; set; }
        [Display(Name = "库位")]
        public string Kuwei { get; set; }
        [Display(Name = "库位序号")]
        public int? KuweiID { get; set; }
        [Display(Name = "数量")]
        public float? Shuliang { get; set; }
        [Display(Name = "重量")]
        public float? Zhongliang { get; set; }
        [Display(Name = "净重")]
        public float? Jingzhong { get; set; }
        [Display(Name = "体积")]
        public float? Tiji { get; set; }
        [Display(Name = "计费吨")]
        public float? Jifeidun { get; set; }
        [Display(Name = "待拣数量")]
        public float? DaijianSL { get; set; }
        [Display(Name = "是否锁定")]
        public bool SuodingSF { get; set; }
        [Display(Name = "存货状态")]
        public int? CunhuoZT { get; set; }
        [Display(Name = "存货说明")]
        public string CunhuoSM { get; set; }
        [Display(Name = "是否加货")]
        public bool JiahuoSF { get; set; }
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
        [Display(Name = "是否合格")]
        public bool HegeSF { get; set; }
    }
}

