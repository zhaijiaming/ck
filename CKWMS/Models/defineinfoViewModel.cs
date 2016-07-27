using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CKWMS.Models
{
public partial class defineinfoViewModel
{
[Display(Name="序号")]
public int ID { get; set; }
[Display(Name="对象名称")]
public string ObjName { get; set; }
[Display(Name="属性名称")]
public string TBValue { get; set; }
[Display(Name="栏目类型")]
public int TBType { get; set; }
[Display(Name="语言一")]
public string Lan1 { get; set; }
[Display(Name="语言二")]
public string Lan2 { get; set; }
[Display(Name="语言三")]
public string Lan3 { get; set; }
[Display(Name="已删除")]
public bool IsDelete { get; set; }
}
}

