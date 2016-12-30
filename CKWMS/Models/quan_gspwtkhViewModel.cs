using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
    public partial class quan_gspwtkhViewModel
    {
        [Display(Name = "序号")]
        public int? ID { get; set; }
        [Display(Name = "代码")]
        public string Daima { get; set; }
        [Display(Name = "简称")]
        public string Jiancheng { get; set; }
        [Display(Name = "名称")]
        public string Kehumingcheng { get; set; }
        [Display(Name = "合同编号")]
        public string Hetongbianhao { get; set; }
        [Display(Name = "营业执照编号")]
        public string YingyezhizhaoBH { get; set; }
        [Display(Name = "营业执照有效期")]
        [DataType(DataType.Date)]
        public DateTime? YingyezhizhaoYXQ { get; set; }
        [Display(Name = "营业执照图片")]
        public string YingyezhizhaoTP { get; set; }
        [Display(Name = "组织机构编号")]
        public string ZuzhijigouBH { get; set; }
        [Display(Name = "组织机构有效期")]
        [DataType(DataType.Date)]
        public DateTime? ZuzhijigouYXQ { get; set; }
        [Display(Name = "组织机构图片")]
        public string ZuzhijigouTP { get; set; }
        [Display(Name = "税务登记编号")]
        public string ShuiwudengjiBH { get; set; }
        [Display(Name = "税务登记有效期")]
        [DataType(DataType.Date)]
        public DateTime? ShuiwudengjiYXQ { get; set; }
        [Display(Name = "税务登记图片")]
        public string ShuiwudengjiTP { get; set; }
        [Display(Name = "经营许可编号")]
        public string JingyingxukeBH { get; set; }
        [Display(Name = "经营许可有效期")]
        [DataType(DataType.Date)]
        public DateTime? JingyingxukeYXQ { get; set; }
        [Display(Name = "经营许可图片")]
        public string JingyingxukeTP { get; set; }
        [Display(Name = "经营范围")]
        public string Jingyingfanwei { get; set; }
        [Display(Name = "经营范围代码")]
        public string JingyingfanweiDM { get; set; }
        [Display(Name = "联系人")]
        public string Lianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string Lianxidianhua { get; set; }
        [Display(Name = "备注")]
        public string Beizhu { get; set; }
        [Display(Name = "首营")]
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
        [Display(Name = "制单日期")]
        [DataType(DataType.Date)]
        public DateTime? MakeDate { get; set; }
        [Display(Name = "制单人")]
        public int? MakeMan { get; set; }
        [Display(Name = "已删除")]
        public bool IsDelete { get; set; }
        [Display(Name = "是否合作")]
        public bool ShenchaSF { get; set; }
        [Display(Name = "是否合作")]
        public bool HezuoSF { get; set; }
        [Display(Name = "备案编号")]
        public string BeianBH { get; set; }
        [Display(Name = "备案有效期")]
        [DataType(DataType.Date)]
        public DateTime? BeianYXQ { get; set; }
        [Display(Name = "备案批准日期")]
        [DataType(DataType.Date)]
        public DateTime? BeianPZRQ { get; set; }
        [Display(Name = "备案发证机关")]
        public string BeianFZJG { get; set; }
        [Display(Name = "备案图片")]
        public string BeianTP { get; set; }
        [Display(Name = "许可批准日期")]
        [DataType(DataType.Date)]
        public DateTime? XukePZRQ { get; set; }
        [Display(Name = "许可发证机关")]
        public string XukeFZJG { get; set; }
        [Display(Name = "注册地址")]
        public string ZhuceDZ { get; set; }
        [Display(Name = "经营地址")]
        public string JingyinDZ { get; set; }
        [Display(Name = "库房地址")]
        public string KufangDZ { get; set; }
        [Display(Name = "委托内容")]
        public string WeituoNR { get; set; }
        [Display(Name = "委托开始日期")]
        [DataType(DataType.Date)]
        public DateTime? WeituoKSRQ { get; set; }
        [Display(Name = "委托结束日期")]
        [DataType(DataType.Date)]
        public DateTime? WeituoJSRQ { get; set; }
        [Display(Name = "委托期限")]
        public int? WeituoQX { get; set; }
        [Display(Name = "贴中文标签")]
        public bool TieZWBQ { get; set; }
        [Display(Name = "合同文件")]
        public string HetongTP { get; set; }
        [Display(Name = "法人")]
        public string Faren { get; set; }
        [Display(Name = "负责人")]
        public string Fuzeren { get; set; }
        [Display(Name = "三证合一")]
        public bool HeyiSF { get; set; }
        [Display(Name = "对象序号")]
        public int? WTKHID { get; set; }
    }
}

