using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class wms_kuweiViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "区域")]
        public int? QuyuID { get; set; }
        [Display(Name = "库位名称")]
        public string Mingcheng { get; set; }
        [Display(Name = "货架")]
        [Required]
        public int? Huojia { get; set; }
        [Display(Name = "列数")]
        [Required]
        public int? Lieshu { get; set; }
        [Display(Name = "层数")]
        [Required]
        public int? Censhu { get; set; }
        [Display(Name = "商品数量")]
        public float? ShangpinSL { get; set; }
        [Display(Name = "批次数量")]
        public float? PiciSL { get; set; }
        [Display(Name = "最大重量")]
        public float? MaxWeight { get; set; }
        [Display(Name = "最大体积")]
        public float? MaxVolumn { get; set; }
        [Display(Name = "是否启用")]
        public bool QiyongSF { get; set; }
        [Display(Name = "格数")]
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

