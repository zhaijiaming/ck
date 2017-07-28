using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Generic;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using CKWMS.Filters;

namespace CKWMS.Controllers
{
public class quan_gspshdwController : Controller
{
private Iquan_gspshdwService ob_quan_gspshdwservice=ServiceFactory.quan_gspshdwservice;
[OutputCache(Duration = 30)]
public ActionResult Index(string page)
{
if (string.IsNullOrEmpty(page))
page ="1";
int userid=(int)Session["user_id"];
string pagetag="quan_gspshdw_index";
Expression<Func<quan_gspshdw, bool>> where = PredicateExtensionses.True<quan_gspshdw>();
searchcondition sc=searchconditionService.GetInstance().GetEntityById(searchcondition=>searchcondition.UserID==userid && searchcondition.PageBrief==pagetag);
if(sc!=null && sc.ConditionInfo!=null)
{
string[] sclist = sc.ConditionInfo.Split(';');
foreach (string scl in sclist)
{
string[] scld = scl.Split(',');
switch (scld[0])
{
case "huozhuid":
string huozhuid = scld[1];
string huozhuidequal = scld[2];
string huozhuidand = scld[3];
if (!string.IsNullOrEmpty(huozhuid))
{
if (huozhuidequal.Equals("="))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
}
if (huozhuidequal.Equals(">"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
}
if (huozhuidequal.Equals("<"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
}
}
break;
default:
break;
}
}
ViewBag.SearchCondition = sc.ConditionInfo;
}

where = where.And(quan_gspshdw=>quan_gspshdw.IsDelete == false);

var tempData = ob_quan_gspshdwservice.LoadSortEntities(where.Compile(),false,quan_gspshdw=>quan_gspshdw.ID).ToPagedList<quan_gspshdw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
ViewBag.quan_gspshdw = tempData;
return View(tempData);
}

[HttpPost]
[OutputCache(Duration = 30)]
public ActionResult Index()
{
int userid=(int)Session["user_id"];
string pagetag="quan_gspshdw_index";
string page="1";
string huozhuid = Request["huozhuid"]??"";
string huozhuidequal = Request["huozhuidequal"]??"";
string huozhuidand = Request["huozhuidand"]??"";
Expression<Func<quan_gspshdw, bool>> where = PredicateExtensionses.True<quan_gspshdw>();
searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
if (sc==null)
{
sc=new searchcondition();
sc.UserID=userid;
sc.PageBrief = pagetag;
if (!string.IsNullOrEmpty(huozhuid))
{
if (huozhuidequal.Equals("="))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
}
if (huozhuidequal.Equals(">"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
}
if (huozhuidequal.Equals("<"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
}
}
if (!string.IsNullOrEmpty(huozhuid))
sc.ConditionInfo=sc.ConditionInfo+string.Format("{0},{1},{2},{3};","huozhuid",huozhuid,huozhuidequal,huozhuidand);
else
sc.ConditionInfo=sc.ConditionInfo+string.Format("{0},{1},{2},{3};","huozhuid","",huozhuidequal,huozhuidand);
searchconditionService.GetInstance().AddEntity(sc);
}
else
{
sc.ConditionInfo="";
if (!string.IsNullOrEmpty(huozhuid))
{
if (huozhuidequal.Equals("="))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID==int.Parse(huozhuid));
}
if (huozhuidequal.Equals(">"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID>int.Parse(huozhuid));
}
if (huozhuidequal.Equals("<"))
{
if (huozhuidand.Equals("and"))
where = where.And(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
else
where = where.Or(quan_gspshdw=>quan_gspshdw.HuozhuID<int.Parse(huozhuid));
}
}
if (!string.IsNullOrEmpty(huozhuid))
sc.ConditionInfo=sc.ConditionInfo+string.Format("{0},{1},{2},{3};","huozhuid",huozhuid,huozhuidequal,huozhuidand);
else
sc.ConditionInfo=sc.ConditionInfo+string.Format("{0},{1},{2},{3};","huozhuid","",huozhuidequal,huozhuidand);
searchconditionService.GetInstance().UpdateEntity(sc);
}
ViewBag.SearchCondition = sc.ConditionInfo;
where = where.And(quan_gspshdw=>quan_gspshdw.IsDelete == false);

var tempData = ob_quan_gspshdwservice.LoadSortEntities(where.Compile(),false,quan_gspshdw=>quan_gspshdw.ID).ToPagedList<quan_gspshdw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
ViewBag.quan_gspshdw = tempData;
return View(tempData);
}

public ActionResult Add()
{
ViewBag.userid=(int)Session["user_id"];
return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Save()
{
string id = Request["id"]??"";
string huozhuid = Request["huozhuid"]??"";
string mingcheng = Request["mingcheng"]??"";
string yingyezhizhaobh = Request["yingyezhizhaobh"]??"";
string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"]??"";
string yingyezhizhaotp = Request["yingyezhizhaotp"]??"";
string jingyingxukebh = Request["jingyingxukebh"]??"";
string jingyingxukeyxq = Request["jingyingxukeyxq"]??"";
string jingyingxuketp = Request["jingyingxuketp"]??"";
string shouying = Request["shouying"]??"";
string col1 = Request["col1"]??"";
string col2 = Request["col2"]??"";
string col3 = Request["col3"]??"";
string col4 = Request["col4"]??"";
string col5 = Request["col5"]??"";
string col6 = Request["col6"]??"";
string makedate = Request["makedate"]??"";
string makeman = Request["makeman"]??"";
string shenchasf = Request["shenchasf"]??"";
string hezuosf = Request["hezuosf"]??"";
string jingyinfanwei = Request["jingyinfanwei"]??"";
string jingyinfanweidm = Request["jingyinfanweidm"]??"";
string quyu = Request["quyu"]??"";
string beianbh = Request["beianbh"]??"";
string beianyxq = Request["beianyxq"]??"";
string beianpzrq = Request["beianpzrq"]??"";
string beianfzjg = Request["beianfzjg"]??"";
string beiantp = Request["beiantp"]??"";
string xukepzrq = Request["xukepzrq"]??"";
string xukefzjg = Request["xukefzjg"]??"";
string qiyedz = Request["qiyedz"]??"";
string songhuodz = Request["songhuodz"]??"";
string lianxiren = Request["lianxiren"]??"";
string lianxidh = Request["lianxidh"]??"";
string shdwid = Request["shdwid"]??"";
try
{
quan_gspshdw ob_quan_gspshdw = new quan_gspshdw();
ob_quan_gspshdw.HuozhuID = huozhuid=="" ? 0:int.Parse(huozhuid);
ob_quan_gspshdw.Mingcheng = mingcheng.Trim();
ob_quan_gspshdw.YingyezhizhaoBH = yingyezhizhaobh.Trim();
ob_quan_gspshdw.YingyezhizhaoYXQ = yingyezhizhaoyxq=="" ? DateTime.Now: DateTime.Parse(yingyezhizhaoyxq);
ob_quan_gspshdw.YingyezhizhaoTP = yingyezhizhaotp.Trim();
ob_quan_gspshdw.JingyingxukeBH = jingyingxukebh.Trim();
ob_quan_gspshdw.JingyingxukeYXQ = jingyingxukeyxq=="" ? DateTime.Now: DateTime.Parse(jingyingxukeyxq);
ob_quan_gspshdw.JingyingxukeTP = jingyingxuketp.Trim();
ob_quan_gspshdw.Shouying = shouying=="" ? 0:int.Parse(shouying);
ob_quan_gspshdw.Col1 = col1.Trim();
ob_quan_gspshdw.Col2 = col2.Trim();
ob_quan_gspshdw.Col3 = col3.Trim();
ob_quan_gspshdw.Col4 = col4.Trim();
ob_quan_gspshdw.Col5 = col5.Trim();
ob_quan_gspshdw.Col6 = col6.Trim();
ob_quan_gspshdw.MakeDate = makedate=="" ? DateTime.Now: DateTime.Parse(makedate);
ob_quan_gspshdw.MakeMan = makeman=="" ? 0:int.Parse(makeman);
ob_quan_gspshdw.ShenchaSF = shenchasf=="" ? false:Boolean.Parse(shenchasf);
ob_quan_gspshdw.HezuoSF = hezuosf=="" ? false:Boolean.Parse(hezuosf);
ob_quan_gspshdw.Jingyinfanwei = jingyinfanwei.Trim();
ob_quan_gspshdw.JingyinfanweiDM = jingyinfanweidm.Trim();
ob_quan_gspshdw.Quyu = quyu.Trim();
ob_quan_gspshdw.BeianBH = beianbh.Trim();
ob_quan_gspshdw.BeianYXQ = beianyxq=="" ? DateTime.Now: DateTime.Parse(beianyxq);
ob_quan_gspshdw.BeianPZRQ = beianpzrq=="" ? DateTime.Now: DateTime.Parse(beianpzrq);
ob_quan_gspshdw.BeianFZJG = beianfzjg.Trim();
ob_quan_gspshdw.BeianTP = beiantp.Trim();
ob_quan_gspshdw.XukePZRQ = xukepzrq=="" ? DateTime.Now: DateTime.Parse(xukepzrq);
ob_quan_gspshdw.XukeFZJG = xukefzjg.Trim();
ob_quan_gspshdw.QiyeDZ = qiyedz.Trim();
ob_quan_gspshdw.SonghuoDZ = songhuodz.Trim();
ob_quan_gspshdw.Lianxiren = lianxiren.Trim();
ob_quan_gspshdw.LianxiDH = lianxidh.Trim();
ob_quan_gspshdw.SHDWID = shdwid=="" ? 0:int.Parse(shdwid);
ob_quan_gspshdw=ob_quan_gspshdwservice.AddEntity(ob_quan_gspshdw);
ViewBag.quan_gspshdw = ob_quan_gspshdw;
}
catch (Exception ex)
{
Console.WriteLine(ex.Message);
}
return RedirectToAction("Index");
}

[OutputCache(Duration =10)]
public ActionResult Edit(int id)
{
quan_gspshdw tempData = ob_quan_gspshdwservice.GetEntityById(quan_gspshdw=>quan_gspshdw.ID==id && quan_gspshdw.IsDelete==false);
ViewBag.quan_gspshdw = tempData;
if (tempData == null)
return View();
else
{
quan_gspshdwViewModel quan_gspshdwviewmodel = new quan_gspshdwViewModel();
quan_gspshdwviewmodel.ID = tempData.ID;
quan_gspshdwviewmodel.HuozhuID = tempData.HuozhuID;
quan_gspshdwviewmodel.Mingcheng = tempData.Mingcheng;
quan_gspshdwviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
quan_gspshdwviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
quan_gspshdwviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
quan_gspshdwviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
quan_gspshdwviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
quan_gspshdwviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
quan_gspshdwviewmodel.Shouying = tempData.Shouying;
quan_gspshdwviewmodel.Col1 = tempData.Col1;
quan_gspshdwviewmodel.Col2 = tempData.Col2;
quan_gspshdwviewmodel.Col3 = tempData.Col3;
quan_gspshdwviewmodel.Col4 = tempData.Col4;
quan_gspshdwviewmodel.Col5 = tempData.Col5;
quan_gspshdwviewmodel.Col6 = tempData.Col6;
quan_gspshdwviewmodel.MakeDate = tempData.MakeDate;
quan_gspshdwviewmodel.MakeMan = tempData.MakeMan;
quan_gspshdwviewmodel.ShenchaSF = tempData.ShenchaSF;
quan_gspshdwviewmodel.HezuoSF = tempData.HezuoSF;
quan_gspshdwviewmodel.Jingyinfanwei = tempData.Jingyinfanwei;
quan_gspshdwviewmodel.JingyinfanweiDM = tempData.JingyinfanweiDM;
quan_gspshdwviewmodel.Quyu = tempData.Quyu;
quan_gspshdwviewmodel.BeianBH = tempData.BeianBH;
quan_gspshdwviewmodel.BeianYXQ = tempData.BeianYXQ;
quan_gspshdwviewmodel.BeianPZRQ = tempData.BeianPZRQ;
quan_gspshdwviewmodel.BeianFZJG = tempData.BeianFZJG;
quan_gspshdwviewmodel.BeianTP = tempData.BeianTP;
quan_gspshdwviewmodel.XukePZRQ = tempData.XukePZRQ;
quan_gspshdwviewmodel.XukeFZJG = tempData.XukeFZJG;
quan_gspshdwviewmodel.QiyeDZ = tempData.QiyeDZ;
quan_gspshdwviewmodel.SonghuoDZ = tempData.SonghuoDZ;
quan_gspshdwviewmodel.Lianxiren = tempData.Lianxiren;
quan_gspshdwviewmodel.LianxiDH = tempData.LianxiDH;
quan_gspshdwviewmodel.SHDWID = tempData.SHDWID;
return View(quan_gspshdwviewmodel);
}
}

[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Update()
{
string id = Request["id"] ?? "";
string huozhuid = Request["huozhuid"] ?? "";
string mingcheng = Request["mingcheng"] ?? "";
string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
string jingyingxukebh = Request["jingyingxukebh"] ?? "";
string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
string jingyingxuketp = Request["jingyingxuketp"] ?? "";
string shouying = Request["shouying"] ?? "";
string col1 = Request["col1"] ?? "";
string col2 = Request["col2"] ?? "";
string col3 = Request["col3"] ?? "";
string col4 = Request["col4"] ?? "";
string col5 = Request["col5"] ?? "";
string col6 = Request["col6"] ?? "";
string makedate = Request["makedate"] ?? "";
string makeman = Request["makeman"] ?? "";
string shenchasf = Request["shenchasf"] ?? "";
string hezuosf = Request["hezuosf"] ?? "";
string jingyinfanwei = Request["jingyinfanwei"] ?? "";
string jingyinfanweidm = Request["jingyinfanweidm"] ?? "";
string quyu = Request["quyu"] ?? "";
string beianbh = Request["beianbh"] ?? "";
string beianyxq = Request["beianyxq"] ?? "";
string beianpzrq = Request["beianpzrq"] ?? "";
string beianfzjg = Request["beianfzjg"] ?? "";
string beiantp = Request["beiantp"] ?? "";
string xukepzrq = Request["xukepzrq"] ?? "";
string xukefzjg = Request["xukefzjg"] ?? "";
string qiyedz = Request["qiyedz"] ?? "";
string songhuodz = Request["songhuodz"] ?? "";
string lianxiren = Request["lianxiren"] ?? "";
string lianxidh = Request["lianxidh"] ?? "";
string shdwid = Request["shdwid"] ?? "";
int uid = int.Parse(id);
try
{
quan_gspshdw p= ob_quan_gspshdwservice.GetEntityById(quan_gspshdw=>quan_gspshdw.ID==uid);
p.HuozhuID = huozhuid=="" ? 0:int.Parse(huozhuid);
p.Mingcheng = mingcheng.Trim();
p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
p.YingyezhizhaoYXQ = yingyezhizhaoyxq=="" ? DateTime.Now: DateTime.Parse(yingyezhizhaoyxq);
p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
p.JingyingxukeBH = jingyingxukebh.Trim();
p.JingyingxukeYXQ = jingyingxukeyxq=="" ? DateTime.Now: DateTime.Parse(jingyingxukeyxq);
p.JingyingxukeTP = jingyingxuketp.Trim();
p.Shouying = shouying=="" ? 0:int.Parse(shouying);
p.Col1 = col1.Trim();
p.Col2 = col2.Trim();
p.Col3 = col3.Trim();
p.Col4 = col4.Trim();
p.Col5 = col5.Trim();
p.Col6 = col6.Trim();
p.MakeDate = makedate=="" ? DateTime.Now: DateTime.Parse(makedate);
p.MakeMan = makeman=="" ? 0:int.Parse(makeman);
p.ShenchaSF = shenchasf=="" ? false:Boolean.Parse(shenchasf);
p.HezuoSF = hezuosf=="" ? false:Boolean.Parse(hezuosf);
p.Jingyinfanwei = jingyinfanwei.Trim();
p.JingyinfanweiDM = jingyinfanweidm.Trim();
p.Quyu = quyu.Trim();
p.BeianBH = beianbh.Trim();
p.BeianYXQ = beianyxq=="" ? DateTime.Now: DateTime.Parse(beianyxq);
p.BeianPZRQ = beianpzrq=="" ? DateTime.Now: DateTime.Parse(beianpzrq);
p.BeianFZJG = beianfzjg.Trim();
p.BeianTP = beiantp.Trim();
p.XukePZRQ = xukepzrq=="" ? DateTime.Now: DateTime.Parse(xukepzrq);
p.XukeFZJG = xukefzjg.Trim();
p.QiyeDZ = qiyedz.Trim();
p.SonghuoDZ = songhuodz.Trim();
p.Lianxiren = lianxiren.Trim();
p.LianxiDH = lianxidh.Trim();
p.SHDWID = shdwid=="" ? 0:int.Parse(shdwid);
ob_quan_gspshdwservice.UpdateEntity(p);
ViewBag.saveok = ViewAddTag.ModifyOk;
}
catch (Exception ex)
{
Console.WriteLine(ex.Message);
ViewBag.saveok = ViewAddTag.ModifyNo;
}
return RedirectToAction("Edit", new { id = uid});
}
 public ActionResult Delete()
{
string sdel= Request["del"] ?? "";
int id;
quan_gspshdw ob_quan_gspshdw;
foreach (string sD in sdel.Split(','))
{
if(sD.Length>0)
{
id = int.Parse(sD);
ob_quan_gspshdw=ob_quan_gspshdwservice.GetEntityById(quan_gspshdw=>quan_gspshdw.ID== id && quan_gspshdw.IsDelete == false);
ob_quan_gspshdw.IsDelete = true;
ob_quan_gspshdwservice.UpdateEntity(ob_quan_gspshdw);
}
}
return RedirectToAction("Index");
 }    }                } 

